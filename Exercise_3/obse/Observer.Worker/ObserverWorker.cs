using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Observer.Worker
{
    public class ObserverWorker : BackgroundService
    {
        private readonly ILogger<ObserverWorker> _logger;

        private readonly string _targetFile;
        private readonly RabbitConfig _rabbitConfig = new RabbitConfig();

        private ConnectionFactory _factory;

        public ObserverWorker(ILogger<ObserverWorker> logger, IConfiguration configuration)
        {
            _logger = logger;

            _targetFile = configuration.GetValue<string>("TargetFilePath");
            configuration.Bind("RabbitConfig", _rabbitConfig);

            _ = DeleteFileAsync(_targetFile);
            InitRabbitFactory();

            _logger.LogInformation("Worker initialized.");
        }
        
        public void InitRabbitFactory()
        {
            _factory = new ConnectionFactory
            {
                HostName = _rabbitConfig.RabbitHost
            };
        }

        private static async Task DeleteFileAsync(string path)
        {
            File.Delete(path);
            await Task.Yield();
        }

        private static Task WriteMessageToFileAsync(string path, string message)
        {
            return File.AppendAllTextAsync(path, message);
        }

        private void Consume(object e, BasicDeliverEventArgs ea)
        {
            var body = ea.Body.ToArray();
            var msg = Encoding.UTF8.GetString(body);

            _logger.LogInformation($" [x] Received: {ea.Exchange} - {ea.RoutingKey} - {msg}");

            _ = WriteMessageToFileAsync(_targetFile, $"{DateTime.UtcNow:O} Topic {ea.RoutingKey}: {msg}\n");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting worker..");

            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(_rabbitConfig.RabbitExchange, "topic");

            var queue = channel.QueueDeclare();

            foreach (var topic in _rabbitConfig.RabbitTopics)
            {
                channel.QueueBind(queue, _rabbitConfig.RabbitExchange, topic);
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consume;

            channel.BasicConsume(queue, false, consumer);

            _logger.LogInformation("Consumer initialized.");

            _logger.LogInformation(" [*] Waiting for messages. To exit press CTRL+C");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(-1, stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
