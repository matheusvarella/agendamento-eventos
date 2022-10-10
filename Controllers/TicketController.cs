using AgendamentoEventos.Data;
using AgendamentoEventos.Extensions;
using AgendamentoEventos.Models;
using AgendamentoEventos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoEventos.Controllers
{
    [ApiController]
    [Route("tickets")]
    [Authorize(Roles = "Participant")]
    public class TicketController : ControllerBase
    {
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetByUserIdAsync(
            [FromServices]AgendamentoEventosDataContext context,
            [FromRoute]int id)
        {
            try
            {
                var user = await context
                    .Users
                    .Include(x => x.Tickets)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<string>("Usuário não encontrado."));

                var tickets = user.Tickets.ToList();

                if (tickets == null)
                    return NotFound(new ResultViewModel<string>("ingressos não encontrados."));

                return Ok(new ResultViewModel<List<Ticket>>(tickets));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Não foi possível encontrar os ingressos.");
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }

        [HttpGet("{userId:int}/{ticketId}")]
        public async Task<IActionResult> GetByUserIdAndTicketIdAsync(
            [FromServices] AgendamentoEventosDataContext context,
            [FromRoute] int userId,
            [FromRoute] int ticketId)
        {
            try
            {
                var user = await context
                    .Users
                    .Include(x => x.Tickets)
                    .FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                    return NotFound(new ResultViewModel<string>("Usuário não encontrado."));

                var ticket = user.Tickets.FirstOrDefault(x => x.Id == ticketId);

                if (ticket == null)
                    return NotFound(new ResultViewModel<string>("ingresso não encontrado."));

                return Ok(new ResultViewModel<Ticket>(ticket));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Não foi possível encontrar o ingresso.");
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }

        [HttpPost("buy-ticket")]
        public async Task<IActionResult> BuyTicketPostAsync(
            [FromServices]AgendamentoEventosDataContext context,
            [FromBody]BuyTicketViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
            
            try
            {
                var ticket = new Ticket
                {
                    EventId = model.EventId,
                    ParticipantId = model.ParticipantId,
                    Name = model.Name.Trim(),
                    Cpf = model.Cpf,
                    Status = "Comprado",
                };

                await context.Tickets.AddAsync(ticket);
                await context.SaveChangesAsync();
                return Created($"{model.ParticipantId}/{ticket.Id}", new ResultViewModel<Ticket>(ticket));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Não foi possível comprar o ingresso.");
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }
    }
}
