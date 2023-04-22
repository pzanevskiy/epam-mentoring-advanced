using System.Threading.Tasks;

namespace AsyncAwait.Task2.CodeReviewChallenge.Services;

public class PrivacyDataService : IPrivacyDataService
{
    private const string PrivacyDataMessage =
        "This Policy describes how async/await processes your personal data, " +
        "but it may not address all possible data processing scenarios.";

    public async Task<string> GetPrivacyDataAsync()
    {
        return await ValueTask.FromResult(PrivacyDataMessage);
    }
}
