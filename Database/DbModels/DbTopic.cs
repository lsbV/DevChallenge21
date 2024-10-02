using System.ComponentModel.DataAnnotations;

namespace Database.DbModels;

public class DbTopic
{
    public required int Id { get; set; }
    [Required]
    [MaxLength(500)]
    public required string Title { get; set; }
    [Required]
    public required HashSet<string> Points { get; set; }
    [Required]
    public required int CallId { get; set; }
}