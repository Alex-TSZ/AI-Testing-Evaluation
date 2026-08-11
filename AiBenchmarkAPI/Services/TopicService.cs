using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AiBenchmarkAPI.Services;
public class TopicService
{
    private readonly ApplicationDbContext _context;

    public TopicService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TopicDto?> CreateAsync(int subjectId, CreateTopicDto dto)
    {
        var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == subjectId);

        if (!subjectExists)
        {
            return null;
        }

        var topic = TopicMapper.ToEntity(dto, subjectId);
        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();
        return TopicMapper.ToDto(topic);
    }
    /*
    public async Task<SubjectDto?> GetByIdAsync(int id)
    {
        var subject = await _context.Subjects.Include(s => s.Topics).FirstOrDefaultAsync(s => s.Id == id);
        
        if (subject == null)
        {
            return null;
        }
        return SubjectMapper.ToDto(subject);
    }

    public async Task<List<SubjectDto>> GetAllAsync()
    {
        var subject = await _context.Subjects.Include(s => s.Topics).ToListAsync();

        return subject.Select(SubjectMapper.ToDto).ToList();
    }

    public async Task<SubjectDto?> UpdateAsync(int id, UpdateSubjectDto dto)
    {
        var subject = await _context.Subjects.FindAsync(id);
        
        if (subject == null)
        {
            return null;
        }

        SubjectMapper.UpdateEntity(subject, dto);
        await _context.SaveChangesAsync();
        return SubjectMapper.ToDto(subject);
    }

    public async Task<SubjectDto?> PatchAsync(int id, PatchSubjectDto dto)
    {
        var subject = await _context.Subjects.FindAsync(id);
        
        if (subject == null)
        {
            return null;
        }

        SubjectMapper.PatchEntity(subject, dto);
        await _context.SaveChangesAsync();
        return SubjectMapper.ToDto(subject);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var subject = await _context.Subjects.FindAsync(id);
        
        if (subject == null)
        {
            return false;
        }
        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();
        return true;
    }
    */
}