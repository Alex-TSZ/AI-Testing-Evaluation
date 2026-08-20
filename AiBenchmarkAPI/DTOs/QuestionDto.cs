using AiBenchmarkAPI.Models;

namespace AiBenchmarkAPI.Dtos;
public class QuestionDto
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public QuestionType QuestionType { get; set; }
    public int EstimatedDifficulty { get; set; }
    public int? TestedDifficulty { get; set; }
    public string Prompt { get; set; } = "";
}