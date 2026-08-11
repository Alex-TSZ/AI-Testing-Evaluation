using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Models;
using AiBenchmarkAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AiBenchmarkAPI.Controllers;

[ApiController]
[Route("api/subjects/{subjectId}/topics")]
public class TopicsController : ControllerBase
{
    private readonly TopicService _topicService;

    public TopicsController(TopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet("{id}")]
    public IActionResult GetTopic(int subjectId, int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTopic(int subjectId, CreateTopicDto  dto)
    {
        var response = await _topicService.CreateAsync(subjectId, dto);
        if(response == null)
        {
            return NotFound($"Subject {subjectId} was not found.");
        }
        return CreatedAtAction(nameof(GetTopic), new
        {
            subjectId = response.SubjectId,
            id = response.Id
        },
        response);
    }
}