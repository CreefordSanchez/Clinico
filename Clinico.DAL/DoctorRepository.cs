using Clinico.Model;

namespace Clinico.DAL {
    public class DoctorRepository {
        private readonly ClinicoContext _context;
        public DoctorRepository(ClinicoContext context) {
            _context = context;
        }

        public async Task CreateDoctor(Doctor doctor) {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public async Task RemoveDoctor(int id) {
            Doctor doctor = await GetDoctor(id);
            _context.Remove(doctor);
            _context.SaveChanges();
        }

        public async Task UpdateDoctor(Doctor doctor) {
            Doctor Newdoctor = await GetDoctor(doctor.Id);
            Newdoctor.Name = doctor.Name;
            Newdoctor.Email = doctor.Email;
            Newdoctor.Address = doctor.Address;
            Newdoctor.PhoneNumber = doctor.PhoneNumber;
            Newdoctor.Specialty = doctor.Specialty;

            _context.Update(Newdoctor);
            _context.SaveChanges();
        }

        public async Task<Doctor> GetDoctor(int id) {
            return _context.Doctors.Find(id);
        }

        public async Task<List<Doctor>> GetDoctorList() {
            return _context.Doctors.ToList();
        }
    }
}