using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using DeepfakeDetectionFramework.Interfaces;

namespace DeepfakeDetectionFramework.Services;

public class FeedbackService : IFeedbackService
{
    private readonly DatabaseContext _databaseContext;
    private readonly MapperConfig _mapperConfig;

    public FeedbackService(DatabaseContext databaseContext, MapperConfig mapperConfig)
    {
        _databaseContext = databaseContext;
        _mapperConfig = mapperConfig;
    }

    public async Task SaveFeedback(FeedbackVM feedbackVM)
    {
        Feedback feedback = _mapperConfig.ToModel(feedbackVM);
        await _databaseContext.AddAsync(feedback);
        await _databaseContext.SaveChangesAsync();
    }
}
