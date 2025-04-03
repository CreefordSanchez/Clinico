using Clinico.Model;

namespace Clinico.DAL {
    public class ExamRoomRepository {
        private readonly ClinicoContext _context;
        public ExamRoomRepository(ClinicoContext context) {
            _context = context;
        }

        public void RemoveExamRoom(ExamRoom Room) {
            _context.ExamRooms.Remove(Room);
            _context.SaveChanges();
        }

        public void UpdateExamRoom(ExamRoom room) {
            ExamRoom newRoom = GetExamRoom(room.Id);
            newRoom.Type = room.Type;

            _context.ExamRooms.Update(newRoom);
            _context.SaveChanges();
        }

        public ExamRoom GetExamRoom(int id) {
            return _context.ExamRooms.Find(id);
        }

        public List<ExamRoom> GetExamRoomList() {
            return _context.ExamRooms.ToList();
        }
    }
}