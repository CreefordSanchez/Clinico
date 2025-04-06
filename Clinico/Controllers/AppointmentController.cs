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

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // GET: api/appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll(
            [FromQuery] int? doctorId,
            [FromQuery] int? patientId,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var appointments = await _appointmentService.GetAppointmentsAsync();

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

        // GET: api/appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        // POST: api/appointments
        [HttpPost]
        public async Task<ActionResult> Create(Appointment appointment)
        {
            await _appointmentService.AddAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
        }

        // PUT: api/appointments/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Appointment appointment)
        {
            if (id != appointment.Id)
                return BadRequest("ID mismatch.");

            var existing = await _appointmentService.GetAppointmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _appointmentService.UpdateAppointmentAsync(appointment);
            return NoContent();
        }

        // DELETE: api/appointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _appointmentService.GetAppointmentByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }
    }
}
