using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<SuggestionSubmission> Ideas { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=PerplexIdeas.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure many-to-many for Ideas and Categories
        modelBuilder.Entity<SuggestionSubmission>()
            .HasMany(i => i.Categories)
            .WithMany(c => c.Ideas)
            .UsingEntity<Dictionary<string, object>>(
                "IdeaCategories",
                j => j.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                j => j.HasOne<SuggestionSubmission>().WithMany().HasForeignKey("IdeaId"),
                j => j.ToTable("IdeaCategories"));
        
        // Configure one-to-many for Ideas and Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Suggestion)
            .WithMany(i => i.Comments)
            .HasForeignKey(c => c.SuggestionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed initial categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Teamactiviteit" },
            new Category { Id = 2, Name = "Kantoorinrichting" },
            new Category { Id = 3, Name = "Software" }
        );
    }
}