using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.ViewModels;

public class DetectionMethodVM
{
    public required long ID { get; set; }

    public required ProcessingType Type { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }
    
    [MaxLength]
    public required string Description { get; set; }
    
    [StringLength(100)]
    public required string TrainingDataset { get; set; }
}
