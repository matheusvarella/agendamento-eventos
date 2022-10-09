using System;
using System.Collections.Generic;

namespace AgendamentoEventos.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CnpjCpf { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Timestamps { get; set; }

        public IList<Event> Events { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }
}
