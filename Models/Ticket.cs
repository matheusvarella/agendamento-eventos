using System;

namespace AgendamentoEventos.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public bool Status { get; set; }
        public DateTime Timestamps { get; set; }

        public Event Event { get; set; }
        public User Participant { get; set; }
    }
}