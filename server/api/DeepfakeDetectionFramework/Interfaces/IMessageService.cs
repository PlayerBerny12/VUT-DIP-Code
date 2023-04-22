using DeepfakeDetectionFramework.Data.ViewModels;
using RabbitMQ.Client;

namespace DeepfakeDetectionFramework.Interfaces;

public interface IMessageService
{
    public ConnectionFactory GetConnectionFactory();

    public IModel CreateChannel(IConnection connection, string queue);    
}
