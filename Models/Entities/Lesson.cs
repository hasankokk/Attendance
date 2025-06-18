using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementApi.Models.Entities;

public class Lesson
{
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; }
    public int SortOrder { get; set; }
    // karar konusu:
    // 1 ders 1 yoklama mı alır yoksa 1den fazla yoklama alabilir miyim???
    public ICollection<Attendance> Attendances { get; set; }
    // one-to-one
    // public int AttendanceId { get; set; }
    // public Attendance Attendance { get; set; }
    public int ClassroomId { get; set; }
    public Classroom Classroom { get; set; }
    // set yerine init yaparsak sadece kayıt ilk kez oluşturulduğunda veri girilir
    // mevcut okunan kayıt üzerinde değişiklik yapılamaz
    // çok basit, hata engellemek için yapılabilecek ufak bir uygulama
    public DateTime Created { get; init; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
}

