using Microsoft.AspNetCore.Mvc;
using Clinico.BLL;
using Clinico.Model;
using AutoMapper;

namespace Clinico.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ExamRoomController : Controller {
        private readonly ExamRoomService _service;
        private readonly DoctorService _doctorService;
        private readonly IMapper _mapper;


        public ExamRoomController(ExamRoomService service, DoctorService doctorService, IMapper mapper) {
            _service = service;
            _doctorService = doctorService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamRoom>> GetRoom(int id) {
            ExamRoom room = await _service.GetExamRoom(id);

            if (room == null) return NotFound();

            return Ok(room);
        }

        [HttpGet]
        public async Task<ActionResult<List<ExamRoom>>> GetExamRoomList() {
            List<ExamRoomDTO> list = await _service.GetExamRoomsList();

            if (list == null) return NotFound();

            List<ExamRoomDTO> listDTO = _mapper.Map<List<ExamRoomDTO>>(list);

            return Ok(listDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditExamRoom(int id, ExamRoomEditDTO room) {
            if (id < 0 || room.Type == null) return BadRequest();

            if (await _service.GetExamRoom(id) == null || await _doctorService.GetDoctor(room.DoctorId) == null) return NotFound();

            ExamRoom examRoom = _mapper.Map<ExamRoom>(room);
            examRoom.Id = id;
            await _service.UpdateExamRoom(examRoom);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletRoom(int id) {
            ExamRoom room = await _service.GetExamRoom(id);

            if (room == null) return NotFound();

            await _service.RemoveExamRoom(id);
            return Ok();
        }
    }
}
