using System.ComponentModel.DataAnnotations;

namespace AiBenchmarkAPI.Dtos;

public class PatchSubjectDto
{
    [MinLength(2)]
    [StringLength(100)]
    public string? Name { get; set; } = "";
    [StringLength(500)]
    public string? Description { get; set; }
}