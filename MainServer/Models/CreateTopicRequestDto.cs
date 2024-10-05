namespace MainServer.Models;

public class CreateTopicRequestDto
{
    public required string Title { get; set; }
    public required string[] Points { get; set; }

}