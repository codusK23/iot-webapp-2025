namespace MyPortfolioWebApp.Models
{
    public class AboutModel
    {
        public About? About { get; set; }   // ? -> null 값도 처리한다는 의미
        public IEnumerable<Skill> Skill { get; set; }   // 스킬 여러 건이 들어갈거임
    }
}
