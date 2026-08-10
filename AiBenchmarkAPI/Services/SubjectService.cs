using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AiBenchmarkAPI.Services;
public class SubjectService
{
    private readonly ApplicationDbContext _context;

    public SubjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public SubjectDto Create(CreateSubjectDto dto)
    {
        var subject = SubjectMapper.ToEntity(dto);
        _context.Subjects.Add(subject);
        _context.SaveChanges();
        return SubjectMapper.ToDto(subject);
    }

    public SubjectDto? GetById(int id)
    {
        var subject = _context.Subjects.Include(s => s.Topics).FirstOrDefault(s => s.Id == id);
        
        if (subject == null)
        {
            return null;
        }
        return SubjectMapper.ToDto(subject);
    }

    public List<SubjectDto> GetAll()
    {
        var subject = _context.Subjects.Include(s => s.Topics).ToList();

        return subject.Select(SubjectMapper.ToDto).ToList();
    }

    public SubjectDto? Update(int id, UpdateSubjectDto dto)
    {
        var subject = _context.Subjects.Find(id);
        
        if (subject == null)
        {
            return null;
        }

        SubjectMapper.UpdateEntity(subject, dto);
        _context.SaveChanges();
        return SubjectMapper.ToDto(subject);
    }

    public SubjectDto? Patch(int id, PatchSubjectDto dto)
    {
        var subject = _context.Subjects.Find(id);
        
        if (subject == null)
        {
            return null;
        }

        SubjectMapper.PatchEntity(subject, dto);
        _context.SaveChanges();
        return SubjectMapper.ToDto(subject);
    }

    public bool Delete(int id)
    {
        var subject = _context.Subjects.Find(id);
        
        if (subject == null)
        {
            return false;
        }
        _context.Subjects.Remove(subject);
        _context.SaveChanges();
        return true;
    }
}