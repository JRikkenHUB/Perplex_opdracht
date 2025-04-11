using Microsoft.AspNetCore.Mvc;

public interface IAPIController
{
    public Task<IActionResult> SubmitIdea([FromBody] string submission);
}