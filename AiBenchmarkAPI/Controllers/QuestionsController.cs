using Microsoft.AspNetCore.Mvc;
using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Models;
using Microsoft.EntityFrameworkCore;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Mappers;
using AiBenchmarkAPI.Services;

namespace AiBenchmarkAPI.Controllers;

[ApiController]
[Route("api/topics/{topicId}/questions")]
public class QuestionsController : ControllerBase
{
    private readonly QuestionService _questionService;

    public QuestionsController(QuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuestion(int topicId, CreateQuestionDto dto)
    {
        var question = await _questionService.CreateAsync(topicId, dto);

        if (question == null)
        {
            return NotFound($"Topic {topicId} was not found.");
        }

        return Ok(question);
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions(int topicId)
    {
        var questions = await _questionService.GetByTopicAsync(topicId);
        if (questions == null)
        {
            return NotFound($"Questions for topic {topicId} was not found");
        }
        return Ok(questions);
    }

    [HttpGet("{quesitonId}")]
    public async Task<IActionResult> GetQuestion(int topicId, int quesitonId)
    {
        var question = await _questionService.GetByIdAsync(topicId, quesitonId);
        if (question == null)
        {
            return NotFound($"Question {quesitonId} was not found under Topic {topicId}");
        }
        return Ok(question);
    }

    [HttpPut("{questionId}")]
    public async Task<IActionResult> UpdateQuestion(int topicId, int questionId, UpdateQuestionDto dto)
    {
        var question = await _questionService.UpdateAsync(topicId, questionId, dto);
        if(question == null)
        {
            return NotFound($"Question {questionId} was not found to update under topic {topicId}");
        }
        return Ok(question);
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> DeleteQuestion(int topicId, int questionId)
    {
        var question = await _questionService.DeleteAsync(topicId, questionId);
        if(question)
        {
            return NotFound($"Question {questionId} was deleted from topic {topicId}");
        }
        return NoContent();
    }
}