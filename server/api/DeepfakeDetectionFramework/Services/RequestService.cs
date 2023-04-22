using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Interfaces;
using RabbitMQ.Client;
using System.Text.Json;

namespace DeepfakeDetectionFramework.Services;

public class RequestService : IRequestService
{
    private readonly DatabaseContext _databaseContext;
    private readonly MapperConfig _mapperConfig;
    private readonly IMessageService _messageService;

    const string QUEUE_VIDEO = "queue_video";
    const string QUEUE_IMAGE = "queue_image";
    const string QUEUE_AUDIO = "queue_audio";

    public RequestService(DatabaseContext databaseContext, MapperConfig mapperConfig, IMessageService messageService)
    {
        _databaseContext = databaseContext;
        _mapperConfig = mapperConfig;
        _messageService = messageService;
    }

    public async Task<RequestVM> CreateRequest(string filename, string checksum, ProcessingType processingType)
    {
        Request request = new()
        {
            Filename = filename,
            Checksum = checksum,
            Status = RequestStatus.Processing,
            Type = processingType
        };

        await _databaseContext.AddAsync(request);
        await _databaseContext.SaveChangesAsync();

        return _mapperConfig.ToViewModel(request);
    }
    public void SendRequestToProcessingUint(RequestVM request)
    {
        byte[] message = JsonSerializer.SerializeToUtf8Bytes(request);
        using IConnection connection = _messageService.GetConnectionFactory()
            .CreateConnection();

        if (request.Type == ProcessingType.Video)
        {
            using IModel channel = _messageService.CreateChannel(connection, QUEUE_VIDEO);
            channel.BasicPublish("", QUEUE_VIDEO, null, message);
        }
        else if (request.Type == ProcessingType.Image)
        {
            using IModel channel = _messageService.CreateChannel(connection, QUEUE_IMAGE);
            channel.BasicPublish("", QUEUE_IMAGE, null, message);
        }
        else if (request.Type == ProcessingType.Audio)
        {
            using IModel channel = _messageService.CreateChannel(connection, QUEUE_AUDIO);
            channel.BasicPublish("", QUEUE_AUDIO, null, message);
        }
    }
}
