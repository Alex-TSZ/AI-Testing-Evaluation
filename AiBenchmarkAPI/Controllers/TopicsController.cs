using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(_context.Topics.ToList());
    }

    [HttpPost]
    public IActionResult CreateTopic(Topic topic)
    {
        _context.Topics.Add(topic);
        _context.SaveChanges();

        return Ok(topic);
    }
}