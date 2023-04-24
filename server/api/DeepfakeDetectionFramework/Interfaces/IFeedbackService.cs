using DeepfakeDetectionFramework.Data.ViewModels;

namespace DeepfakeDetectionFramework.Interfaces;

public interface IFeedbackService
{
    Task SaveFeedback(FeedbackVM feedbackVM);
}
