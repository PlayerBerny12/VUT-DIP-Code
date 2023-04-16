using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Interfaces;

namespace DeepfakeDetectionFramework.Services;

public class RequestService : IRequestService
{
    private readonly DatabaseContext _databaseContext;
    private readonly MapperConfig _mapperConfig;
    
    public RequestService(DatabaseContext databaseContext, MapperConfig mapper)
    {
        _databaseContext = databaseContext;
        _mapperConfig = mapper;
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
}
