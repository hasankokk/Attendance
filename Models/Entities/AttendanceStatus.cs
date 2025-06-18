using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementApi.Models.Entities;

public class AttendanceStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
}