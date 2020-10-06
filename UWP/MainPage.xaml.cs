using Microsoft.Azure.Devices.Client;
using SharedUwpLibrary.Models;
using SharedUwpLibrary.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private DeviceClient deviceClient = 
            DeviceClient.CreateFromConnectionString("HostName=EC-WIN20-MB-IoT-hubb-1.azure-devices.net;DeviceId=ConsoleApp;SharedAccessKey=KhOzfu1bdjGysi3hmNSD+lA7RBVHjjtoaThZ+c0RIyY=", TransportType.Mqtt);


        public MainPage()

        {
            this.InitializeComponent();

            ReceiveMessageAsync().GetAwaiter();

        }


        private async void btnTempHum_Click_1(object sender, RoutedEventArgs e)
        {
            {
                await DeviceService.SendMessageAsync(deviceClient);
            }
        }

        private async Task SendMessageAsync(string message)
        {
            var payload = new Message(Encoding.UTF8.GetBytes(message));
            await deviceClient.SendEventAsync(payload);
        }

        private async Task ReceiveMessageAsync()
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                    continue;

                await deviceClient.CompleteAsync(payload);
            }
        }

        
    }
}
