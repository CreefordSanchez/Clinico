using AutoMapper;
namespace Clinico.Model {
    public class MapperProfile : Profile {
        public MapperProfile() {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Doctor, DoctorListDTO>().ReverseMap();
            CreateMap<ExamRoom, ExamRoomDTO>().ReverseMap();
            CreateMap<ExamRoom, ExamRoomEditDTO>().ReverseMap().ForMember(dest => dest.Doctor, opt => opt.Ignore());
        }
    }
}
