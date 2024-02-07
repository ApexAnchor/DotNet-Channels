using DotnetChannels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

namespace DotnetChannels.Producer.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class MessageController : ControllerBase
    {  
        private readonly ChannelWriter<WeatherForecast> _channelWriter;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public MessageController()
        {
            _channelWriter = WeatherForecastChannel.Instance.Writer;            
        }

        [HttpPost(Name ="PublishMessageToChannel")]
        public async Task<bool> Publish()
        {
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            foreach (var forecast in forecasts)
            {
                if (await _channelWriter.WaitToWriteAsync())
                {
                    await _channelWriter.WriteAsync(forecast);
                    await Task.Delay(500);
                }
               
            }
            return true;
        }
    }
}
