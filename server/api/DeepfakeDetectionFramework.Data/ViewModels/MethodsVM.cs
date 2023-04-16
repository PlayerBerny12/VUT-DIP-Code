namespace DeepfakeDetectionFramework.Data.ViewModels;

public class MethodVM
{
    public long? ID { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required ProcessingType Type { get; set; }
}
