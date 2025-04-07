namespace Clinico.Model {
    public class Appointment {
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string SpecialistType { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public int RoomId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public ExamRoom ExamRoom { get; set; }
    }
}
