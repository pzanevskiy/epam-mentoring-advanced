﻿using AsyncAwait.Task2.CodeReviewChallenge.Models;
using AsyncAwait.Task2.CodeReviewChallenge.Models.Support;
using AsyncAwait.Task2.CodeReviewChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Controllers;

public class HomeController : Controller
{
    private readonly IAssistant _assistant;

    private readonly IPrivacyDataService _privacyDataService;

    public HomeController(IAssistant assistant, IPrivacyDataService privacyDataService)
    {
        _assistant = assistant ?? throw new ArgumentNullException(nameof(assistant));
        _privacyDataService = privacyDataService ?? throw new ArgumentNullException(nameof(privacyDataService));
    }

    public IActionResult Index()
    {
        return View();
    }

    // Method should be asynchronous, return Interface
    public async Task<IActionResult> Privacy()
    {
        // Can be awaited
        ViewBag.Message = await _privacyDataService.GetPrivacyDataAsync();
        return View();
    }

    public async Task<IActionResult> Help()
    {
        ViewBag.RequestInfo = await _assistant.RequestAssistanceAsync("guest");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
