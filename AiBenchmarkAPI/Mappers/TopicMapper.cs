using AiBenchmarkAPI.Models;
using AiBenchmarkAPI.Dtos;

namespace AiBenchmarkAPI.Mappers;

public static class TopicMapper
{
    public static TopicDto ToDto(Topic topic)
    {
        return new TopicDto
        {
            Id = topic.Id,
            Name = topic.Name,
            Description = topic.Description,
            SubjectId = topic.SubjectId
        };
    }

    public static Topic ToEntity(CreateTopicDto dto, int subjectId)
    {
        return new Topic
        {
            Name = dto.Name,
            Description = dto.Description,
            SubjectId = subjectId
        };
    }

    public static void UpdateEntity(Topic topic, UpdateTopicDto dto)
    {
        topic.Name = dto.Name;
        topic.Description = dto.Description;
    }

    /*
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
    */
}