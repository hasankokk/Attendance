using System.ComponentModel.DataAnnotations;
using AttendanceManagementApi.Data;
using AttendanceManagementApi.Helpers;
using AttendanceManagementApi.Models.DTOs.User;
using AttendanceManagementApi.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementApi.Endpoints;

public static class UserEndpoints
{
        public static void RegisterUserEndpoints(this WebApplication app)
        {
                var users = app.MapGroup("users");
                users.MapGet("", GetAllUsers).WithDescription("Tüm kullanıcıları listeler.");
                users.MapGet("/{id:int}", GetUser);
                users.MapPost("", AddUser);
                users.MapPut("/{id:int}", UpdateUser);
                users.MapDelete("/{id:int}", DeleteUser);
        }
        static Ok<UserDto[]> GetAllUsers(AppDbContext db) =>
                TypedResults.Ok(db.Users.Adapt<UserDto[]>().ToArray());

        static Results<Ok<UserDto[]>, NotFound<string>> GetUser(AppDbContext db, int id)
        {
                if (!db.Users.Any(x => x.Id == id))
                        return TypedResults.NotFound("Kullanıcı bulunamadı!");
                return TypedResults.Ok(db.Users.Adapt<UserDto[]>().Where(x => x.Id == id).ToArray());
        }

        static Results<Created<UserDto>, BadRequest<List<ValidationResult>>> AddUser(AppDbContext db,
                CreateUserDto userCreateDto)
        {
                if (!ValidationHelper.ValidateModel(userCreateDto, out var validationResults))
                {
                        return TypedResults.BadRequest(validationResults);
                }

                if (userCreateDto.Role != Role.Student && userCreateDto.Role != Role.Teacher)
                {
                        return TypedResults.BadRequest(new List<ValidationResult>
                        {
                                new ("Geçersiz Rol girişi.", new[] { "Role" })
                        });
                }
                if (db.Users.Any(u => u.EmailAddress == userCreateDto.EmailAddress))
                {
                        return TypedResults.BadRequest(new List<ValidationResult>
                        {
                                new("Bu e-posta adresi zaten kayıtlı.", new[] { "EmailAddress" })
                        });
                }

                var user = userCreateDto.Adapt<User>();
                db.Users.Add(user);
                db.SaveChanges();
                return TypedResults.Created($"/users/{user.Id}", user.Adapt<UserDto>());
        }

        static Results<Ok<string>, NotFound<string>, BadRequest<string>> UpdateUser(AppDbContext db, int id, UpdateUserDto userUpdateDto)
        {
                var user = db.Users.Include(u => u.Classrooms).FirstOrDefault(x => x.Id == id);
                if (user == null)
                        return TypedResults.NotFound("Kullanıcı bulunamadı!");
                if (userUpdateDto.Role != null && userUpdateDto.Role != Role.Student && userUpdateDto.Role != Role.Teacher)
                        return TypedResults.BadRequest("Hatalı Role girişi yaptınız!");
               
                TypeAdapterConfig<UpdateUserDto, User>
                        .NewConfig()
                        .Ignore(x => x.Classrooms)
                        .IgnoreNullValues(true);
                
                userUpdateDto.Adapt(user);
                user.Updated = DateTime.Now;
                
                if (userUpdateDto.ClassroomId?.Any() == true)
                        Helper.AddOrDeleteClassrooms(userUpdateDto, user, db);
                
                db.SaveChanges();
                return TypedResults.Ok("Kullanıcı başarıyla güncellendi!");
        }

        static Results<Ok<string>, NotFound<string>> DeleteUser(AppDbContext db, int id)
        {
                var user = db.Users.Find(id);
                if (user == null)
                        return TypedResults.NotFound("Kullanıcı bulunamadı!");
                db.Users.Remove(user);
                db.SaveChanges();
                return TypedResults.Ok("Kullanıcı başarıyla silindi.");
        }
}