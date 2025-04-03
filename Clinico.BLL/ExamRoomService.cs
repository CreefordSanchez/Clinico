using Clinico.DAL;
using Clinico.Model;

namespace Clinico.BLL {
    public class ExamRoomService {
        private readonly ExamRoomRepository _repository;
        public ExamRoomService(ExamRoomRepository repository) {
            _repository = repository;
        }

        public void RemoveExamRoom(ExamRoom room) {
            if (room != null) {
                _repository.RemoveExamRoom(room);
            }
        }

        public void UpdateExamRoom(ExamRoom room) {
            if (room != null) {
                _repository.UpdateExamRoom(room);
            }
        }

        public ExamRoom GetExamRoom(int id) {
            if (id != null) {
                return _repository.GetExamRoom(id);
            }

            return new ExamRoom();
        }

        public List<ExamRoom> GetExamRoomsList() {
            List<ExamRoom> list = _repository.GetExamRoomList();

            if (list != null) {
                return list;
            }

            return new List<ExamRoom>();
        }
    }
}