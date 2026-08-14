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

    [HttpGet()]
    public async Task<IActionResult> GetTopics(int subjectId)
    {
        var topics = await _topicService.GetBySubjectAsync(subjectId);
        if(topics == null)
        {
            return NotFound($"Subject {subjectId} was not found.");
        }
        return Ok(topics);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTopic(int subjectId, int id)
    {
        var topic = await _topicService.GetByIdAsync(subjectId, id);
        if(topic == null)
        {
            return NotFound($"Topic {id} was not found or is not apart of subjects {subjectId} topics.");
        }
        return Ok(topic);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTopic(int subjectId, int id, UpdateTopicDto dto)
    {
        var topic = await _topicService.UpdateAsync(subjectId,  id, dto);
        if(topic == null)
        {
            return NotFound($"Topic {id} was not found or  not apart of subject {subjectId}");
        }
        return Ok(topic);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTopic(int subjectId, int id)
    {
        var deleted = await _topicService.DeleteAsync(subjectId,  id);
        if (!deleted)
        {
            return NotFound($"Task {id} was not found in subject {subjectId}");
        }
        return NoContent();
    }
}