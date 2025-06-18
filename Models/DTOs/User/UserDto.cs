using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Models.Entities;

namespace AttendanceManagementApi.Models.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string EmailAddress { get; set; }
    [MaxLength(100)]
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}