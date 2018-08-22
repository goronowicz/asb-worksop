using Microsoft.Azure.ServiceBus;
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

            queueClient = new QueueClient(configuration["serviceBusConnection"],
                configuration["queueName"],
                receiveMode: ReceiveMode.ReceiveAndDelete);
            var options = new MessageHandlerOptions(ExceptionHandler);
            queueClient.RegisterMessageHandler(ReceiveMessage, options);

            WaitForEnd();
        }

        private static Task CleanUp()
        {
            return queueClient.CloseAsync();
        }

        public static Task ReceiveMessage(Message message, CancellationToken cancellation)
        {
            lock (Console.Out)
            {
                var content = Encoding.UTF8.GetString(message.Body);

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
