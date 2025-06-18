using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementApi.Models.DTOs.User;

[Index(nameof(EmailAddress), IsUnique = true)]
public class CreateUserDto
{
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100), EmailAddress]
    public string EmailAddress { get; set; }
    [MaxLength(100)]
    public string PhoneNumber { get; set; }
    public Role Role { get; set; } = Role.Student;
}