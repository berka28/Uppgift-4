using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedUwpLibrary.Models;

namespace SharedUwpLibrary.Services
{
    public static class DeviceService
    {
        private static readonly Random rnd = new Random();

        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var data = new TemperatureModel
                {
                    Temperature = rnd.Next(10, 50),
                    Humidity = rnd.Next(10, 80)
                };

                var json = JsonConvert.SerializeObject(data);

                var payload = new Message(Encoding.UTF8.GetBytes(json));
                await deviceClient.SendEventAsync(payload);

                Console.WriteLine($"Message sent: {json}");
                await Task.Delay(15 * 1000);
            }
        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

                Console.WriteLine($"Message Received: { Encoding.UTF8.GetString(payload.GetBytes())}");

                await deviceClient.CompleteAsync(payload);
            }
        }

        public static async Task SendMessageToDeviceAsync(MAD.ServiceClient serviceClient, string targetDeviceId, string message)
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceId, payload);

        }
    }
}
