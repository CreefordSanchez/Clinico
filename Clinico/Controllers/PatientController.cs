using AutoMapper;
using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;

namespace Clinico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;
        private readonly IMapper _patientMapper;

        public PatientsController(PatientService patientService, IMapper patientMapper)
        {
            _patientService = patientService;
            _patientMapper = patientMapper;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAllPatients()
        {
            List<Patient> patients = await _patientService.GetPatientsAsync();
            if (patients == null) return NotFound();
            List<PatientDTO> patientsDTO = _patientMapper.Map<List<PatientDTO>>(patients);
            return Ok(patientsDTO);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            Patient patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult> CreatePatient(PatientDTO patientDTO)
        {
            if(patientDTO.Name == null || patientDTO.Email == null || patientDTO.Address == null
                || patientDTO.PhoneNumber == null)
            {
                return BadRequest("Patient details cannot be null.");
            }
            Patient patient = _patientMapper.Map<Patient>(patientDTO);
            try
            {
                await _patientService.AddPatientAsync(patient);
                return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(int id, PatientDTO patientDTO)
        {
            if (id < 0 || patientDTO.Name == null || patientDTO.Email == null || patientDTO.Address == null
                || patientDTO.PhoneNumber == null)
            {
                return BadRequest("Patient details cannot be null.");
            }
            try
            {
                if (await _patientService.GetPatientByIdAsync(id) == null)
                    {
                        return NotFound("No patient found by that id.");
                    }
                Patient patientUpdate = _patientMapper.Map<Patient>(patientDTO);
                patientUpdate.Id = id;
                await _patientService.UpdatePatientAsync(patientUpdate);
                return Ok(patientUpdate);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            Patient patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();

            await _patientService.DeletePatientAsync(id);
            return Ok();
        }
    }
}
