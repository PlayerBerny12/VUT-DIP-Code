using DeepfakeDetectionFramework.Data.ViewModels;

namespace DeepfakeDetectionFramework.Interfaces;

public interface IMessageService
{
    public void SendRequestToProcessingUint(RequestVM request);
}
