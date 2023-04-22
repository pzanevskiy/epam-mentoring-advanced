using CloudServices.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Models.Support;

public class ManualAssistant : IAssistant
{
    private const string ExceptionMessage = "Failed to register assistance request. Please try later. {0}";

    private readonly ISupportService _supportService;

    public ManualAssistant(ISupportService supportService)
    {
        _supportService = supportService ?? throw new ArgumentNullException(nameof(supportService));
    }

    public async Task<string> RequestAssistanceAsync(string requestInfo)
    {
        try
        {
            await _supportService.RegisterSupportRequestAsync(requestInfo);
            return await _supportService.GetSupportInfoAsync(requestInfo);
        }
        catch (HttpRequestException ex)
        {
            return await Task.FromResult(string.Format(ExceptionMessage, ex.Message));
        }
    }
}
