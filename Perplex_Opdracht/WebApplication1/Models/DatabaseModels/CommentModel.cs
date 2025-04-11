using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Reactie is verplicht")]
    public string Content { get; set; }

    public string Author { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign key
    public int SuggestionId { get; set; }

    // Navigation property
    public SuggestionSubmission Suggestion { get; set; }
}