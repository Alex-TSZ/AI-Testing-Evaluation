namespace AiBenchmarkAPI.Dtos;
public class SubjectDto
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string? Description { get; set; }

    public List<TopicDto> Topics { get; set; } = new();
}