using WebApplication1.Models;

public interface ISuggestionService
{
    Task<ApiResponse> SubmitIdeaAsync(SuggestionModel submission);
}

public class IdeaService : ISuggestionService
{
    private readonly IStorageProvider _storage;

    public IdeaService(IStorageProvider storage)
    {
        _storage = storage;
    }

    public async Task<ApiResponse> SubmitIdeaAsync(SuggestionModel submission)
    {
        try
        {
            // Additional business logic validation
            if (submission.Type == "uitje" && submission.BeginDatum > submission.EindDatum)
            {
                return new ApiResponse(false, "Eind datum moet na begin datum liggen");
            }

            
            var result = await _storage.SaveIdeaAsync(submission);

            return new ApiResponse(true, "Idee succesvol opgeslagen", result);
        }
        catch (Exception ex)
        {
            return new ApiResponse(false, $"Fout tijdens opslaan: {ex.Message}");
        }
    }
}