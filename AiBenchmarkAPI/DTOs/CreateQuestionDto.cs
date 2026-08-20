using System.ComponentModel.DataAnnotations;
using AiBenchmarkAPI.Models;
namespace AiBenchmarkAPI.Dtos;

public class CreateQuestionDto
{
    [Required]
    [StringLength(4000)]
    public string Prompt { get; set; } = "";
    [Required]
    public QuestionType QuestionType { get; set; }
    [Range(1,5)]
    public int EstimatedDifficulty { get; set; }
}