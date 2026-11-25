using Microsoft.AspNetCore.Mvc;
using DSW_3P_EJ02.Services;
using DSW_3P_EJ02.Models;
using Microsoft.AspNetCore.Authorization;

namespace DSW_3P_EJ02.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize]
public class PersonajeController : ControllerBase
{
    private readonly IPersonajeService _service;
    
    public PersonajeController(IPersonajeService service)
    {
        _service = service;
    }

    //GET - Obtener todos los personajes
    [HttpGet]
    [ProducesResponseType(typeof(List<Personaje>),200)]
    public IActionResult ObtenerTodos()
    {
        return Ok(_service.ObtenerTodos());   
    }

    //GET - Obtener personaje por ID
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(List<Personaje>), 200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var Personaje = _service.ObtenerPorId(id);

        if(Personaje == null)
        {
            return NotFound();
        }
        return Ok(Personaje);
    }

    //GET - Obtener personaje por videojuego
    [HttpGet("videojuego/{videojuego}")]
    [ProducesResponseType(typeof(List<Personaje>), 200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorVideojuego(string videojuego)
    {
        var personajes = _service.ObtenerPorVideojuego(videojuego);

        if(personajes.Count == 0)
        {
            return NotFound();
        }
        return Ok(personajes);
    }

    //POST - Agregar Personaje
    [HttpPost]
    [ProducesResponseType(typeof(Personaje), 200)]
    [ProducesResponseType(400)]
    public IActionResult Agregar([FromBody] Personaje Personaje)
    {
        try
        {
            var creada = _service.Agregar(Personaje);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new {id = creada.Id},
                creada
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //PUT - Actualizar Personaje
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public IActionResult Actualizar(int id, [FromBody] Personaje Personaje)
    {
        try
        {
            if (!_service.Actualizar(id, Personaje))
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new {mensaje = ex.Message});
        }
    }

    //DELETE - Eliminar Personaje
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        if (!_service.Eliminar(id))
        {
            return NotFound();
        }
        return NoContent();
    }
}