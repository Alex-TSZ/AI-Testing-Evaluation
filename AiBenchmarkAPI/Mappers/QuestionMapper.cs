using AiBenchmarkAPI.Models;
using AiBenchmarkAPI.Dtos;

namespace AiBenchmarkAPI.Mappers;

public static class QuestionMapper
{
    public static QuestionDto ToDto(Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            TopicId = question.TopicId,
            QuestionType =question.QuestionType,
            EstimatedDifficulty =question.EstimatedDifficulty,
            TestedDifficulty =question.TestedDifficulty,
            Prompt =question.Prompt
        };
    }

    public static Question ToEntity(CreateQuestionDto dto, int topicId)
    {
        return new Question
        {
            TopicId = topicId,
            QuestionType =dto.QuestionType,
            EstimatedDifficulty =dto.EstimatedDifficulty,
            Prompt =dto.Prompt
        };
    }

    public static void UpdateEntity(Question question, UpdateQuestionDto dto)
    {
        question.Prompt = dto.Prompt;
        question.QuestionType = dto.QuestionType;
        question.EstimatedDifficulty = dto.EstimatedDifficulty;
    }
}