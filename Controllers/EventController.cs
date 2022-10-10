using AgendamentoEventos.Data;
using AgendamentoEventos.Extensions;
using AgendamentoEventos.Models;
using AgendamentoEventos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendamentoEventos.Controllers
{
    [ApiController]
    [Authorize(Roles = "Organizer")]
    public class EventController : ControllerBase
    {
        [HttpGet("events")]
        public async Task<IActionResult> GetAsync(
            [FromServices]AgendamentoEventosDataContext context)
        {
            try
            {
                var events = await context.Events.ToListAsync();
                return Ok(new ResultViewModel<List<Event>>(events));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Event>>("Falha interna do servidor."));
            }
        }

        [HttpGet("events/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices]AgendamentoEventosDataContext context,
            [FromRoute]int id)
        {
            try
            {
                var item = await context
                    .Events
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (item == null)
                    return NotFound(new ResultViewModel<string>("Evento não encontrado."));

                return Ok(new ResultViewModel<Event>(item));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }

        [HttpPost("events")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AgendamentoEventosDataContext context,
            [FromBody] EditorEventViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Event>(ModelState.GetErrors()));

            try
            {
                if (model.StartDate > model.FinalDate)
                    return NotFound(new ResultViewModel<string>("Datas informadas para o evento inválidas."));


                var item = new Event
                {
                    Title = model.Title.Trim(),
                    Value = model.Value,
                    TicketLimit = model.TicketLimit,
                    Description = model.Description.Trim(),
                    StartDate = new DateTime(model.StartDate.Year, model.StartDate.Month, model.StartDate.Day),
                    FinalDate = new DateTime(model.FinalDate.Year, model.FinalDate.Month, model.FinalDate.Day),
                };
                await context.Events.AddAsync(item);
                await context.SaveChangesAsync();

                return Created($"events/{item.Id}", new ResultViewModel<Event>(item));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<string>("Não foi possível incluir o evento." + ex.InnerException));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }

        [HttpPut("events/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AgendamentoEventosDataContext context,
            [FromRoute] int id,
            [FromBody] Event model)
        {
            var item = await context
                .Events
                .FirstOrDefaultAsync(x => x.Id == id);

            item.Description = model.Description;

            context.Events.Update(item);
            await context.SaveChangesAsync();

            return Ok(model);
        }
    }
}
