using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models;

public class Board
{
    [Key]
    [DisplayName("번호")]
    public int Id { get; set; }

    [DisplayName("이메일")]
    [Required(ErrorMessage = "{0}은 필수입니다.")]
    public string Email { get; set; }

    [DisplayName("작성자")]
    public string? Writer { get; set; }

    [DisplayName("제목")]
    [Required(ErrorMessage = "{0}은 필수입니다.")]
    public string Title { get; set; }

    [DisplayName("내용")]
    [Required(ErrorMessage = "{0}은 필수입니다.")]
    public string Contents { get; set; }

    [DisplayName("작성일")]
    [DisplayFormat(DataFormatString = "{0:yyyy년 MM월 dd일}", ApplyFormatInEditMode = true)]
    [BindNever]
    public DateTime? PostDate { get; set; }

    [DisplayName("조회수")]
    [BindNever]
    public int? ReadCount { get; set; }
}

