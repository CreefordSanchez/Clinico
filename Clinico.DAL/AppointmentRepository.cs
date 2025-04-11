using Clinico.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinico.DAL
{
    public class AppointmentRepository
    {
        private readonly ClinicoContext _context;

        public AppointmentRepository(ClinicoContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.DoctorId)
                .Include(a => a.PatientId)
                .Include(a => a.RoomId)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            Appointment newAppointment = await GetAppointmentByIdAsync(appointment.Id);
            newAppointment.Duration = appointment.Duration;
            newAppointment.ScheduledDate = appointment.ScheduledDate;
            newAppointment.SpecialistType = appointment.SpecialistType;
            newAppointment.DoctorId = appointment.DoctorId;
            newAppointment.PatientId = appointment.PatientId;
            newAppointment.RoomId = appointment.RoomId;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
