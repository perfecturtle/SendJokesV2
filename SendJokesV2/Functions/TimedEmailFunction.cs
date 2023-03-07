using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendJokesV2.Models;
using SendJokesV2.Services;

namespace SendJokesV2.Functions
{
    public class TimedEmailFunction
    {
        private readonly IAPIService _apiService;
        private readonly IEmailService _emailService;

        public TimedEmailFunction(IAPIService apiService, IEmailService emailService)
        {
            _apiService = apiService;
            _emailService = emailService;
        }
        [FunctionName("TimedEmailFunction")]
        public async Task Run([TimerTrigger("0 30 8 * * 1-5"     
#if DEBUG
, RunOnStartup = true
#endif
            )] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            JokesModel JokeResponse = await _apiService.GetJokesHttpRequest();
            var ComicUrl = await _apiService.GetComicHttpRequest();
            var fact = await _apiService.GetRandomFactHttpRequest();
            JokesOutput.JokeFormatter(JokeResponse);
            _emailService.ExecuteEmail(JokesOutput.title, JokesOutput.message, fact.FirstOrDefault()?.fact, ComicUrl).Wait();

        }
    }
}
