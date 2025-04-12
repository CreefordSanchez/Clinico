using AutoMapper;
using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;

namespace Clinico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly IMapper _appointmentMapper;
        private readonly DoctorService _doctorService;
        private readonly PatientService _patientService;
        private readonly ExamRoomService _roomService;
        public AppointmentsController(AppointmentService appointmentService, IMapper appointmentMapper, DoctorService doctorService, PatientService patientService, ExamRoomService examRoomService)
        {
            _appointmentService = appointmentService;
            _appointmentMapper = appointmentMapper;
            _doctorService = doctorService;
            _patientService = patientService;
            _roomService = examRoomService;
        }
        // FromQuery is used to filter the appointments based on the provided parameters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAll(
            [FromQuery] int? doctorId,
            [FromQuery] int? patientId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)

        {
            List<Appointment> appointments = await _appointmentService.GetAppointmentsAsync();
            if(appointments == null) return NotFound();
            List<AppointmentDTO> appointmentsDTO = _appointmentMapper.Map<List<AppointmentDTO>>(appointments);

            if (doctorId.HasValue)
                appointments = appointments.Where(a => a.DoctorId == doctorId).ToList();

            if (patientId.HasValue)
                appointments = appointments.Where(a => a.PatientId == patientId).ToList();

            if (startDate.HasValue)
                appointments = appointments.Where(a => a.ScheduledDate >= startDate.Value).ToList();

            if (endDate.HasValue)
                appointments = appointments.Where(a => a.ScheduledDate <= endDate.Value).ToList();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(int id)
        {
            Appointment appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAppointment(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO == null)
            {
               // return BadRequest("Appointment data is required.");
            }
        if (appointmentDTO.Duration <= 0 || appointmentDTO.ScheduledDate == null
            || string.IsNullOrWhiteSpace(appointmentDTO.SpecialistType))
        {
            return BadRequest("Duration, ScheduledDate, and SpecialistType are required.");
        }
        if (appointmentDTO.DoctorId <= 0 || appointmentDTO.PatientId <= 0 || appointmentDTO.RoomId <= 0)
        {
            return BadRequest("Valid DoctorId, PatientId, and RoomId are required.");
        }

        if (await _doctorService.GetDoctor(appointmentDTO.DoctorId) == null || await _doctorService.GetDoctor(appointmentDTO.DoctorId) == null || await _roomService.GetExamRoom(appointmentDTO.RoomId) == null)
            {
                return NoContent();
            }

            Appointment appointment = _appointmentMapper.Map<Appointment>(appointmentDTO);
            await _appointmentService.AddAppointmentAsync(appointment);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAppointment(int id, AppointmentDTO appointmentDTO) 
        {
            if (appointmentDTO == null)
            {
                return BadRequest("Appointment data is required.");
            }
            if (appointmentDTO.Duration <= 0 || appointmentDTO.ScheduledDate == null
                || string.IsNullOrWhiteSpace(appointmentDTO.SpecialistType))
            {
                return BadRequest("Duration, ScheduledDate, and SpecialistType are required.");
            }
            if (appointmentDTO.DoctorId <= 0 || appointmentDTO.PatientId <= 0 || appointmentDTO.RoomId <= 0)
            {
                return BadRequest("Valid DoctorId, PatientId, and RoomId are required.");
            }

            if (await _doctorService.GetDoctor(appointmentDTO.DoctorId) == null || await _doctorService.GetDoctor(appointmentDTO.DoctorId) == null || await _roomService.GetExamRoom(appointmentDTO.RoomId) == null)
            {
                return NoContent();
            }
            if (await _appointmentService.GetAppointmentByIdAsync(id) == null) return NotFound();
            Appointment appointmentNew = _appointmentMapper.Map<Appointment>(appointmentDTO);
            appointmentNew.Id = id;
            await _appointmentService.UpdateAppointmentAsync(appointmentNew);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Appointment existing = await _appointmentService.GetAppointmentByIdAsync(id);
            if (existing == null) return NotFound();
            await _appointmentService.DeleteAppointmentAsync(id);
            return Ok();
        }
    }
}
