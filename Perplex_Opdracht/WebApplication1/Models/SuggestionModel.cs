using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SuggestionModel
    {
        [Required]
        [StringLength(512)]
        public string Onderwerp { get; set; }

        [Required]
        public string Beschrijving { get; set; }

        public int? UserId { get; set; }

        [StringLength(512)]
        public string UserName { get; set; }

        [Required]
        public string Type { get; set; } = "suggestie"; // Default to "suggestie"

        [Display]
        public DateTime? BeginDatum { get; set; }

        [Display]
        public DateTime? EindDatum { get; set; }

        public List<string> Categories { get; set; } = new List<string>();
    }
}
