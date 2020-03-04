using CoreAngular.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngular.Controllers.Api
{
    [Route("api/personas")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PersonasController(
      ApplicationDbContext context
      )
        {

            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var persona = await _context.Persona.SingleOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }

            _context.Persona.Remove(persona);
            await _context.SaveChangesAsync();

            return Ok(persona);
        }
        // POST: api/Personas
        [HttpPost("crear")]
        public async Task<IActionResult> PostPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Persona.Add(persona);
            await _context.SaveChangesAsync();

            return Ok(persona);
        }


        private async Task CrearOEditarDirecciones(List<Direccion> direcciones)
        {
            List<Direccion> direccionesACrear = direcciones.Where(x => x.Id == 0).ToList();
            List<Direccion> direccionesAEditar = direcciones.Where(x => x.Id != 0).ToList();

            if (direccionesACrear.Any())
            {
                await _context.AddRangeAsync(direccionesACrear);
            }

            if (direccionesAEditar.Any())
            {
                _context.UpdateRange(direccionesAEditar);
            }

        }

         // POST: api/Personas
        [HttpPut("actualizar")]
        public async Task<IActionResult> actualizarPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
           _context.Entry(persona).State = EntityState.Modified;
           try
            {
              await CrearOEditarDirecciones(persona.Direcciones);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaExists(persona.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.Id == id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> listPersonabyId([FromRoute] int id,bool incluirDirecciones=false)
        {

            Persona persona;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (incluirDirecciones)
            {
                 persona = await _context.Persona.Include(x=>x.Direcciones).Where(p => p.Id == id).SingleOrDefaultAsync();
            }
            else
            {
                 persona = await _context.Persona.Where(p => p.Id == id).SingleOrDefaultAsync();
            } 

            if (persona == null)
            {
               return NotFound();
            }
               return Ok(persona);
         

        }


        [HttpGet("")]
        public IActionResult listPersonas()
        {
            return Ok(_context.Persona.ToList());

        }


    }
}