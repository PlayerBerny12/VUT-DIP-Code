using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Interfaces;
using RabbitMQ.Client;
using System.Text.Json;

namespace DeepfakeDetectionFramework.Services;

public class MessageService : IMessageService
{
    private readonly ConnectionFactory connectionFactory;

    public MessageService(IConfiguration configuration)
    {
        connectionFactory = new()
        {
            HostName = configuration["ConnectionStrings:RabbitMQ"]
        };
    }

    private IModel CreateChannel(IConnection connection, string queue)
    {
        IModel channel = connection.CreateModel();

        channel.QueueDeclare(queue, false, false, false);

        return channel;
    }
    public void SendRequestToProcessingUint(RequestVM request)
    {
        byte[] message = JsonSerializer.SerializeToUtf8Bytes(request);
        using IConnection connection = connectionFactory.CreateConnection();

        if (request.Type == ProcessingType.Video)
        {
            string queue = "queue_video";
            using IModel channel = CreateChannel(connection, queue);
            channel.BasicPublish("", queue, null, message);
        }
        else if (request.Type == ProcessingType.Image)
        {
            string queue = "queue_iamge";
            using IModel channel = CreateChannel(connection, queue);
            channel.BasicPublish("", queue, null, message);
        }
        else if (request.Type == ProcessingType.Audio)
        {
            string queue = "queue_audio";
            using IModel channel = CreateChannel(connection, queue);
            channel.BasicPublish("", queue, null, message);
        }
    }
}
