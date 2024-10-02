namespace MainServer.Models;

public class CreateTopicRequestDTO
{
    public required string Title { get; set; }
    public required string[] Points { get; set; }

}