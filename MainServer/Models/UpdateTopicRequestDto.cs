using System.ComponentModel.DataAnnotations;

namespace MainServer.Models;

public class UpdateTopicRequestDto
{
    [Required]
    public required string Title { get; set; }
    public required string[] Points { get; set; }
}