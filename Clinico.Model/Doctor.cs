using Clinico.Model;

namespace Clinico.Model {
    public class Doctor {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialty { get; set; }
        public int RoomId { get; set; }
        public int AppointmentId { get; set; }
        public ICollection<ExamRoom> ExamRooms { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
