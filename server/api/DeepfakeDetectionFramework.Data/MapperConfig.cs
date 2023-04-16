using DeepfakeDetectionFramework.Data.Models;
using DeepfakeDetectionFramework.Data.ViewModels;
using Riok.Mapperly.Abstractions;

namespace DeepfakeDetectionFramework.Data
{
    [Mapper]
    public partial class MapperConfig
    {
        public partial MethodVM ToViewModel(Method method);
        public partial RequestVM ToViewModel(Request request);
        public partial ResponseVM ToViewModel(Response response);

        public partial Method ToModel(MethodVM methodVM);
        public partial Request ToModel(RequestVM requestVM);
        public partial Response ToModel(ResponseVM responseVM);
    }
}
