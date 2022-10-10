using System;
using System.Collections.Generic;

namespace AgendamentoEventos.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int OrganizerId { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
        public int TicketLimit { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinalDate { get; set; }
        public DateTime Timestamps { get; set; }

        public User Organizer { get; set; }
        public IList<Ticket> Tickets { get; set; }
    }
}