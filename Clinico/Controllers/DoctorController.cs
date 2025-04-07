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

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);

            if (doctor == null) return NotFound();

            return Ok(doctor);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetDoctorList() {
            List<Doctor> list = await _service.GetDoctorList();

            if (list == null) return NotFound();

            return Ok(list);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> NewDoctor(Doctor doctor) {
            if (doctor == null) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            await _service.CreateDoctor(doctor);
            return Ok();
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditDoctor(int id, Doctor doctor) {
            if (id != doctor.Id || !ModelState.IsValid) return BadRequest();
            await _service.UpdateDoctor(doctor);          

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult>  DeleteDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);

            if (doctor == null) return NotFound();

            return Ok();
        }
    }
}
