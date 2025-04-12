using Clinico.DAL;
using Clinico.Model;

namespace Clinico.BLL {
    public class DoctorService {
        private readonly DoctorRepository _repository;

        public DoctorService(DoctorRepository repository) {
            _repository = repository;
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