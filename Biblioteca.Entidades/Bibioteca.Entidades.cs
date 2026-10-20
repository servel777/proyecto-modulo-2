namespace Biblioteca.Entidades;

public class Libros
{
    public int IdLibros { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public int AnioPublicacion { get; set; }
    public int Stock { get; set; }
}

public class Socios
{
    public int IdSocio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public DateTime? FechaRegistro { get; set; }
}

public class Prestamos
{
    public int IdPrestamo { get; set; }
    public int IdLibros { get; set; }
    public int IdSocio { get; set; }
    public DateTime? FechaPrestamo { get; set; }
    public DateTime? FechaDevolucion { get; set; }
}
