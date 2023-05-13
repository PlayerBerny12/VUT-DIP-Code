using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.ViewModels;

namespace DeepfakeDetectionFramework.Interfaces;

public interface IRequestService
{
    Task<RequestVM> CreateRequest(string filename, string checksum, ProcessingType processingType);
    Task SendRequestToProcessingUint(RequestVM request);

    Task<ResponsesVM?> GetRequestResonses(long requestID);
}
