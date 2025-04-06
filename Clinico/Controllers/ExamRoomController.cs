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
    }
}
