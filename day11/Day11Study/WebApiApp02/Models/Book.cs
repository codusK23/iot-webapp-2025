using System.ComponentModel.DataAnnotations;

namespace WebApiApp02.Models
{
    public class Book
    {
        [Key]
        // Key
        public int Idx { get; set; }

        [Required]
        // 책제목
        public string Names { get; set; }

        [Required]
        // 책저자
        public string Author { get; set; }
        
        [Required]
        // 출판일
        public DateOnly ReleaseDate { get; set; }
    }
}
