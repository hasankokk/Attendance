using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Data;
using AttendanceManagementApi.Helpers;
using AttendanceManagementApi.Models.DTOs.Lesson;
using AttendanceManagementApi.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AttendanceManagementApi.Endpoints;

public static class LessonEndpoints
{
    public static void RegisterLessonEndpoints(this WebApplication app)
    {
        var lessons = app.MapGroup("lessons");
        lessons.MapGet("", GetLessons);
        lessons.MapPost("", AddLesson);
        lessons.MapPost("", AddLessons).WithDescription("Birden fazla ders eklemek için kullanılır.");
        lessons.MapPut("/{id:int}", UpdateLesson);
        lessons.MapDelete("/{id:int}", DeleteLesson);
    }

    static Ok<LessonDto[]> GetLessons(AppDbContext db) =>
        TypedResults.Ok(db.Lessons.Adapt<LessonDto[]>().ToArray());

    static Created<LessonDto[]> AddLessons(AppDbContext db, LessonAddDto[] newLessons)
    {
        // validasyon patlarsa hangisinde patladığını söylemem lazım galiba
        // bir de valid olanları ne yapcam? onlar ok ama diğerleri patladı mı dicem
        
        var lessons = newLessons.Adapt<Lesson[]>();
        db.Lessons.AddRange(lessons);
        db.SaveChanges();
        
        return TypedResults.Created("/lessons", lessons.Adapt<LessonDto[]>());
    }

    static Results<Accepted, NotFound<string>, BadRequest<List<ValidationResult>>> UpdateLesson(AppDbContext db, int id, UpdateLessonDto lessonUpdateDto)
    {
        //if(!ValidationHelper.ValidateModel(lessonUpdateDto, out var errors))
        //    return TypedResults.BadRequest(errors);
        if (lessonUpdateDto == null)
            return TypedResults.NotFound("Güncellenecek bilgiler eksik!");
        
        var lesson = db.Lessons.Find(id);
        if (lesson == null)
            return TypedResults.NotFound($"{id}, ders bulunamadı.");
        
        if (!db.Classrooms.Any(c => c.Id == lesson.ClassroomId))
            return TypedResults.NotFound($"{lessonUpdateDto.ClassroomId}, bulunamadı.");
        if (db.Classrooms.Any(c => c.Lessons.Any(l => l.Name == lessonUpdateDto.Name)))
            return TypedResults.BadRequest(new List<ValidationResult>
            {
                new ("Aynı dersten iki tane olamaz.", new[] { "Name" })
            });

        TypeAdapterConfig<UpdateLessonDto, Lesson>
            .NewConfig()
            .IgnoreNullValues(true);
        lessonUpdateDto.Adapt(lesson);
        lesson.Updated = DateTime.Now;
        db.SaveChanges();
        
        return TypedResults.Accepted("/lessons/{id:int}");
    }
    static Results<NoContent, BadRequest<List<ValidationResult>>, NotFound<string>> AddLesson(AppDbContext db, LessonAddDto newLesson)
    {
        if (!ValidationHelper.ValidateModel(newLesson, out var validationResults))
        {
            return TypedResults.BadRequest(validationResults);
        }
        // model var mı? -> bad request - formatı lazım
        // model valid mi? -> bad request - formatı lazım
        // istersem ikisinin badrequest aynı olur
        
        // böyle bir classroom var mı? -> NotFound
        // enum -> errorCode.RelatedEntityNotFound
        // eğer classroom'u bulup, daha sonra kullanacaksam is pattern'i olur ama var mı yok mu kontrolü için null check
        // if (db.Classrooms.Find(newLesson.ClassroomId) is not Classroom classroom)
        if (db.Classrooms.Find(newLesson.ClassroomId) == null)
        {
            return TypedResults.NotFound($"{newLesson.ClassroomId} sınıfı bulunamadı.");
        }
        
        // Mapster'ın daha popüler alternatifi AutoMapper
        var lesson = newLesson.Adapt<Lesson>();
        db.Lessons.Add(lesson);
        db.SaveChanges();
        return TypedResults.NoContent();
    }

    static Results<Ok<string>, NotFound<string>> DeleteLesson(AppDbContext db, int id)
    {
        var lesson = db.Lessons.Find(id);
        if (lesson == null)
            return TypedResults.NotFound("Ders bulunamadı!");
        db.Lessons.Remove(lesson);
        db.SaveChanges();
        return TypedResults.Ok("Başarıyla silindi");
    }
}