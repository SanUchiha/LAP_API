using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using SimpleLAP.Models;

namespace SimpleLAP.Controllers;

[EnableCors("CORS")]
[Route("api/[controller]")]
[ApiController]
public class CampusController : ControllerBase
{
    private readonly LapDbSimpleContext _context;

    public CampusController(LapDbSimpleContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los campus
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Campus>>> GetCampuses()
    {
        return await _context.Campuses.ToListAsync();
    }

    /// <summary>
    /// Obtiene un campus por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Campus>> GetCampus(int id)
    {
        var campus = await _context.Campuses.FindAsync(id);

        if (campus == null)
        {
            return NotFound();
        }

        return campus;
    }

    /// <summary>
    /// MOdifica un campus
    /// </summary>
    /// <param name="id"></param>
    /// <param name="campus"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCampus(int id, Campus campus)
    {
        if (id != campus.IdCampus)
        {
            return BadRequest();
        }

        _context.Entry(campus).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CampusExistsAsync(id))
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
    /// Crea un campus
    /// </summary>
    /// <param name="campus"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Campus>> PostCampus(Campus campus)
    {
        _context.Campuses.Add(campus);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCampus", new { id = campus.IdCampus }, campus);
    }

    /// <summary>
    /// Elimina un campus
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCampus(int id)
    {
        var campus = await _context.Campuses.FindAsync(id);
        if (campus == null)
        {
            return NotFound();
        }

        _context.Campuses.Remove(campus);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Camprueba si existe un campus
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool CampusExistsAsync(int id)
    {
        return _context.Campuses.Any(e => e.IdCampus == id);
    }

    /// <summary>
    /// Descargar pdf campus por ID
    /// </summary>
    /// <param name="campusId"></param>
    /// <returns></returns>
    [HttpGet("pdf/{idCampus}")]
    public IActionResult DownloadPdf(int idCampus)
    {
        string rutaPdf =
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                idCampus + ".pdf");

        if (System.IO.File.Exists(rutaPdf))
        {
            byte[] data = System.IO.File.ReadAllBytes(rutaPdf);

            const string type = "application/pdf";

            return File(data, type, "Campus_Navidad_Beniajan_2024.pdf");
        }
        else
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Obtiene los participantes de un campus
    /// </summary>
    /// <param name="idCampus"></param>
    /// <returns></returns>
    [HttpGet("participantes/{idCampus}")]
    public async Task<IActionResult> GetParticipantesByCampus(int idCampus)
    {
        bool existCampus = CampusExistsAsync(idCampus);

        if(!existCampus) return NotFound();

        List<Participante> participantes = 
            await _context.Participantes
                .Where(p => p.IdCampus == idCampus)
                .ToListAsync();

        return Ok(participantes);
    }
}
