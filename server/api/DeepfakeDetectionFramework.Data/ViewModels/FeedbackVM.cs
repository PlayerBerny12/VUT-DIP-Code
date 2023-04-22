using System.ComponentModel.DataAnnotations;

namespace DeepfakeDetectionFramework.Data.ViewModels;

public class FeedbackVM
{
    public long? RequestID { get; set; }

    [MaxLength]
    public required string Text { get; set; }
}
