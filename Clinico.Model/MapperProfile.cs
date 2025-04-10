using AutoMapper;
namespace Clinico.Model {
    public class MapperProfile : Profile {
        public MapperProfile() {
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Doctor, DoctorListDTO>().ReverseMap();
        }
    }
}
