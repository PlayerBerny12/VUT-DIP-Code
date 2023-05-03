using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.ViewModels;

public class ResponsesVM
{
    public required double? Value { get; set; }
        
    public required List<ResponseVM> Responses { get; set; }
}

public class ResponseVM
{
    public required DetectionMethodVM DetectionMethod { get; set; }
    
    public required double? Value { get; set; }
}

public class ResponsesBackendVM
{
    public required long RequestID { get; set; }

    public required List<ResponseBackendVM> Responses { get; set; }
}


public class ResponseBackendVM
{
    public required long MethodID { get; set; }

    public required long RequestID { get; set; }

    public required double? Value { get; set; }    
}
