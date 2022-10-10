using AgendamentoEventos.Data;
using AgendamentoEventos.Extensions;
using AgendamentoEventos.Models;
using AgendamentoEventos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendamentoEventos.Controllers
{
    [ApiController]
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
            [FromServices] AgendamentoEventosDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var item = await context
                    .Events
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (item == null)
                    return NotFound(new ResultViewModel<Event>("Evento não encontrado."));

                return Ok(new ResultViewModel<Event>(item));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Event>("Falha interna no servidor."));
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
                var item = new Event
                {
                    Id = 0,
                    Title = model.Title,
                    Value = model.Value,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    FinalDate = model.FinalDate,
                };
                await context.Events.AddAsync(item);
                await context.SaveChangesAsync();

                return Created($"events/{item.Id}", item);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível incluir o evento.");
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Event>("Falha interna no servidor."));
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
