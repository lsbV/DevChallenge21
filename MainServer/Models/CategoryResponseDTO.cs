namespace MainServer.Models;

public class TopicResponseDTO
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string[] Points { get; set; }
}