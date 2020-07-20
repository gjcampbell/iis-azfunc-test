using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IIS.FuncTest
{
    public class Tests
    {
        private readonly ILogger<Tests> logger;

        public Tests(ILogger<Tests> logger)
        {
            this.logger = logger;
        }

        [FunctionName(nameof(TestHttp))]
        public IActionResult TestHttp(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            logger.LogInformation("HttpTrigger ran");
            return new OkObjectResult("Perfect! 🙃");
        }

        [FunctionName(nameof(TestQueue))]
        public void TestQueue([QueueTrigger("iis-q-test")]string item)
        {
            logger.LogInformation($"QueueTrigger ran value: {item}");
        }

        [FunctionName(nameof(TestStorage))]
        public void TestStorage([BlobTrigger("iis-blob-test/{name}")]Stream blob, string name)
        {
            logger.LogInformation($"BlobTrigger ran file name: {name}, size: {blob.Length}");
        }

        [FunctionName(nameof(TestTimer))]
        public void TestTimer([TimerTrigger("*/20 * * * * *")]TimerInfo timer)
        {
            logger.LogInformation($"TimerTrigger ran");
        }
    }
}
