using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementApi.Models.DTOs.Lesson;

public class UpdateLessonDto
{
    public string? Name { get; set; }
    public int? SortOrder { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public int ClassroomId { get; set; }
}