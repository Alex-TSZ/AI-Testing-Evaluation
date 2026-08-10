using AiBenchmarkAPI.Models;
using AiBenchmarkAPI.Dtos;

namespace AiBenchmarkAPI.Mappers;

public static class SubjectMapper
{
    public static SubjectDto ToDto(Subject subject)
    {
        return new SubjectDto
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
        };
    }

    public static Subject ToEntity(CreateSubjectDto dto)
    {
        return new Subject
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    public static void UpdateEntity(Subject subject, UpdateSubjectDto dto)
    {
        subject.Name = dto.Name;
        subject.Description = dto.Description;
    }

    public static void PatchEntity(Subject subject, PatchSubjectDto dto)
    {
        if(dto.Name != null)
        {
            subject.Name = dto.Name;
        }
        if(dto.Description != null)
        {
            subject.Description = dto.Description;
        }
    }
}