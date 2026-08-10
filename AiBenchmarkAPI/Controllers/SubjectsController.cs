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
        var subjects = _subjectService.GetAll();
        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public IActionResult GetSubject(int id)
    {
        var subject = _subjectService.GetById(id);
        if (subject == null)
        {
            return NotFound();
        }
        return Ok(subject);
    }

    [HttpPost]
    public IActionResult CreateSubject(CreateSubjectDto dto)
    {
        var response = _subjectService.Create(dto);
        
        return CreatedAtAction(
            nameof(GetSubject),
            new { id = response.Id },
            response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSubject(int id)
    {
        var deleted = _subjectService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSubject(int id, UpdateSubjectDto dto)
    {
        var subject = _subjectService.Update(id, dto);

        if (subject == null)
        {
            return NotFound();
        }

        return Ok(subject);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchSubject(int id, PatchSubjectDto dto)
    {
        var subject = _subjectService.Patch(id, dto);

        if (subject == null)
        {
            return NotFound();
        }
        return Ok(subject);
    }
}