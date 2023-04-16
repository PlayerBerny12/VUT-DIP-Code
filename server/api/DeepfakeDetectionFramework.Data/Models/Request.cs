using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.Models;

public class Request
{
    [Key]
    public long? ID { get; set; }

    [StringLength(64)]
    public required string Checksum { get; set; }

    [StringLength(50)]
    public required string Filename { get; set; }

    public required RequestStatus Status { get; set; }

    public required ProcessingType Type { get; set; }
}
