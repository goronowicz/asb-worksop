using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace exc_two_receive
{
    class Program
    {
        private static IQueueClient queueClient;

        static void Main(string[] args)
        {
            var configuration = BuildConfiguration();

            //TODO: utwórz klienta czytającego z kolejki IQueueClient lub MessageReceiver
            // ustaw odpowiedni connection string i nazwę kolejki w konfiguracji
            // właściwości serviceBusConnection, queueName

            //zajerestruj message handler - ReceiveMessage i exceptionHandler - ExceptionHandler

            WaitForEnd();
        }

        private static Task CleanUp()
        {
            //TODO: zakończ połączenie do kolejki
            return Task.CompletedTask;
        }


        public static Task ReceiveMessage(Message message, CancellationToken cancellation)
        {
            lock (Console.Out)
            {
                //TODO: zdekoduj przesłaną wiadomość - Body
                var content = "";

                Console.WriteLine("Message received:");
                Console.WriteLine(content);
                
            }

            return Task.CompletedTask;
        }

        private static Task ExceptionHandler(ExceptionReceivedEventArgs arg)
        {
            lock (Console.Out)
            {
                Console.WriteLine(arg.Exception.Message);
            }
            return Task.CompletedTask;
        }

        private static void WaitForEnd()
        {
            Task.WhenAll(Task.Run(() => Console.ReadKey()),
                         Task.Delay(TimeSpan.FromSeconds(10))
                            ).ContinueWith((t) => CleanUp()).GetAwaiter().GetResult();
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            return configuration;
        }
    }
}
