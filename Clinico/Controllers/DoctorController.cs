using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Clinico.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller {
        private readonly DoctorService _service;
        private readonly ExamRoomService _roomService;
        private readonly AppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public DoctorController(DoctorService service, ExamRoomService roomService, AppointmentService appointmentService, IMapper mapper) {
            _service = service;
            _roomService = roomService;
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);
            
            if (doctor == null) return NotFound();

            DoctorDTO dto = _mapper.Map<DoctorDTO>(doctor);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctorList() {
            List<Doctor> list = await _service.GetDoctorList();

            if (list == null) return NotFound();

            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult> NewDoctor(DoctorCreateDTO doctorDTO) {
            if (doctorDTO.Name == null || doctorDTO.Email == null || doctorDTO.Address == null 
                || doctorDTO.PhoneNumber == null || doctorDTO.Specialty == null) {
                return BadRequest();                
            }

            Doctor doctor = _mapper.Map<Doctor>(doctorDTO);

            await _service.CreateDoctor(doctor);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditDoctor(
            int id, string name, string email, string address,
            string phoneNumber, string specialty, int appointmentId, int roomId) {
            Doctor doctor = await _service.GetDoctor(id);

            if (doctor == null || await _roomService.GetExamRoom(roomId) == null ||
                await _appointmentService.GetAppointmentByIdAsync(appointmentId) == null
                ) return NotFound();
            
            if (name == null || email == null ||
                address == null || phoneNumber == null ||
                specialty == null) return BadRequest();


            await _service.UpdateDoctor(new Doctor()
            {
                Name = name,
                Email = email,
                Address = address,
                PhoneNumber = phoneNumber,
                Specialty = specialty
            });

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult>  DeleteDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);
            
            if (doctor == null) return NotFound();

            await _service.DeleteDoctor(id);
            return Ok();
        }
    }
}
