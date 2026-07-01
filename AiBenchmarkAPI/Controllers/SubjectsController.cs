using Microsoft.AspNetCore.Mvc;
using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Models;
using Microsoft.EntityFrameworkCore;
using AiBenchmarkAPI.Dtos;

namespace AiBenchmarkAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetSubjects()
    {
        var subjects = _context.Subjects.Include(s => s.Topics).Select(subject => new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description,

            Topics = subject.Topics.Select(topic => new TopicDto
            {
                Id = topic.Id,
                Name = topic.Name,
                Description = topic.Description
            }).ToList()
        }).ToList();

        return Ok(subjects);
    }

    [HttpPost]
    public IActionResult CreateSubject(CreateSubjectDto dto)
    {
        var subject = new Subject
        {
            Name = dto.Name,
            Description = dto.Description
        };
        _context.Subjects.Add(subject);
        _context.SaveChanges();
        var response = new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description
        };

        return CreatedAtAction(
            nameof(GetSubjects),
            new { id = subject.Id },
            response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSubject(int id)
    {
        var subject = _context.Subjects.Find(id);

        if (subject == null)
        {
            return NotFound();
        }

        _context.Subjects.Remove(subject);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSubject(int id, UpdateSubjectDto dto)
    {
        var subject = _context.Subjects.Find(id);

        if (subject == null)
        {
            return NotFound();
        }
        subject.Name = dto.Name;
        subject.Description = dto.Description;
        

        _context.SaveChanges();
        var response = new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description
        };
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchSubject(int id, Subject patch)
    {
        var subject = _context.Subjects.Find(id);

        if (subject == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(patch.Name))
        {
            subject.Name = patch.Name;
        }

        if (!string.IsNullOrEmpty(patch.Description))
        {
            subject.Description = patch.Description;
        }

        _context.SaveChanges();

        return Ok(subject);
    }
}