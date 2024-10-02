namespace MainServer.Controllers;

public class CreateCategoryRequestDTO
{
    public required string Title { get; set; }
    public required string[] Points { get; set; }

}