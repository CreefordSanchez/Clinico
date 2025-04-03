using Clinico.Model;
namespace Clinico.Models {
    public class ExamRoom {
        public int Id { get; set; }
        public string Type { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
