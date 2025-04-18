﻿
namespace Clinico.Model {
    public class Patient {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
