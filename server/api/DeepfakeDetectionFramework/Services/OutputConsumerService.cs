using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using DeepfakeDetectionFramework.Interfaces;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Data;
using Microsoft.AspNetCore.Connections;
using System.Text.Json;
using System.Text;
using DeepfakeDetectionFramework.Data.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DeepfakeDetectionFramework.Services;

public class OutputConsumerService : BackgroundService
{
    private readonly MapperConfig _mapperConfig;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IFileService _fileService;
    private readonly IMessageService _messageService;

    private readonly IConnection _connection;
    private readonly IModel _channel;

    const string QUEUE_OUTPUT = "queue_output";

    public OutputConsumerService(MapperConfig mapperConfig, IServiceScopeFactory scopeFactory,
        IFileService fileService, IMessageService messageService)
    {
        _mapperConfig = mapperConfig;
        _scopeFactory = scopeFactory;
        _fileService = fileService;
        _messageService = messageService;

        _connection = _messageService.GetConnectionFactory()
            .CreateConnection();
        _channel = _messageService.CreateChannel(_connection, QUEUE_OUTPUT);
    }

    protected override Task ExecuteAsync(CancellationToken _)
    {
        EventingBasicConsumer consumer = new(_channel);

        consumer.Received += async (model, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body).Replace('\'', '\"');

            ResponsesBackendVM? responsesVM = null;
            try {
                responsesVM = JsonSerializer.Deserialize<ResponsesBackendVM>(message);
            } catch(Exception ex) {
                Log.Error(ex, "Error during processing occured.");
            }

            if (responsesVM != null)
            {
                using IServiceScope scope = _scopeFactory.CreateScope();
                DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                foreach (ResponseBackendVM responseVM in responsesVM.Responses)
                {
                    Response response = _mapperConfig.ToModel(responseVM);
                    await databaseContext.AddAsync(response);
                }

                // Change request status 
                Request request = await databaseContext.Requests.Where(x => x.ID == responsesVM.RequestID)
                    .FirstAsync();
                request.Status = RequestStatus.Done;

                // Remove file
                _fileService.RemoveFile(request.Filename);
                                
                await databaseContext.SaveChangesAsync();
            }

            _channel.BasicAck(args.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: QUEUE_OUTPUT, autoAck: false, consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
        }
        if (_connection.IsOpen)
        {
            _connection.Close();
        }
    }
}
