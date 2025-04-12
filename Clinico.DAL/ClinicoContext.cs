using Microsoft.EntityFrameworkCore;
using Clinico.Model;
using System.Reflection.Emit;

namespace Clinico.DAL {
    public class ClinicoContext : DbContext {
        public ClinicoContext(DbContextOptions<ClinicoContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<ExamRoom> ExamRooms { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.Name).IsRequired();
                entity.Property(d => d.Email).IsRequired();
                entity.Property(d => d.Address).IsRequired();
                entity.Property(d => d.PhoneNumber).IsRequired();
                entity.Property(d => d.Specialty).IsRequired();

                entity.HasMany(d => d.ExamRooms)
                    .WithOne(er => er.Doctor)
                    .HasForeignKey(er => er.DoctorId)
                    .OnDelete(DeleteBehavior.SetNull);
              });

            builder.Entity<ExamRoom>(entity =>
            {
                entity.HasKey(er => er.Id);

                entity.Property(er => er.Type).IsRequired();
            });

            builder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Duration).IsRequired();
                entity.Property(a => a.ScheduledDate).IsRequired();
                entity.Property(a => a.SpecialistType).IsRequired();

                entity.HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(a => a.DoctorId)
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(a => a.ExamRoom)
                    .WithMany(er => er.Appointments)
                    .HasForeignKey(a => a.RoomId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.Email).IsRequired();
                entity.Property(p => p.Address).IsRequired();
                entity.Property(p => p.PhoneNumber).IsRequired();

                entity.HasMany(p => p.Appointments)
                    .WithOne(a => a.Patient)
                    .HasForeignKey(a => a.PatientId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Seed Doctors
            builder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, Name = "Dr. John Smith", Email = "john@example.com", Address = "123 Main St", PhoneNumber = "1234567890", Specialty = "Cardiology" },
                new Doctor { Id = 2, Name = "Dr. Emily White", Email = "emily@example.com", Address = "456 Health Ave", PhoneNumber = "0987654321", Specialty = "Dermatology" }
            );
             
            // Seed Patients
            builder.Entity<Patient>().HasData(
                new Patient { Id = 1, Name = "Alice Brown", Email = "alice@example.com", Address = "789 Maple Rd", PhoneNumber = "5551234567" },
                new Patient { Id = 2, Name = "Bob Johnson", Email = "bob@example.com", Address = "321 Oak Ln", PhoneNumber = "5559876543" }
            );

            // Seed ExamRooms
            builder.Entity<ExamRoom>().HasData(
                new ExamRoom { Id = 1, Type = "General", DoctorId = 1 },
                new ExamRoom { Id = 2, Type = "Surgical", DoctorId = 2 }
            );

            // Seed Appointments
            builder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Duration = 30,
                    ScheduledDate = new DateTime(2025, 5, 1, 9, 0, 0),
                    SpecialistType = "Cardiology",
                    DoctorId = 1,
                    PatientId = 1,
                    RoomId = 1
                },
                new Appointment
                {
                    Id = 2,
                    Duration = 45,
                    ScheduledDate = new DateTime(2025, 5, 2, 10, 30, 0),
                    SpecialistType = "Dermatology",
                    DoctorId = 2,
                    PatientId = 2,
                    RoomId = 2
                }
            );
        }
    }
}
