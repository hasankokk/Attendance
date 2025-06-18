using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementApi.Models.DTOs.Lesson;

public class LessonAddDto
{
    [Required, MaxLength(150)] 
    public string Name { get; set; }
    public int SortOrder { get; set; }
    [Range(1, int.MaxValue)]
    public int ClassroomId { get; set; }
}