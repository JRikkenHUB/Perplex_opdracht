using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

[ApiController]
[Route("api/[controller]")]
public class APIController : ControllerBase, IAPIController
{
    private readonly ISuggestionService _suggestionService;

    public APIController(ISuggestionService suggestionService)
    {
        _suggestionService = suggestionService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitIdea([FromBody] string submission)
    {
        SuggestionModel? convertedModel = JsonConvert.DeserializeObject<SuggestionModel>(submission);

        if (convertedModel == null)
        {
            return BadRequest("Invalid suggestion");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new ApiResponse(false, "Ongeldig verzoek", errors));
        }

        var result = await _suggestionService.SubmitIdeaAsync(convertedModel);

        return result.Success
            ? Ok(result)
            : BadRequest(result);
    }
}