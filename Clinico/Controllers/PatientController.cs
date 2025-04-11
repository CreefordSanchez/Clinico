using AutoMapper;
using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            List<Patient> patients = await _patientService.GetPatientsAsync();
            return Ok(patients);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            Patient patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();
            PatientDTO dto = _patientMapper.Map<PatientDTO>(patient); //Patient => PatientDto
            return Ok(dto);
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult> Create(Patient patient)
        {
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
        public async Task<ActionResult> Update(int id, Patient patient)
        {
            if (id != patient.Id)
                return BadRequest("ID mismatch.");

            try
            {
                await _patientService.UpdatePatientAsync(patient);
                return NoContent();
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound();

            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }
    }
}
