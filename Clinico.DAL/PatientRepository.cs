using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinico.Model;

namespace Clinico.DAL
{
    public class PatientRepository
    {
        private readonly ClinicoContext _context;

        public PatientRepository(ClinicoContext context)
        {
            _context = context;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            Patient newPatient = await GetPatientByIdAsync(patient.Id);
            newPatient.Name = patient.Name;
            newPatient.Email = patient.Email;
            newPatient.Address = patient.Address;
            newPatient.PhoneNumber = patient.PhoneNumber;
            _context.Patients.Update(newPatient);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
