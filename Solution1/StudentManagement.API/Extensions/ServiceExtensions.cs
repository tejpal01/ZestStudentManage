using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Infrastructure.JWT;
using StudentManagement.Infrastructure.Repositories;

namespace StudentManagement.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // DB Context
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Repository
            services.AddScoped<IStudentRepository, StudentRepository>();

            // Service
            services.AddScoped<IStudentService, StudentService>();

            services.AddScoped<JwtService>();
        }
    }
}
