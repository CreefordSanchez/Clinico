using Clinico.Model;

namespace Clinico.DAL {
    public class ExamRoomRepository {
        private readonly ClinicoContext _context;
        public ExamRoomRepository(ClinicoContext context) {
            _context = context;
        }

        public async Task RemoveExamRoom(int id) {
            ExamRoom room = await GetExamRoom(id);

             _context.ExamRooms.Remove(room);
            _context.SaveChanges();
        }

        public async Task UpdateExamRoom(ExamRoom room) {
            ExamRoom newRoom = await GetExamRoom(room.Id);
            newRoom.Type = room.Type;

            _context.ExamRooms.Update(newRoom);
            _context.SaveChanges();
        }

        public async Task<ExamRoom> GetExamRoom(int id) {
            return  _context.ExamRooms.Find(id);
        }

        public async Task<List<ExamRoom>> GetExamRoomList() {
            return  _context.ExamRooms.ToList();
        }
    }
}