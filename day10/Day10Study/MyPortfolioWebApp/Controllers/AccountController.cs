using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp.Controllers
{
    public class AccountController : Controller
    {
        // ASP.NET Core Identity 필요한 변수
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;

        // 생성자
        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            // userManager나 signInManager에 null값이 들어오면 안 됨
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();  // Register.cshtml을 렌더링
        }

        // NewController GET Create(), POST Create()와 동일하게 생각
        [HttpPost]  // [HttpGet]가 default. 생략 가능
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Id를 이메일로 사용하겠다.
                // 나중에 속성 추가
                var user = new CustomUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);   // 자동으로 DB에 저장

                if (result.Succeeded)
                {
                    // 위의 저장한 유저로 로그인, isPersistent 로그인 상태 유지.
                    // false -> 20~30분 동안 사용 안 하면 로그아웃
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");  
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model); // 회원가입 오류나면 다시 회원가입 화면으로 돌아감
        }

        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home"); // 로그인 성공하면 홈으로
                }
                ModelState.AddModelError("", "로그인 실패!");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
