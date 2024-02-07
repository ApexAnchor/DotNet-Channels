
namespace DotnetChannels.Models
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public override string ToString()
        {
            return $"Here is the weather forecast for the date {Date}: " +
                   $"Temperature in Celcius: {TemperatureC}, " +
                   $"Temperature in Farehneit: {TemperatureF}, " +
                   $"Summary: {Summary}";
        }
    }

}
