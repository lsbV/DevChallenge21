using System.ComponentModel.DataAnnotations;

namespace MainServer.Models;

public class CreateCallRequestDto
{
    [Required]
    [Url]
    public required string Audio_Url { get; set; }
}