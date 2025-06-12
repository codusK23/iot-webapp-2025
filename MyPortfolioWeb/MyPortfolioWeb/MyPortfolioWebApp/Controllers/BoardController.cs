using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System;

namespace MyPortfolioWebApp.Controllers
{
    public class BoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Board
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            ViewData["Title"] = "서버에서 변경가능";

            // 최종단계
            var totalCount = _context.Board.Where(n => EF.Functions.Like(n.Title, $"%{search}%")).Count(); // HACK! 위 구문이 오류 발생
            var countList = 10; // 한 페이지에 기본 뉴스갯수 10개
            var totalPage = totalCount / countList; // 한 페이지당 개수로 나누면 전체페이지 수 
            // HACK : 게시판페이지 중요 로직. 남는 데이터도 한 페이지를 차지해야 함
            if (totalCount % countList > 0) totalPage++;  // 남은 게시글이 있으면 페이지수 증가
            if (totalPage < page) page = totalPage;
            // 마지막 페이지 구하기
            var countPage = 10; // 페이지를 표시할 최대페이지개수, 10개
            var startPage = ((page - 1) / countPage) * countPage + 1;
            var endPage = startPage + countPage - 1;
            // HACK : 나타낼 페이지 수가 10이 안 되면 페이지 수 조정.
            // 마지막 페이지까지 글이 12개면  1, 2 페이지만 표시
            if (totalPage < endPage) endPage = totalPage;

            // 저장 프로시저에 보낼 rowNum값, 시작번호랑 끝번호
            var startCount = ((page - 1) * countPage) + 1; // 2페이지의 경우 11
            var endCount = startCount + countList - 1; // 2페이지의 경우 20

            // View로 넘기는 데이터, 페이징 숫자컨트롤 사용
            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.Search = search;  // 검색어

            // 저장프로시저 호출
            var board = await _context.Board.FromSql($"CALL New_PagingBoard({startCount}, {endCount}, {search})").ToListAsync();
            return View(board);
        }

        // GET: Board/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            board.ReadCount++;
            _context.Board.Update(board);
            await _context.SaveChangesAsync();

            return View(board);
        }

        // GET: Board/Create
        public IActionResult Create()
        {
            var board = new Board
            {   // 여기 어캐 수정?
                Writer = "관리자",
                PostDate = DateTime.Now,
                ReadCount = 0,
            };
            return View(board);
        }

        // POST: Board/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Writer,Title,Contents")] Board board)
        {
            if (ModelState.IsValid)
            {
                board.Writer = board.Writer;         // 입력한 닉네임이 작성자
                board.PostDate = DateTime.Now; // 게시일자는 현재
                board.ReadCount = 0;

                _context.Add(board);
                await _context.SaveChangesAsync();

                TempData["success"] = "게시글 저장 성공!";
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Board/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (TempData["Verified"]?.ToString() != "true")
            {
                return RedirectToAction("VerifyEmail", new { id });
            }

            TempData.Keep("Verified"); // TempData 유지

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Board/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Writer,Title,Contents")] Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBoard = await _context.Board.FindAsync(id);
                    if (existingBoard == null)
                    {
                        return NotFound();
                    }

                    if (!string.Equals(existingBoard.Email, board.Email, StringComparison.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError("Email", "이메일이 일치하지 않아 수정할 수 없습니다.");
                        return View(board);
                    }

                    existingBoard.Title = board.Title;
                    existingBoard.Contents = board.Contents;

                    await _context.SaveChangesAsync();
                    TempData["success"] = "게시글 수정 성공!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // 이메일 입력 화면(GET)
        [HttpGet]
        public IActionResult VerifyEmail(int id, string action = "edit")
        {
            // id로 게시글 존재 여부 확인
            var board = _context.Board.Find(id);
            if (board == null)
                return NotFound();

            ViewBag.Action = action;
            return View("VerifyEmail", board);
        }

        // 이메일 확인 처리(POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyEmail(int id, string email, string action = "edit")
        {
            var board = _context.Board.Find(id);
            if (board == null)
                return NotFound();

            if (!string.Equals(board.Email?.Trim(), email?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Email", "이메일이 일치하지 않습니다.");
                ViewBag.BoardId = id;
                ViewBag.Action = action;
                return View("VerifyEmail", board);  // 뷰 이름 명시 + 모델 전달
            }

            TempData["Verified"] = "true";
            if (action == "delete")
            {
                return RedirectToAction("Delete", new { id });
            }
            else
            {
                return RedirectToAction("Edit", new { id });
            }
        }

        // 이메일 인증 화면 (삭제용)
        [HttpGet]
        public IActionResult VerifyEmailForDelete(int id)
        {
            var board = _context.Board.Find(id);
            if (board == null) return NotFound();

            ViewBag.BoardId = id;
            return View("VerifyEmailForDelete", board);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyEmailForDelete(int id, string email)
        {
            var board = _context.Board.Find(id);
            if (board == null) return NotFound();

            if (!string.Equals(board.Email?.Trim(), email?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Email", "이메일이 일치하지 않습니다.");
                ViewBag.BoardId = id;
                return View("VerifyEmailForDelete", board);
            }

            TempData[$"Verified_Delete_{id}"] = "true";
            return RedirectToAction("Delete", new { id });
        }

        // 삭제 GET (확인 페이지)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            if (TempData[$"Verified_Delete_{id}"]?.ToString() != "true")
            {
                return RedirectToAction("VerifyEmailForDelete", new { id });
            }
            TempData.Keep($"Verified_Delete_{id}");

            var board = await _context.Board.FindAsync(id);
            if (board == null) return NotFound();

            return View(board);
        }

        // POST: Board/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (TempData[$"Verified_Delete_{id}"]?.ToString() != "true")
            {
                return RedirectToAction("VerifyEmailForDelete", new { id });
            }

            TempData.Keep($"Verified_Delete_{id}");

            var board = await _context.Board.FindAsync(id);
            if (board != null)
            {
                _context.Board.Remove(board);
                await _context.SaveChangesAsync();
            }
            TempData.Remove($"Verified_Delete_{id}");

            TempData["success"] = "게시글 삭제 성공!";
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(int id)
        {
            return _context.Board.Any(e => e.Id == id);
        }
    }
}
