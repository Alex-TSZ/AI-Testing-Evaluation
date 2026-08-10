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
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
    {
        var subjects = await _subjectService.GetAllAsync();
        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubject(int id)
    {
        var subject = await _subjectService.GetByIdAsync(id);
        if (subject == null)
        {
            return NotFound();
        }
        return Ok(subject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubject(CreateSubjectDto dto)
    {
        var response = await _subjectService.CreateAsync(dto);
        if(response == null)
        {
            return Conflict("A subject with this name already exist.");
        }
        return CreatedAtAction(
            nameof(GetSubject),
            new { id = response.Id },
            response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        var deleted = await _subjectService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto dto)
    {
        var subject = await _subjectService.UpdateAsync(id, dto);

        if (subject == null)
        {
            return NotFound();
        }

        return Ok(subject);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchSubject(int id, PatchSubjectDto dto)
    {
        var subject = await _subjectService.PatchAsync(id, dto);

        if (subject == null)
        {
            return NotFound();
        }
        return Ok(subject);
    }
}