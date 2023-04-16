using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.Models;

public class Method
{
    [Key]
    public long? ID { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }

    public required ProcessingType Type { get; set; }
}
