using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApplication1.Controllers;
using WebApplication1.Models;

public class APIService : IAPIService
{
    private readonly ISuggestionService _suggestionService;

    public APIService(ISuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
    }

    public async Task<SubmissionResult> SubmitIdea([FromBody] string submission)
    {
        if (!IsValidJson(submission)) { 
            return new SubmissionResult { Success = false, Error = "Invalid Json" };
        }

        SuggestionModel? convertedModel = JsonConvert.DeserializeObject<SuggestionModel>(submission);

        if (convertedModel == null)
        {
            return new SubmissionResult { Success = false, Error = "Invalid Suggestion" };
        }

        var result = await _suggestionService.SubmitIdeaAsync(convertedModel);

        return result.Success
            ? new SubmissionResult { Success = true, Error = "Saved successfully" }
            : new SubmissionResult { Success = false, Error = "Problem occured during saving" };
    }

    public static bool IsValidJson(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return false;
        }

        try
        {
            JToken.Parse(jsonString);
            return true;
        }
        catch (JsonReaderException)
        {
            return false;
        }
    }
}