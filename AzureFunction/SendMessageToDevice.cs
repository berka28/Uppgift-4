using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibraries.Models;
using SharedLibraries.Services;
using Microsoft.Azure.Devices;
using System.Text;

namespace AzureFunction
{
    public static class SendMessageToDevice
    {
        private static ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHubConnection"));

        [FunctionName("SendMessageToDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string DeviceId = req.Query["HostName=EC-WIN20-MB-IoT-hubb-1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Q5ue7rCw+m/F431P5x4DdH0Fyea0S/0Q4GSjqQwsgzw="];
            string message = req.Query["message"];

            var payload = new Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(req.Query["HostName=EC-WIN20-MB-IoT-hubb-1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Q5ue7rCw+m/F431P5x4DdH0Fyea0S/0Q4GSjqQwsgzw="], payload);

            return new OkResult();
        }
    }
}
