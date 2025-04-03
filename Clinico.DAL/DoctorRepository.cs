using Clinico.Model;

namespace Clinico.DAL {
    public class DoctorRepository {
        private readonly ClinicoContext _context;
        public DoctorRepository(ClinicoContext context) {
            _context = context;
        }

        public void CreateDoctor(Doctor doctor) {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void RemoveDoctor(Doctor doctor) {
            _context.Remove(doctor);
            _context.SaveChanges();
        }

        public void UpdateDoctor(Doctor doctor) {
            _context.Update(doctor);
            _context.SaveChanges();
        }

        public void DeleteDoctor(Doctor doctor) {
            _context.Remove(doctor);
            _context.SaveChanges();
        }

        public Doctor GetDoctor(int id) {
            return _context.Doctors.Find(id);
        }

        public List<Doctor> GetDoctorList() {
            return _context.Doctors.ToList();
        }
    }
}