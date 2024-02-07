using System.Threading.Channels;

namespace DotnetChannels.Models
{
    public sealed class WeatherForecastChannel
    {
        private static WeatherForecastChannel? instance;

        private static readonly object _lock = new object();

        private  readonly Channel<WeatherForecast> channel;

        private WeatherForecastChannel()
        {
            channel = Channel.CreateUnbounded<WeatherForecast>(
                new UnboundedChannelOptions
                {
                    SingleWriter = true,
                    SingleReader = false,
                    AllowSynchronousContinuations = true
                }
            );
        }

        public static WeatherForecastChannel Instance
        {
            get
            {
                if (instance == null) // Null check
                {
                    lock (_lock) // Thread-safe lock
                    {
                        if (instance == null) // Double-check to prevent race conditions
                        {
                            instance = new WeatherForecastChannel();
                        }
                    }
                }
                return instance;
            }
        }

        // public static WeatherForecastChannel Instance => instance;

        public ChannelWriter<WeatherForecast> Writer => channel.Writer;

        public ChannelReader<WeatherForecast> Reader => channel.Reader;
    }
}
