using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SuggestionSubmission
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(512)]
    public string Onderwerp { get; set; }

    [Required]
    [Column(TypeName = "TEXT")] 
    public string Beschrijving { get; set; }

    public int? UserId { get; set; }

    [StringLength(512)]
    public string UserName { get; set; }

    [Required]
    [RegularExpression("^(suggestie|uitje)$", ErrorMessage = "Type moet 'suggestie' of 'uitje' zijn")]
    public string Type { get; set; } = "suggestie";

    [Display(Name = "Begin datum")]
    public DateTime? BeginDatum { get; set; }

    [Display(Name = "Eind datum")]
    public DateTime? EindDatum { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}