using AttendanceManagementApi.Data;
using AttendanceManagementApi.Models.DTOs.User;
using AttendanceManagementApi.Models.Entities;

namespace AttendanceManagementApi.Helpers;

public static class Helper
{
    public static void AddOrDeleteClassrooms(UpdateUserDto userUpdateDto, User user, AppDbContext db)
    {
        var classroomIds = userUpdateDto.ClassroomId.ToList();
        var currentIds = user.Classrooms.Select(c => c.Id).ToList();

        if (userUpdateDto.AddClassroom)
        {
            foreach (var id in classroomIds)
            {
                if (!currentIds.Contains(id))
                {
                    var classroom = db.Classrooms.Find(id);
                    if (classroom is not null)
                        user.Classrooms.Add(classroom);
                }
            }
        }
        else
        {
            var toRemove = user.Classrooms
                .Where(c => classroomIds.Contains(c.Id))
                .ToList();

            foreach (var classroom in toRemove)
                user.Classrooms.Remove(classroom);
        }
    }

}