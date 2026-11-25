using DSW_3P_EJ02.Models;

namespace DSW_3P_EJ02.Services;

public interface IPersonajeService
{
    List<Personaje> ObtenerTodos();
    Personaje? ObtenerPorId(int id);
    List<Personaje> ObtenerPorVideojuego(string videojuego);
    Personaje Agregar(Personaje nuevoPersonaje);
    bool Actualizar(int id, Personaje actualizado);
    bool Eliminar(int id);
}