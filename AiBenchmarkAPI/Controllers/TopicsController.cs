using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Models;

namespace AiBenchmarkAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TopicsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetTopics()
    {
        var topics = _context.Topics
            .Include(t => t.Subject)
            .ToList();

        return Ok(topics);
    }

    [HttpGet("{id}")]
    public IActionResult GetTopic(int id)
    {
        var topic = _context.Topics
            .Include(t => t.Subject)
            .FirstOrDefault(t => t.Id == id);

        if (topic == null)
            return NotFound();

        return Ok(topic);
    }

    [HttpPost]
    public IActionResult CreateTopic(Topic topic)
    {
        var subjectExists = _context.Subjects.Any(s => s.Id == topic.SubjectId);

        if (!subjectExists)
        {
            return BadRequest("Subject does not exist.");
        }

        _context.Topics.Add(topic);
        _context.SaveChanges();

        return Ok(topic);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTopic(int id, Topic updatedTopic)
    {
        var topic = _context.Topics.Find(id);

        if (topic == null)
            return NotFound();

        topic.Name = updatedTopic.Name;
        topic.Description = updatedTopic.Description;
        topic.SubjectId = updatedTopic.SubjectId;

        _context.SaveChanges();

        return Ok(topic);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchTopic(int id, Topic patch)
    {
        var topic = _context.Topics.Find(id);

        if (topic == null)
            return NotFound();

        if (!string.IsNullOrEmpty(patch.Name))
            topic.Name = patch.Name;

        if (!string.IsNullOrEmpty(patch.Description))
            topic.Description = patch.Description;

        if (patch.SubjectId != 0)
            topic.SubjectId = patch.SubjectId;

        _context.SaveChanges();

        return Ok(topic);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTopic(int id)
    {
        var topic = _context.Topics.Find(id);

        if (topic == null)
            return NotFound();

        _context.Topics.Remove(topic);

        _context.SaveChanges();

        return NoContent();
    }
}