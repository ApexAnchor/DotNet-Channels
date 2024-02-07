
using DotnetChannels.Models;
using System.Threading.Channels;

namespace DotnetChannels.Producer.Consumer
{
    public class WeatherForecastConsumer : BackgroundService
    {
        private static readonly ChannelReader<WeatherForecast> _channelReader = WeatherForecastChannel.Instance.Reader;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("Channel Reader is waiting for messages");

                while (!stoppingToken.IsCancellationRequested)
                {
                    await foreach (var forecast in _channelReader.ReadAllAsync())
                    {
                        Console.WriteLine("Channel Reader received a message");
                        Console.WriteLine($"Received weather forecast: {forecast}");
                        await Task.Delay(500);
                    }
                }                
            }
            catch (ChannelClosedException)
            {
                Console.WriteLine("Channel was closed");
            }
        }
    }
}
