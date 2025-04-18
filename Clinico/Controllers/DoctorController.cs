﻿using Clinico.BLL;
using Clinico.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Clinico.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller {
        private readonly DoctorService _service;
        private readonly IMapper _mapper;

        public DoctorController(DoctorService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);
            
            if (doctor == null) return NotFound();

            return Ok(doctor);
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorListDTO>>> GetDoctorList() {
            List<DoctorListDTO> list = await _service.GetDoctorList();

            if (list == null) return NotFound();

            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult> NewDoctor(DoctorDTO doctorDTO) {
            if (doctorDTO.Name == "" || doctorDTO.Email == "" || doctorDTO.Address == ""
                || doctorDTO.PhoneNumber == "" || doctorDTO.Specialty == "") {
                return BadRequest();                
            }

            Doctor doctor = _mapper.Map<Doctor>(doctorDTO);

            await _service.CreateDoctor(doctor);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditDoctor(int id, DoctorDTO doctor) {
            if (id < 0 || doctor.Email == "" ||
               doctor.Address == "" || doctor.PhoneNumber == "" ||
               doctor.Specialty == "" || doctor.Name == "") return BadRequest();

            if (await _service.GetDoctor(id) == null) return NotFound();

            Doctor doctorNew = _mapper.Map<Doctor>(doctor);
            doctorNew.Id = id;

            await _service.UpdateDoctor(doctorNew);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult>  DeleteDoctor(int id) {
            Doctor doctor = await _service.GetDoctor(id);
            
            if (doctor == null) return NotFound();

            await _service.DeleteDoctor(id);
            return Ok();
        }
    }
}
