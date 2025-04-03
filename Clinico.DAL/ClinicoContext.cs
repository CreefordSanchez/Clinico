using Microsoft.EntityFrameworkCore;
using Clinico.Model;

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

            builder.Entity<ExamRoom>(entity =>
            {
                entity.HasKey(er => er.Id);

                entity.Property(er => er.Type).IsRequired();
            });

            builder.Entity<Appointment>(entity =>
            {

            });

            builder.Entity<Patient>(entity =>
            {

            });
        }
    }
}
