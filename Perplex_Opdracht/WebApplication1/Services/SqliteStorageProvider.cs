using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

public class SqliteStorageProvider : IStorageProvider
{
    private readonly AppDbContext _context;

    public SqliteStorageProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object> SaveIdeaAsync(SuggestionModel submission)
    {
        try
        {
            // Create new Idea entity
            var suggestion = new SuggestionSubmission
            {
                Onderwerp = submission.Onderwerp,
                Beschrijving = submission.Beschrijving,
                UserId = submission.UserId,
                UserName = submission.UserName,
                Type = submission.Type,
                BeginDatum = submission.BeginDatum,
                EindDatum = submission.EindDatum,
                CreatedAt = DateTime.UtcNow
            };

            // Handle categories
            foreach (var categoryName in submission.Categories)
            {
                var existingCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Name == categoryName);

                if (existingCategory != null)
                {
                    suggestion.Categories.Add(existingCategory);
                }
                else
                {
                    suggestion.Categories.Add(new Category { Name = categoryName });
                }
            }

            _context.Ideas.Add(suggestion);
            await _context.SaveChangesAsync();

            return new { Id = suggestion.Id, Success = true };
        }
        catch (Exception ex)
        {
            // Log error here (consider using ILogger)
            return new { Success = false, Error = ex.Message };
        }
    }
}