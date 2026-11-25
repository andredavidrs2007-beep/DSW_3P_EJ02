using DSW_3P_EJ02.Models;
using DSW_3P_EJ02.Repositories;

namespace DSW_3P_EJ02.Services;

public class PersonajeService : IPersonajeService
{
    private readonly IPersonajeRepository _repository;

    public PersonajeService(IPersonajeRepository repository)
    {
        _repository = repository;
    }

    private static void Validar(Personaje Personaje)
    {
        if (string.IsNullOrWhiteSpace(Personaje.Nombre))
            throw new ArgumentException("El nombre no puede estar vacio");
        if (string.IsNullOrWhiteSpace(Personaje.VideoJuego))
            throw new ArgumentException("El videojuego no puede estar vacio");
        if (string.IsNullOrWhiteSpace(Personaje.Clase))
            throw new ArgumentException("La clase no puede estar vacia");
        if (Personaje.Nivel <= 0 || Personaje.Nivel > 100)
            throw new ArgumentException("El nivel debe estar entre 1 y 100");
        if (Personaje.Poder < 0)
            throw new ArgumentException("El poder debera ser mayor de 0");
        if (Personaje.Disponible == false)
            throw new ArgumentException("El personaje deben registrarse como disponible");
    }

    public Personaje? ObtenerPorId(int id)
    {
        var Personaje = _repository.ObtenerPorId(id);

        return Personaje;
    }

    public List<Personaje> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public List<Personaje> ObtenerPorVideojuego(string videojuego)
    {
        return _repository.ObtenerPorVideojuego(videojuego);
    }

    public Personaje Agregar(Personaje nuevaPersonaje)
    {
        Validar(nuevaPersonaje);

        return _repository.Agregar(nuevaPersonaje);
    }

    public bool Actualizar(int id, Personaje Personaje)
    {
        Validar(Personaje);

        return _repository.Actualizar(id, Personaje);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id);
    }
}