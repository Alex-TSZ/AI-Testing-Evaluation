using System.Data;

namespace AiBenchmarkAPI.Models;

public class Question
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public Topic? Topic { get; set; }
    //public int? DatasetId { get; set; }
    //public DataSet? DataSet { get; set; }
    public QuestionType QuestionType { get; set; }
    public int EstimatedDifficulty { get; set; }
    public int? TestedDifficulty { get; set; }
    public string Prompt { get; set; }= "";
}