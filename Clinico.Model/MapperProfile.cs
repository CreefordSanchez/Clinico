using AutoMapper;
namespace Clinico.Model {
    public class MapperProfile : Profile {
        public MapperProfile() {
           CreateMap<Doctor, DoctorCreateDTO>().ReverseMap();
           CreateMap<Doctor, DoctorDTO>().ReverseMap();  
           //ReverseMap means it can go back to Doctor <-> DTO
          // CreateMap<Something something>...
        }
    }
}
