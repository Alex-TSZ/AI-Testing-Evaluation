using AiBenchmarkAPI.Data;
using AiBenchmarkAPI.Dtos;
using AiBenchmarkAPI.Mappers;

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
}