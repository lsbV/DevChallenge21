namespace MainServer.Models;

public class TopicResponseDto
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string[] Points { get; set; }
}