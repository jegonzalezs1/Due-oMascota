using System.Collections.Generic;
using System.Threading.Tasks;
using DueñoMascota.Models;
using DueñoMascota.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DueñoMascota.Controllers
{
    [ApiController]
    [Route("api/persona")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService _personaService;

        public PersonaController(IPersonaService personaService)
        {
            _personaService = personaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Persona>>> ObtenerTodas()
        {
            var personas = await _personaService.ObtenerTodasAsync();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> ObtenerPorId(int id)
        {
            var persona = await _personaService.ObtenerPorIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult> Insertar([FromBody] Persona persona)
        {
            await _personaService.CrearAsync(persona);
            return Ok(new { message = "Los datos de la persona se han guardado correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] Persona persona)
        {
            var personaExistente = await _personaService.ObtenerPorIdAsync(id);
            if (personaExistente == null)
            {
                return NotFound();
            }
            persona.IdPersona = id;
            await _personaService.ActualizarAsync(persona);
            return Ok(new { message = "Los datos de la persona se han actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var persona = await _personaService.ObtenerPorIdAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            await _personaService.EliminarAsync(id);
            return Ok(new { message = "Los datos de la persona se han eliminado correctamente" });
        }
    }
}
