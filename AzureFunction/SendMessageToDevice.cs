using AzureFunction.Models;
using AzureFunction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction
{
    public class SendMessageToDevice
    {


        private static readonly ServiceClient serviceClient =
         ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHubConnection"));

        [FunctionName("SendMessageToDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            // QueryString localhost:7071/api/sendmessagetodevice?targetdeviceid=consoleapp&message=dettaarmeddelandet

            string targetDeviceId = req.Query["targetdeviceid"];
            string message = req.Query["message"];


            // Http Body som vi skickar in ett json-objekt = { "targetdeviceid": "consoleapp", "message": "detta är ett meddelande" }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();





            var data = JsonConvert.DeserializeObject<BodyMessageUw>(requestBody);




            targetDeviceId = targetDeviceId ?? data?.TargetDeviceIde;
            message = message ?? data?.Messagee;

            await DeviceServices.SendMessageToDeviceAsync(serviceClient, targetDeviceId, message);


            return new OkResult();
        }
    }
}

