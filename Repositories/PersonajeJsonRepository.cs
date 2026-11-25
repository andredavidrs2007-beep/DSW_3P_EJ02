using DSW_3P_EJ02.Models;
using System.Text.Json;

namespace DSW_3P_EJ02.Repositories;

public class PersonajeJsonRepository : IPersonajeRepository
{
    private readonly string _ruta;
    private readonly JsonSerializerOptions _opciones = new()
    {
        WriteIndented = true
    };

    public PersonajeJsonRepository(IWebHostEnvironment environment)
    {
        _ruta = Path.Combine(environment.ContentRootPath, "Data", "personajes.json");

        Directory.CreateDirectory(Path.GetDirectoryName(_ruta)!);

        if (!File.Exists(_ruta))
        {
            File.WriteAllText(_ruta, "[]");
        }
    }

    private List<Personaje> Leer()
    {
        string Json = File.ReadAllText(_ruta);

        return JsonSerializer.Deserialize<List<Personaje>>(Json) ?? new List<Personaje>();
    }

    private void Guardar(List<Personaje> datos)
    {
        string Json = JsonSerializer.Serialize(datos, _opciones);
        
        File.WriteAllText(_ruta, Json);
    }

    public List<Personaje> ObtenerTodos()
    {
        return Leer();
    }

    public Personaje? ObtenerPorId(int id)
    {
        return Leer().FirstOrDefault(d => d.Id == id);
    }

    public List<Personaje> ObtenerPorVideojuego(string videojuego)
    {
        return Leer()
            .Where(d => d.VideoJuego.Equals(videojuego, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Personaje Agregar(Personaje nueva)
    {
        var datos = Leer();

        nueva.Id = datos.Count == 0 ? 1 : datos.Max(d => d.Id + 1);
        datos.Add(nueva);
        Guardar(datos);

        return nueva;
    }

    public bool Actualizar(int id, Personaje actualizada)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(d => d.Id == id);

        if(existente == null)
        {
            return false;
        }
        existente.Nombre = actualizada.Nombre;
        existente.VideoJuego = actualizada.VideoJuego;
        existente.Clase = actualizada.Clase;
        existente.Nivel = actualizada.Nivel;
        existente.Poder = actualizada.Poder;
        existente.Disponible = actualizada.Disponible;
        existente.ImagenUrl = actualizada.ImagenUrl;
        
        Guardar(datos);

        return true;
    }

    public bool Eliminar(int id)
    {
        var datos = Leer();

        var existente = datos.FirstOrDefault(d => d.Id == id);

        if(existente == null)
        {
            return false;
        }
        datos.Remove(existente);
        Guardar(datos);

        return true;
    }
}