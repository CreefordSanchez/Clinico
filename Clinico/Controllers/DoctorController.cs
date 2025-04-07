using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;

namespace Clinico.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller {
        private readonly DoctorService _service;

        public DoctorController(DoctorService service) {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);

            if (doctor == null) return NotFound();

            return Ok(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctorList() {
            List<Doctor> list = await _service.GetDoctorList();

            if (list == null) return NotFound();

            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult> NewDoctor(Doctor doctor) {
            if (doctor == null) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            await _service.CreateDoctor(doctor);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditDoctor(int id, Doctor doctor) {
            if (id != doctor.Id || !ModelState.IsValid) return BadRequest();
            await _service.UpdateDoctor(doctor);          

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult>  DeleteDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);

            if (doctor == null) return NotFound();

            return Ok();
        }
    }
}
