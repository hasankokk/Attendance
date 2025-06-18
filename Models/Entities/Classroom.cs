using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementApi.Models.Entities;

public class Classroom
{
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    // bu sınıftaki kullanıcıların rolleri uyguluma içinde yönetiliyor
    public ICollection<User> Users { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
}

// her sınıfın bir sürü öğrencisi olabilir
// bir öğrenci birden fazla sınıfta olabilir
// aynı durum öğretmen için de geçerli