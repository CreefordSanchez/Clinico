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

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamRoom>> GetRoom(int id) {
            ExamRoom room = await _service.GetExamRoom(id);

            if (room == null) return NotFound();

            return Ok(room);
        }

        [HttpGet]
        public async Task<ActionResult<List<ExamRoom>>> GetExamRoomList() {
            List<ExamRoom> list = await _service.GetExamRoomsList();

            if (list == null) return NotFound();

            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditDoctor(int id, ExamRoom room) {
            if (id != room.Id || !ModelState.IsValid) return BadRequest();
            await _service.UpdateExamRoom(room);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletRoom(int id) {
            ExamRoom room = await _service.GetExamRoom(id);

            if (room == null) return NotFound();

            return Ok();
        }
    }
}
