using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

public interface IAPIService
{
    public Task<SubmissionResult> SubmitIdea([FromBody] string submission);
}