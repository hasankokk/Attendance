using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementApi.Models.DTOs.Classroom;

public class ClassroomAddDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; }
}