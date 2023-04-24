using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using Riok.Mapperly.Abstractions;

namespace DeepfakeDetectionFramework.Data;

[Mapper]
public partial class MapperConfig
{
    public partial RequestVM ToViewModel(Request request);

    public partial Request ToModel(RequestVM requestVM);
    public partial Response ToModel(ResponseBackendVM responseVM);
    public partial Feedback ToModel(FeedbackVM feedbackVM);            
}
