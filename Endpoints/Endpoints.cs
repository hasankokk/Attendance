namespace AttendanceManagementApi.Endpoints;

public static class Endpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.RegisterClassroomEndpoints();
        app.RegisterLessonEndpoints();
        app.RegisterUserEndpoints();
    } 
}