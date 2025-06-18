using System.Data;
using AttendanceManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<AttendanceDetail> AttendanceDetails { get; set; }
    public DbSet<AttendanceStatus> AttendanceStatusList { get; set; }
}