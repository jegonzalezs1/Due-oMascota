using System.Collections.Generic;
using System.Threading.Tasks; // Asegúrate de incluir esto
using DueñoMascota.Models;
using DueñoMascota.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DueñoMascota.Controllers
{
    [ApiController]
    [Route("api/mascota")]
    public class MascotaController : ControllerBase
    {
        private readonly IMascotaService _mascotaService;

        public MascotaController(IMascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mascota>>> ObtenerTodas()
        {
            var mascotas = await _mascotaService.ObtenerTodasAsync();
            return Ok(mascotas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mascota>> ObtenerPorId(int id)
        {
            var mascota = await _mascotaService.ObtenerPorIdAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            return Ok(mascota);
        }

        [HttpPost]
        public async Task<ActionResult> Insertar([FromBody] Mascota mascota)
        {
            await _mascotaService.CrearAsync(mascota);
            return Ok(new { message = "Los datos de la mascota se han guardado correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] Mascota mascota)
        {
            var mascotaExistente = await _mascotaService.ObtenerPorIdAsync(id);
            if (mascotaExistente == null)
            {
                return NotFound();
            }
            mascota.IdMascota = id;
            await _mascotaService.ActualizarAsync(mascota);
            return Ok(new { message = "Los datos de la mascota se han actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var mascota = await _mascotaService.ObtenerPorIdAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            await _mascotaService.EliminarAsync(id);
            return Ok(new { message = "Los datos de la mascota se han eliminado correctamente" });
        }
    }
}
