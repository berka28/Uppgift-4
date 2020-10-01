using SharedLibraries.Services;
using SharedLibraries.Models;
using Microsoft.Azure.Devices.Client;
using System;

namespace ConsoleApp
{
    class Program
    {
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString("HostName=EC-WIN20-MB-IoT-hubb-1.azure-devices.net;DeviceId=ConsoleApp;SharedAccessKey=KhOzfu1bdjGysi3hmNSD+lA7RBVHjjtoaThZ+c0RIyY=", TransportType.Mqtt);


        static void Main(string[] args)
        {
            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
            DeviceService.ReceiveMessageAsync(deviceClient).GetAwaiter();

            Console.ReadKey();
        }
    }
}
