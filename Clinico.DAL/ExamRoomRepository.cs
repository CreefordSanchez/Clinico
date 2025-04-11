using AutoMapper;
using Clinico.Model;

namespace Clinico.DAL {
    public class ExamRoomRepository {
        private readonly ClinicoContext _context;
        private readonly IMapper _mapper;

        public ExamRoomRepository(ClinicoContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task RemoveExamRoom(int id) {
            ExamRoom room = await GetExamRoom(id);

             _context.ExamRooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExamRoom(ExamRoom room) {
            ExamRoom newRoom = await GetExamRoom(room.Id);
            newRoom.Type = room.Type;
            newRoom.DoctorId = room.DoctorId;
            _context.ExamRooms.Update(newRoom);
             await _context.SaveChangesAsync();
        }

        public async Task<ExamRoom> GetExamRoom(int id) {
            return  await _context.ExamRooms.FindAsync(id);
        }

        public async Task<List<ExamRoomDTO>> GetExamRoomList() {
            List<ExamRoom> list = _context.ExamRooms.ToList();

            return _mapper.Map<List<ExamRoomDTO>>(list);
        }
    }
}