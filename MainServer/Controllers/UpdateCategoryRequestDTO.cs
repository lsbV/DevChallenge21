using System.ComponentModel.DataAnnotations;

namespace MainServer.Controllers;

public class UpdateCategoryRequestDTO
{
    [Required]
    public required string Title { get; set; }
    public required string[] Points { get; set; }
}