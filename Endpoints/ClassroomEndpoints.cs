using AttendanceManagementApi.Data;
using AttendanceManagementApi.Models.DTOs.Classroom;
using AttendanceManagementApi.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AttendanceManagementApi.Endpoints;

public static class ClassroomEndpoints
{
    public static void RegisterClassroomEndpoints(this WebApplication app)
    {
        var classrooms = app.MapGroup("classrooms");
        classrooms.MapGet("", GetAllClassrooms);
        classrooms.MapPost("", AddClassroom);
        classrooms.MapPut("/{id:int}", UpdateClassroom);
        classrooms.MapDelete("/{id:int}", DeleteClassroom);
    }

    static Ok<ClassroomDto[]> GetAllClassrooms(AppDbContext db) => 
        TypedResults.Ok(db.Classrooms.Adapt<ClassroomDto[]>().ToArray());

    static Ok<string> AddClassroom(AppDbContext db, ClassroomAddDto newClassroom)
    {
        var classroom = newClassroom.Adapt<Classroom>();
        db.Classrooms.Add(classroom);
        db.SaveChanges();
        return TypedResults.Ok("eklendi");
    }

    static Results<NoContent, NotFound<string>> UpdateClassroom(AppDbContext db, int id, ClassroomAddDto updatedClassroom)
    {
        var classroom = db.Classrooms.Find(id);
        if (classroom == null)
            return TypedResults.NotFound("Sınıf bulunamadı!");
        updatedClassroom.Adapt(classroom);
        classroom.Updated = DateTime.Now;
        db.SaveChanges();
        return TypedResults.NoContent();
    }
    static Results<Ok<string>, NotFound<string>> DeleteClassroom(AppDbContext db, int id)
    {
        var classroom = db.Classrooms.Find(id);
        if (classroom == null)
            TypedResults.NotFound("Sınıf bulunamadı!");
        db.Classrooms.Remove(classroom);
        db.SaveChanges();
        return TypedResults.Ok("Sınıf başarıyla silindi.");
    }
    // static Results<Ok<string>, NotFound> GetClassrooms()
    // {
    //     // type result istiyorsak
    //     // ama anonim obj gönderirsek kabul etmeyecek
    //     // sebebi dokümantasyon oluştururken tamamen ne çıktı olacağını
    //     // blueprint - taslak olarak göstermek istemesi
    //     // eğer sonuç anonim obj olursa bu sefer dokümantasyonda gösterim yapamaz
    //     return TypedResults.Ok("merhaba");
    // }
}

// eğer servis yazıyorsak kebap-case yazmalıyız