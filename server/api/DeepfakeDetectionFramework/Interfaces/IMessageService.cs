using DeepfakeDetectionFramework.Data.ViewModels;
using RabbitMQ.Client;

namespace DeepfakeDetectionFramework.Interfaces;

public interface IMessageService
{
    ConnectionFactory GetConnectionFactory();

    IModel CreateChannel(IConnection connection, string queue);    
}
