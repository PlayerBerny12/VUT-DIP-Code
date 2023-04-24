using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeepfakeDetectionFramework.Data.Models;

public class Response
{
    [Key]
    public long? ID { get; set; }

    public required long RequestID { get; set; }

    [ForeignKey(nameof(RequestID))]
    public Request? Request { get; set; }

    public required long MethodID { get; set; }

    public required double Value { get; set; }
}
