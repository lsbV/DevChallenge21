using System.ComponentModel.DataAnnotations;
using Core;
using Database.DbModels;

namespace Database;

public class DbCall
{
    public required int Id { get; set; }
    [Required]
    [MaxLength(5000)]
    public required string Transcription { get; set; }
    [Required]
    public required HashSet<string> People { get; set; }
    [Required]
    public required HashSet<string> Locations { get; set; }
    [Required]
    public required EmotionalTone Tone { get; set; }

    public required Status Status { get; set; }
    public virtual required ICollection<DbTopic> Topics { get; set; }
}