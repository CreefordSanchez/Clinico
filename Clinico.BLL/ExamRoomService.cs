using Clinico.DAL;
using Clinico.Model;

namespace Clinico.BLL {
    public class ExamRoomService {
        private readonly ExamRoomRepository _repository;
        public ExamRoomService(ExamRoomRepository repository) {
            _repository = repository;
        }

        public async Task RemoveExamRoom(int id) {
            await _repository.RemoveExamRoom(id);         
        }

        public async Task UpdateExamRoom(ExamRoom room) {
            if (room != null) {
                await _repository.UpdateExamRoom(room);
            }
        }

        public async Task<ExamRoom> GetExamRoom(int id) {
            return await _repository.GetExamRoom(id);
        }

        public async Task<List<ExamRoom>> GetExamRoomsList() {
            List<ExamRoom> list = await _repository.GetExamRoomList();

            if (list != null) {
                return list;
            }

            return new List<ExamRoom>();
        }
    }
}