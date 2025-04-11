using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Categorienaam is verplicht")]
    [StringLength(512, ErrorMessage = "Categorienaam mag maximaal 512 tekens bevatten")]
    public string Name { get; set; }

    // Navigation property
    public ICollection<SuggestionSubmission> Ideas { get; set; } = new List<SuggestionSubmission>();
}