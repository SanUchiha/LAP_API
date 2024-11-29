using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleLAP.DTOs;
using SimpleLAP.Models;

namespace SimpleLAP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipantesController : ControllerBase
{
    private readonly LapDbSimpleContext _context;
    private readonly IMapper _mapper;

    public ParticipantesController(LapDbSimpleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtiene todos los participantes
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
    {
        return await _context.Participantes.ToListAsync();
    }

    /// <summary>
    /// Obtiene un participante por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Participante>> GetParticipante(int id)
    {
        var participante = await _context.Participantes.FindAsync(id);

        if (participante == null)
        {
            return NotFound();
        }

        return participante;
    }

    /// <summary>
    /// Modifica un participante
    /// </summary>
    /// <param name="id"></param>
    /// <param name="participante"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParticipante(int id, Participante participante)
    {
        if (id != participante.IdParticipante)
        {
            return BadRequest();
        }

        _context.Entry(participante).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParticipanteExists(id))
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

    /// <summary>
    /// Registra un participante
    /// </summary>
    /// <param name="participante"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Participante>> PostParticipante(ParticipanteDTO participanteDTO)
    {
        Campus? campus = _context.Campuses
                 .AsNoTracking()
                 .FirstOrDefault(c => c.IdCampus == participanteDTO.IdCampus);

        if (campus == null)
            return BadRequest("El campus asociado no existe.");

        Participante participante =
            _mapper.Map<ParticipanteDTO, Participante>(participanteDTO);

        try
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Captura el error relacionado con la clave única
            if (ex.InnerException?.Message.Contains("UQ__Particip__AD99A6EEBCC439EB") ?? false)
            {
                return Conflict("Ya existe un participante con el mismo DNI.");
            }

            // Si es otro tipo de error, puedes manejarlo aquí
            return StatusCode(500, "Ocurrió un error al intentar guardar el participante.");
        }

        return Ok();
    }

    /// <summary>
    /// Elimina un participante
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParticipante(int id)
    {
        var participante = await _context.Participantes.FindAsync(id);
        if (participante == null)
        {
            return NotFound();
        }

        _context.Participantes.Remove(participante);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ParticipanteExists(int id)
    {
        return _context.Participantes.Any(e => e.IdParticipante == id);
    }
}
