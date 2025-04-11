using AutoMapper;
using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinico.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly IMapper _appointmentMapper;
        public AppointmentsController(AppointmentService appointmentService, IMapper appointmentMapper)
        {
            _appointmentService = appointmentService;
            _appointmentMapper = appointmentMapper;
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
            if (appointmentDTO.Duration == 0 || appointmentDTO.ScheduledDate == default
                || string.IsNullOrWhiteSpace(appointmentDTO.SpecialistType)
                || appointmentDTO.DoctorId == 0 || appointmentDTO.PatientId == 0 || appointmentDTO.RoomId == 0)
            {
                return BadRequest("Appointment details cannot be null or zero.");
            }

            Appointment appointment = _appointmentMapper.Map<Appointment>(appointmentDTO);
            await _appointmentService.AddAppointmentAsync(appointment);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditAppointment(int id, AppointmentDTO appointmentDTO) 
        { 
            if (id < 0 || appointmentDTO.Duration == 0 || appointmentDTO.ScheduledDate == null
                || string.IsNullOrWhiteSpace(appointmentDTO.SpecialistType) || appointmentDTO.DoctorId == 0 
                || appointmentDTO.PatientId == 0 || appointmentDTO.RoomId == 0) return BadRequest("Appointment details cannot be null or zero.");
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
