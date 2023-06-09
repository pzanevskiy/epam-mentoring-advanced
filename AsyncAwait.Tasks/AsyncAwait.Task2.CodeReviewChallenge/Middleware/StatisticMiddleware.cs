﻿using AsyncAwait.Task2.CodeReviewChallenge.Headers;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;

        // No need to run task in new thread and wait for it
        await _statisticService.RegisterVisitAsync(path);

        // Little optimization of UpdateHeaders(). No need to wait task completion
        var count = await _statisticService.GetVisitsCountAsync(path);
        context.Response.Headers.Add(CustomHttpHeaders.TotalPageVisits, count.ToString());

        await _next(context);
    }
}
