namespace DeepfakeDetectionFramework.Data.ViewModels;

public class ResponseVM
{
    public long? ID { get; set; }

    public required RequestVM Request { get; set; }

    public required double Value { get; set; }

    public required MethodVM Method { get; set; }
}
