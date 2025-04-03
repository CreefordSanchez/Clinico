using Microsoft.EntityFrameworkCore;
using Clinico.Model;
using Clinico.Models;

namespace Clinico.DAL {
    public class ClinicoContext : DbContext {
        public ClinicoContext(DbContextOptions<ClinicoContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<ExamRoom> ExamRooms { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
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

                entity.HasMany(d => d.Appointments)
                    .WithOne(p => p.Doctor)
                    .HasForeignKey(p => p.DoctorId)
                    .OnDelete(DeleteBehavior.Cascade);//doctor dead so apointment has to be canceled
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
                    .OnDelete(DeleteBehavior.Restrict);
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
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
