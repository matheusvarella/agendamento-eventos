using AgendamentoEventos.Data;
using AgendamentoEventos.Extensions;
using AgendamentoEventos.Models;
using AgendamentoEventos.Services;
using AgendamentoEventos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System;
using System.Threading.Tasks;

namespace AgendamentoEventos.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : ControllerBase
    {
        [HttpPost("register-pf")]
        public async Task<IActionResult> RegisterPFPostAsync(
            [FromServices]AgendamentoEventosDataContext context,
            [FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = new User
            {
                Name = model.Name,
                CnpjCpf = model.CnpjCpf,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Password = PasswordHasher.Hash(model.Password),
                TypeUser = "Participant",
                Timestamps = DateTime.Now,
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = model.Email, password = model.Password
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("Este e-mail ou cpf já está cadastrado."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }

        [HttpPost("register-pj")]
        public async Task<IActionResult> RegisterPJPostAsync(
            [FromServices] AgendamentoEventosDataContext context,
            [FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = new User
            {
                Name = model.Name,
                CnpjCpf = model.CnpjCpf,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Password = PasswordHasher.Hash(model.Password),
                TypeUser = "Organizer",
                Timestamps = DateTime.Now,
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = model.Email,
                    password = model.Password
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("Este e-mail ou cnpj já está cadastrado."));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> loginAsync(
            [FromServices]AgendamentoEventosDataContext context,
            [FromServices]TokenService tokenService,
            [FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos."));

            if(!PasswordHasher.Verify(user.Password, model.Password))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos."));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor."));
            }
        }
    }
}
