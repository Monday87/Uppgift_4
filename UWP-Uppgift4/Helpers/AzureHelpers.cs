using Microsoft.Azure.Devices.Client;
using SharedLibraryUniversalWindow.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Uppgift4.Helpers
{
    class AzureHelpers
    {
       
        

            private static readonly string _conn = "HostName=ec-win20-iothub-steven.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=LbS3gbEJz0lipgRWexRYLRdN0BH+RgIZ+D8R/oU6N6g=";

            private static readonly DeviceClient deviceClient =
                DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
            public static void SendRecieveMessageAsync()
            {

                DeviceServiceUwp.SendMessageAsync(deviceClient).GetAwaiter();
                DeviceServiceUwp.RecieveMessageAsync(deviceClient).GetAwaiter();

            }
        

    }
}
