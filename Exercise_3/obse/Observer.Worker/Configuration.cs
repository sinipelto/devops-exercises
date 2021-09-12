namespace Observer.Worker
{
    public class AppsettingsConfig
    {
        public Logging Logging { get; set; }
        public int IdleTimeout { get; set; }
        public RabbitConfig RabbitConfig { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class RabbitConfig
    {
        public string RabbitHost { get; set; }
        public string RabbitExchange { get; set; }
        public string[] RabbitTopics { get; set; }
    }
}