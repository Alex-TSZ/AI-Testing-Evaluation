using Microsoft.AspNetCore.Mvc;
using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Models;

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
        var subjects = _context.Subjects.ToList();
        return Ok(subjects);
    }

    [HttpPost]
    public IActionResult CreateSubject(Subject subject)
    {
        _context.Subjects.Add(subject);
        _context.SaveChanges();
        
        return Ok(subject);
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
    public IActionResult UpdateSubject(int id, Subject updatedSubject)
    {
        var subject = _context.Subjects.Find(id);

        if (subject == null)
        {
            return NotFound();
        }

        subject.Name = updatedSubject.Name;
        subject.Description = updatedSubject.Description;

        _context.SaveChanges();

        return Ok(subject);
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