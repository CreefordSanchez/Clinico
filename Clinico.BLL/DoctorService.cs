using Clinico.DAL;
using Clinico.Model;

namespace Clinico.BLL {
    public class DoctorService {
        private readonly DoctorRepository _repository;

        public DoctorService(DoctorRepository repository) {
            _repository = repository;
        }
        public void CreateDoctor(Doctor doctor) {
            if (doctor != null) {
                _repository.CreateDoctor(doctor);
            }
        }
        public void RemoveDoctor(Doctor doctor) {
            if (doctor != null) {
                _repository.RemoveDoctor(doctor);
            }

        }

        public void UpdateDoctor(Doctor doctor) {
            if (doctor != null) {
                _repository.UpdateDoctor(doctor);
            }
        }

        public void DeleteDoctor(Doctor doctor) {
            if (doctor != null) {
                _repository.RemoveDoctor(doctor);
            }
        }

        public Doctor GetDoctor(int id) {
            if (id != null) {
                return _repository.GetDoctor(id);
            }

            return new Doctor();
        }

        public List<Doctor> GetDoctorList() {
            List<Doctor> list = _repository.GetDoctorList();

            if (list != null) {
                return list;
            }

            return new List<Doctor>();
        }
    }
}