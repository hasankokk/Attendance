namespace AttendanceManagementApi.Models.DTOs.Lesson;

public class LessonDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public DateTime Created { get; init; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
}