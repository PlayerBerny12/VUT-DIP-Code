using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    public ConnectionFactory GetConnectionFactory() => connectionFactory;
    
    public IModel CreateChannel(IConnection connection, string queue)
    {
        IModel channel = connection.CreateModel();

        channel.QueueDeclare(queue, false, false, false);

        return channel;
    }
}
