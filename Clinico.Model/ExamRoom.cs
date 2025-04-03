using Clinico.Model;
namespace Clinico.Model {
    public class ExamRoom {
        public int Id { get; set; }
        public string Type { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
