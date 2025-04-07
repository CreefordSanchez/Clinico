using Microsoft.AspNetCore.Mvc;
using Clinico.BLL;
using Clinico.Model;

namespace Clinico.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ExamRoomController : Controller {
        private readonly ExamRoomService _service;

        public ExamRoomController(ExamRoomService service) {
            _service = service;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetRoom(int id) {
            ExamRoom room = await _service.GetExamRoom(id);

            if (room == null) return NotFound();

            return Ok(room);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetExamRoomList() {
            List<ExamRoom> list = await _service.GetExamRoomsList();

            if (list == null) return NotFound();

            return Ok(list);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditDoctor(int id, ExamRoom room) {
            if (id != room.Id || !ModelState.IsValid) return BadRequest();
            await _service.UpdateExamRoom(room);

            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeletRoom(int id) {
            ExamRoom room = await _service.GetExamRoom(id);

            if (room == null) return NotFound();

            return Ok();
        }
    }
}
