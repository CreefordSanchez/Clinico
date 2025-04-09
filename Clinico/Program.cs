using Clinico.DAL;
using Clinico.BLL;
using Microsoft.EntityFrameworkCore;
using Clinico.Model;
using AutoMapper;

namespace Clinico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(MapperProfile)); 


            builder.Services.AddDbContext<ClinicoContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<PatientRepository>();
            builder.Services.AddTransient<ExamRoomRepository>();
            builder.Services.AddTransient<DoctorRepository>();
            builder.Services.AddTransient<AppointmentRepository>();


            builder.Services.AddTransient<PatientService>();
            builder.Services.AddTransient<ExamRoomService>();
            builder.Services.AddTransient<DoctorService>();
            builder.Services.AddTransient<AppointmentService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
