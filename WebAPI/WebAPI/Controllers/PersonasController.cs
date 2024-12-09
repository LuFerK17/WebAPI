using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Database;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : Controller
    {
        private readonly AppDBContext _context;

        public PersonasController(AppDBContext context)
        {
            {
                _context = context;
            }
        }
        //Metodos
        //Obtener todas las personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersona()
        {

            return await _context.Personas.ToListAsync();

        }
        //Obtener personas por ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            return persona;

        }
        // Crear una nueva persona
        [HttpPost]
        public async Task<ActionResult<Persona>> CreatePersona(Persona persona)
        {

            persona.FechaAdicion = DateTime.UtcNow; // Asignar fecha actual
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersona), new { id = persona.IdPersona }, persona);


        }
        // Actualizar una persona
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, Persona persona)
        {
            if (id != persona.IdPersona) return BadRequest();

            var personaExistente = await _context.Personas.FindAsync(id);
            if (personaExistente == null) return NotFound();

            personaExistente.Nombre = persona.Nombre;
            personaExistente.Apellido = persona.Apellido;
            personaExistente.Email = persona.Email;
            personaExistente.FechaNacimiento = persona.FechaNacimiento;
            personaExistente.Telefono = persona.Telefono;
            personaExistente.ModificadoPor = persona.ModificadoPor;
            personaExistente.FechaModificacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();


        }
        // Eliminar una persona
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePersona(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return NotFound();
            _context.Personas.Remove(persona);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}