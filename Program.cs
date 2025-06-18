using System.Globalization;using AttendanceManagementApi.Data;
using AttendanceManagementApi.Endpoints;
using AttendanceManagementApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

#region builder configs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();
#endregion

#region app configs
var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.RegisterEndpoints();

#endregion

app.Run();