using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Models.Entities;

namespace AttendanceManagementApi.Models.DTOs.User;

public class UpdateUserDto
{
    [MaxLength(100)]
    public string? FirstName { get; set; }
    [MaxLength(100)]
    public string? LastName { get; set; }
    [MaxLength(100), EmailAddress]
    public string? EmailAddress { get; set; }
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
    public Role? Role { get; set; }
    public bool AddClassroom {get; set;} = true;
    public ICollection<int>? ClassroomId { get; set; }
}