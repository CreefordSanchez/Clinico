using Clinico.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Clinico.Model;
using AutoMapper;

namespace Clinico.BLL
{
    public class AppointmentService
    {
        private readonly AppointmentRepository _appointmentRepository;
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly ExamRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public AppointmentService(
            AppointmentRepository appointmentRepository, 
            PatientRepository patientRepository, 
            DoctorRepository doctorRepository, 
            ExamRoomRepository roomRepository,
            IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<List<Appointment>> GetAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAppointmentsAsync();
        }
        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _appointmentRepository.GetAppointmentByIdAsync(id);
        }
        public async Task<Appointment> CreateAppointmentWithEntitiesAsync(AppointmentDTO dto)
        {
            /*
             Doctor doctor = await _doctorRepository.GetDoctor(dto.DoctorId)
                         ?? throw new ArgumentException("Doctor not found.");
            Patient patient = await _patientRepository.GetPatientByIdAsync(dto.PatientId)
                         ?? throw new ArgumentException("Patient not found.");
            ExamRoom room = await _roomRepository.GetExamRoom(dto.RoomId)
                         ?? throw new ArgumentException("Room not found.");

                        maybe try doign the validation in the controller. some wierd stuff goining on when injecting other repo in service

            var appointment = new Appointment
            {
                Duration = dto.Duration,
                ScheduledDate = dto.ScheduledDate,
                SpecialistType = dto.SpecialistType,
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                RoomId = dto.RoomId,
                Doctor = doctor,
                Patient = patient,
                ExamRoom = room
            };
            await _appointmentRepository.AddAppointmentAsync(appointment);
            // Add the appointment to the patient's list if necessary
            patient.Appointments ??= new List<Appointment>();
            patient.Appointments.Add(appointment);
            await _patientRepository.UpdatePatientAsync(patient);
            return appointment;
        }
             */



            Appointment appointment = _mapper.Map<Appointment>(dto);

            await _appointmentRepository.AddAppointmentAsync(appointment);
            return appointment;
        }
        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.AddAppointmentAsync(appointment);
        }
        public async Task<Appointment> UpdateAppointmentWithEntitiesAsync(int id, AppointmentDTO dto)
        {
            Appointment appointment = await _appointmentRepository.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Appointment not found.");
            }

            Doctor doctor = await _doctorRepository.GetDoctor(dto.DoctorId)
                         ?? throw new ArgumentException("Doctor not found.");
            Patient patient = await _patientRepository.GetPatientByIdAsync(dto.PatientId)
                         ?? throw new ArgumentException("Patient not found.");
            ExamRoom room = await _roomRepository.GetExamRoom(dto.RoomId)
                         ?? throw new ArgumentException("Room not found.");
            // Update fields
            appointment.Duration = dto.Duration;
            appointment.ScheduledDate = dto.ScheduledDate;
            appointment.SpecialistType = dto.SpecialistType;
            appointment.DoctorId = dto.DoctorId;
            appointment.PatientId = dto.PatientId;
            appointment.RoomId = dto.RoomId;
            appointment.Doctor = doctor;
            appointment.Patient = patient;
            appointment.ExamRoom = room;
            await _appointmentRepository.UpdateAppointmentAsync(appointment);
            return appointment;
        }
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.UpdateAppointmentAsync(appointment);
        }
        public async Task DeleteAppointmentAsync(int id)
        {
            await _appointmentRepository.DeleteAppointmentAsync(id);
        }
    }
}
