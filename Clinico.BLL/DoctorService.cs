using Clinico.DAL;
using Clinico.Model;

namespace Clinico.BLL {
    public class DoctorService {
        private readonly DoctorRepository _repository;
        private readonly AppointmentService _appointmentService;

        public DoctorService(DoctorRepository repository, AppointmentService appointment) {
            _repository = repository;
            _appointmentService = appointment;
        }
        public async Task CreateDoctor(Doctor doctor) {
            if (doctor != null) {
                await _repository.CreateDoctor(doctor);
            }
        }
        public async Task UpdateDoctor(Doctor doctor) {
            if (doctor != null) {
                await _repository.UpdateDoctor(doctor);
            }
        }

        public async Task DeleteDoctor(int id) {
            List<Appointment> list = await _appointmentService.GetAppointmentsAsync();
            List<Appointment> newList = list.Where(a => a.DoctorId == id).ToList();

            foreach (Appointment appointment in newList) {
                await _appointmentService.DeleteAppointmentAsync(appointment.Id);
            }

            await _repository.RemoveDoctor(id);            
        }

        public async Task<Doctor> GetDoctor(int id) {
           return await _repository.GetDoctor(id);
        }

        public async Task<List<DoctorListDTO>> GetDoctorList() {
            return await _repository.GetDoctorList();
        }
    }
}