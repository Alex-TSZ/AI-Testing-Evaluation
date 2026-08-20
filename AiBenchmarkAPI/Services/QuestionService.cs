using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AiBenchmarkAPI.Services;

public class QuestionService
{
    private readonly ApplicationDbContext _context;

    public QuestionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<QuestionDto?> CreateAsync(int topicId, CreateQuestionDto dto)
    {
        var topicExists  = await _context.Topics.AnyAsync(t => t.Id == topicId);
        if (!topicExists)
        {
            return null;
        }
        var question = QuestionMapper.ToEntity(dto, topicId);
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return QuestionMapper.ToDto(question);
    }

    public async Task<List<QuestionDto>?> GetByTopicAsync(int topicId)
    {
        var topicExists = await _context.Topics.AnyAsync(t => t.Id == topicId);
        if (!topicExists)
        {
            return null;
        }
        var question = await _context.Questions.Where(q => q.TopicId == topicId).ToListAsync();
        return question.Select(QuestionMapper.ToDto).ToList();
    }

    public async Task<QuestionDto?> GetByIdAsync(int topicId, int questionId)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId  && q.TopicId == topicId);
        if (question ==null)
        {
            return null;
        }
        return QuestionMapper.ToDto(question);
    }

    public async Task<QuestionDto?> UpdateAsync(int topicId, int questionId, UpdateQuestionDto dto)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId && q.TopicId == topicId);
        if(question == null)
        {
            return null;
        }
        QuestionMapper.UpdateEntity(question,dto);
        await _context.SaveChangesAsync();
        return QuestionMapper.ToDto(question);
    }

    public async Task<bool> DeleteAsync(int topicId, int questionId)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId && q.TopicId == topicId);
        if (question == null)
        {
            return false;
        }
        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();
        return true;
    }
}