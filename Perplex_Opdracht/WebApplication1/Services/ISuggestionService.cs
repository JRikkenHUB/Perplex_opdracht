using WebApplication1.Models;

public interface ISuggestionService
{
    Task<ApiResponse> SubmitIdeaAsync(SuggestionModel submission);
}

public class SuggestionService : ISuggestionService
{
    private readonly IStorageProvider _storage;

    public SuggestionService(IStorageProvider storage)
    {
        _storage = storage;
    }

    public async Task<ApiResponse> SubmitIdeaAsync(SuggestionModel submission)
    {
        try
        {
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