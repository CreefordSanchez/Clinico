using Clinico.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinico.Model;

namespace Clinico.BLL
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly ExamRoomRepository _roomRepository;

        public AppointmentService(
            AppointmentRepository appointmentRepository, 
            PatientRepository patientRepository, 
            DoctorRepository doctorRepository, 
            ExamRoomRepository roomRepository)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
        }
        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }
        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }
        public async Task AddAppointmentAsync(Appointment appointment)
        {
            Doctor doctor = await _doctorRepository.GetDoctor(appointment.DoctorId);
            Patient patient = await _patientRepository.GetPatientByIdAsync(appointment.PatientId);
            ExamRoom room = await _roomRepository.GetExamRoom(appointment.RoomId);

            await _appointmentRepository.AddAppointmentAsync(appointment);
        }
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            if (appointment != null)
            {
                await _appointmentRepository.UpdateAppointmentAsync(appointment);
            }
        }
        public async Task DeleteAppointmentAsync(int id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
        }
    }
}
