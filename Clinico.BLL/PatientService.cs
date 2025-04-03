using Clinico.Models;
using Clinico.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinico.BLL
{
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<Patient>> GetPatientsAsync()
        {
            return await _patientRepository.GetAllPatientsAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetPatientByIdAsync(id);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name))
            {
                throw new ArgumentException("Patient name cannot be empty or null");
            }
            if (string.IsNullOrWhiteSpace(patient.Email))
            {
                throw new ArgumentException("Patient email cannot be empty or null");
            }
            if (string.IsNullOrWhiteSpace(patient.Address))
            {
                throw new ArgumentException("Patient address cannot be empty or null");
            }
            if (string.IsNullOrWhiteSpace(patient.PhoneNumber))
            {
                throw new ArgumentException("Patient phone number cannot be empty or null");
            }
            await _patientRepository.AddPatientAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name))
            {
                throw new ArgumentException("Patient name cannot be empty or null");
            }
            if (string.IsNullOrWhiteSpace(patient.Email))
            {
                throw new ArgumentException("Patient email cannot be empty or null");
            }
            if (string.IsNullOrWhiteSpace(patient.Address))
            {
                throw new ArgumentException("Patient address cannot be empty or null");
            }
            if (string.IsNullOrWhiteSpace(patient.PhoneNumber))
            {
                throw new ArgumentException("Patient phone number cannot be empty or null");
            }
            await _patientRepository.UpdatePatientAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _patientRepository.DeletePatientAsync(id);
        }
    }
}
