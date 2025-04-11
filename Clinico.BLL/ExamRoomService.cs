using Clinico.DAL;
using Clinico.Model;

namespace Clinico.BLL {
    public class ExamRoomService {
        private readonly ExamRoomRepository _repository;
        private readonly AppointmentService _appointmentService;
        public ExamRoomService(ExamRoomRepository repository, AppointmentService appointmentService) {
            _repository = repository;
            _appointmentService = appointmentService;

        }

        public async Task RemoveExamRoom(int id) {
            List<Appointment> list = await _appointmentService.GetAppointmentsAsync();
            List<Appointment> newList = list.Where(a => a.RoomId == id).ToList();

            foreach (Appointment appointment in newList) {
                await _appointmentService.DeleteAppointmentAsync(appointment.Id);
            }

            await _repository.RemoveExamRoom(id);         
        }

        public async Task UpdateExamRoom(ExamRoom room) {
            await _repository.UpdateExamRoom(room);

        }

        public async Task<ExamRoom> GetExamRoom(int id) {
            return await _repository.GetExamRoom(id);
        }

        public async Task<List<ExamRoomDTO>> GetExamRoomsList() {
            return await _repository.GetExamRoomList();
        }
    }
}