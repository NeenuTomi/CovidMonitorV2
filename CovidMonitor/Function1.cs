using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace CovidMonitor
{
    public static class Function1
    {
        [FunctionName("FunctionNew")]
        // azure function runs everday at 6.30 am
        public static void Run([TimerTrigger("0 30 6 * * *")]TimerInfo myTimer, ILogger log)
        {
            // calling function to get the count from website
            AzureBrowserClass.ExtractCount();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
