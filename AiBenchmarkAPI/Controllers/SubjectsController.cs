using Microsoft.AspNetCore.Mvc;
using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Models;
using Microsoft.EntityFrameworkCore;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Mappers;
using AiBenchmarkAPI.Services;

namespace AiBenchmarkAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly SubjectService _subjectService;

    public SubjectsController(ApplicationDbContext context, SubjectService subjectService)
    {
        _context = context;
        _subjectService = subjectService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<SubjectDto>> GetSubjects()
    {
        var subjects = _context.Subjects.Include(s => s.Topics).ToList().Select(SubjectMapper.ToDto).ToList();
        return subjects;
    }

    [HttpPost]
    public IActionResult CreateSubject(CreateSubjectDto dto)
    {
        var response = _subjectService.Create(dto);
        
        return CreatedAtAction(
            nameof(GetSubjects),
            new { id = response.Id },
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

        SubjectMapper.UpdateEntity(subject, dto);
        _context.SaveChanges();
        return Ok(SubjectMapper.ToDto(subject));
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