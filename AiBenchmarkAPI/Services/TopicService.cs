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
    
    public async Task<List<TopicDto>?> GetBySubjectAsync(int subjectId)
    {
        var subjectExists = await _context.Subjects.AnyAsync(s => s.Id == subjectId);
        if (!subjectExists)
        {
            return null;
        }
        var topics = await _context.Topics.Where(t => t.SubjectId == subjectId).ToListAsync();
        return topics.Select(TopicMapper.ToDto).ToList();
    }

    public async Task<TopicDto?> GetByIdAsync(int subjectId, int topicId)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == topicId && t.SubjectId == subjectId);
        if (topic == null)
        {
            return null;
        }
        return TopicMapper.ToDto(topic);
    }

    public async Task<TopicDto?> UpdateAsync(int subjectId, int topicId, UpdateTopicDto dto)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == topicId && t.SubjectId==subjectId);
        
        if (topic == null)
        {
            return null;
        }

        TopicMapper.UpdateEntity(topic, dto);
        await _context.SaveChangesAsync();
        return TopicMapper.ToDto(topic);
    }

    public async Task<bool> DeleteAsync(int subjectId, int topicId)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == topicId && t.SubjectId==subjectId);
        
        if (topic == null)
        {
            return false;
        }
        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync();
        return true;
    }
    /*
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
    */
}