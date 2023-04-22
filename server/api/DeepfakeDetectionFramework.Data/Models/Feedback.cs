using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.Models;

public class Feedback
{
    [Key]
    public long? ID { get; set; }

    public long? RequestID { get; set; }

    [ForeignKey(nameof(RequestID))]
    public Request? Request { get; set; }

    [MaxLength]
    public required string Text { get; set; }
}
