using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.ViewModels;

public class ResponseVM
{
    public long? ID { get; set; }

    public required RequestVM Request { get; set; }

    public required double Value { get; set; }
}

public class ResponseBackendVM
{
    public long? ID { get; set; }

    public required long RequestID { get; set; }

    public required double Value { get; set; }
    [StringLength(200)]
    public required string Name { get; set; }
    [MaxLength]
    public required string Description { get; set; }
}
