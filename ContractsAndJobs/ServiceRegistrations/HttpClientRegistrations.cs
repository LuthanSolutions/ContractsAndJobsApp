using ContractsAndJobs.ApiServices;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace ContractsAndJobs.ServiceRegistrations;

public static class HttpClientRegistrations
{
    internal static void RegisterHttpClientServices(IServiceCollection services)
    {
        services.AddHttpClient<IApiService, ApiService>(client =>
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/"))
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(delay);
    }
}
