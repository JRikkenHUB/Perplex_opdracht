using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

public class SuggestionController : Controller
{
    public readonly IAPIService _api;
    public SuggestionController(IAPIService controller) {
        _api = controller;
    }

    public IActionResult Create()
    {
        return base.View(new SuggestionModel
        {
            Categories = new List<string>()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SuggestionModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Type == "uitje")
            {
                if (model.BeginDatum == null)
                {
                    ModelState.AddModelError("BeginDatum", "Begin datum is verplicht voor uitje");
                }
                if (model.EindDatum == null)
                {
                    ModelState.AddModelError("EindDatum", "Eind datum is verplicht voor uitje");
                }
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try{
                    var jsonRequest = TransformToJson(model);

                    SubmissionResult result = await _api.SubmitIdea(jsonRequest);

                    if (result.Success)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else {
                        return BadRequest(result.Error);
                    }
                }
                catch (Exception ex) {
                    ModelState.AddModelError("", $"Error submitting idea: {ex.Message}");
                    return View(model);
                }
            }
        }

        return View(model);
    }

    private string TransformToJson(SuggestionModel model)
    {
        var jsonObject = new
        {
            onderwerp = model.Onderwerp,
            beschrijving = model.Beschrijving,
            userId = model.UserId,
            userName = model.UserName,
            type = model.Type,
            beginDatum = model.BeginDatum?.ToString("yyyy-MM-dd HH:mm"),
            eindDatum = model.EindDatum?.ToString("yyyy-MM-dd HH:mm"),
            categories = model.Categories
        };

        return Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);
    }
}