namespace WebApplication1.Models.ViewModels
{
    public class SuggestionViewModel
    {
        public int Id { get; set; }
        public string Onderwerp { get; set; }
        public string Beschrijving { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Categories { get; set; }
        public int CommentCount { get; set; }
    }
}
