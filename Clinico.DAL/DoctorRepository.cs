using Clinico.Model;
using AutoMapper;
using System.Collections.Generic;

namespace Clinico.DAL {
    public class DoctorRepository {
        private readonly ClinicoContext _context;
        private readonly IMapper _mapper;
        public DoctorRepository(ClinicoContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateDoctor(Doctor doctor) {
            _context.Doctors.Add(doctor);
             await _context.SaveChangesAsync();
        }

        public async Task RemoveDoctor(int id) {
            Doctor doctor = await GetDoctor(id);
            _context.Remove(doctor);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctor(Doctor doctor) {
            Doctor Newdoctor = await GetDoctor(doctor.Id);
            Newdoctor.Name = doctor.Name;
            Newdoctor.Email = doctor.Email;
            Newdoctor.Address = doctor.Address;
            Newdoctor.PhoneNumber = doctor.PhoneNumber;
            Newdoctor.Specialty = doctor.Specialty;
             
            _context.Update(Newdoctor);
             await _context.SaveChangesAsync();
        }

        public async Task<Doctor> GetDoctor(int id) {
            return _context.Doctors.Find(id);
        }

        public async Task<List<DoctorListDTO>> GetDoctorList() {
            List<Doctor> list = _context.Doctors.ToList();
            List<DoctorListDTO> listDTO = _mapper.Map<List<DoctorListDTO>>(list);
            return listDTO;
        }
    }
}