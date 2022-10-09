using AgendamentoEventos.Data.Mappings;
using AgendamentoEventos.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoEventos.Data
{
    public class AgendamentoEventosDataContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433; Database=AgendamentoEventos;User ID=sa;Password=1q2w3e4r@#$");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventMap());
            modelBuilder.ApplyConfiguration(new TicketMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
