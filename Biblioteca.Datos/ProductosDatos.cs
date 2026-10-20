using System.Data;
using Biblioteca.Entidades;
using Microsoft.Data.SqlClient;

namespace Biblioteca.Datos;

public class ProductosDatos
{
    public List<Libros> ListarLibros()
    {
        const string consulta = """
            SELECT id_libros, titulo, autor, anio_publicacion, stock
            FROM dbo.Libros
            ORDER BY id_libros;
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        conexion.Open();

        using var lector = comando.ExecuteReader();
        var libros = new List<Libros>();
        while (lector.Read())
        {
            libros.Add(new Libros
            {
                IdLibros = lector.GetInt32(0),
                Titulo = lector.GetString(1),
                Autor = lector.GetString(2),
                AnioPublicacion = lector.GetInt32(3),
                Stock = lector.GetInt32(4)
            });
        }

        return libros;
    }

    public bool RegistrarLibro(Libros libro)
    {
        const string consulta = """
            INSERT INTO dbo.Libros (titulo, autor, anio_publicacion, stock)
            VALUES (@titulo, @autor, @anio_publicacion, @stock);
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@titulo", SqlDbType.NVarChar, 150).Value = libro.Titulo;
        comando.Parameters.Add("@autor", SqlDbType.NVarChar, 100).Value = libro.Autor;
        comando.Parameters.Add("@anio_publicacion", SqlDbType.Int).Value = libro.AnioPublicacion;
        comando.Parameters.Add("@stock", SqlDbType.Int).Value = libro.Stock;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public bool EliminarLibro(int idLibro)
    {
        const string consulta = """
            DELETE FROM dbo.Libros
            WHERE id_libros = @id_libros
              AND NOT EXISTS (
                  SELECT 1 FROM dbo.Prestamos WHERE id_libros = @id_libros
              );
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_libros", SqlDbType.Int).Value = idLibro;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public bool ActualizarLibro(Libros libro)
    {
        const string consulta = """
            UPDATE dbo.Libros
            SET titulo = @titulo,
                autor = @autor,
                anio_publicacion = @anio_publicacion,
                stock = @stock
            WHERE id_libros = @id_libros;
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_libros", SqlDbType.Int).Value = libro.IdLibros;
        comando.Parameters.Add("@titulo", SqlDbType.NVarChar, 150).Value = libro.Titulo;
        comando.Parameters.Add("@autor", SqlDbType.NVarChar, 100).Value = libro.Autor;
        comando.Parameters.Add("@anio_publicacion", SqlDbType.Int).Value = libro.AnioPublicacion;
        comando.Parameters.Add("@stock", SqlDbType.Int).Value = libro.Stock;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public List<Socios> ListarSocios()
    {
        const string consulta = """
            SELECT id_socio, nombre, apellido, correo, fecha_registro
            FROM dbo.Socios
            ORDER BY id_socio;
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        conexion.Open();

        using var lector = comando.ExecuteReader();
        var socios = new List<Socios>();
        while (lector.Read())
        {
            socios.Add(new Socios
            {
                IdSocio = lector.GetInt32(0),
                Nombre = lector.GetString(1),
                Apellido = lector.GetString(2),
                Correo = lector.GetString(3),
                FechaRegistro = lector.IsDBNull(4) ? null : lector.GetDateTime(4)
            });
        }

        return socios;
    }

    public bool RegistrarSocio(Socios socio)
    {
        const string consulta = """
            INSERT INTO dbo.Socios (nombre, apellido, correo, fecha_registro)
            VALUES (@nombre, @apellido, @correo, @fecha_registro);
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = socio.Nombre;
        comando.Parameters.Add("@apellido", SqlDbType.NVarChar, 100).Value = socio.Apellido;
        comando.Parameters.Add("@correo", SqlDbType.NVarChar, 150).Value = socio.Correo;
        comando.Parameters.Add("@fecha_registro", SqlDbType.Date).Value =
            socio.FechaRegistro is null ? DBNull.Value : socio.FechaRegistro.Value.Date;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public bool EliminarSocio(int idSocio)
    {
        const string consulta = """
            DELETE FROM dbo.Socios
            WHERE id_socio = @id_socio
              AND NOT EXISTS (
                  SELECT 1 FROM dbo.Prestamos WHERE id_socio = @id_socio
              );
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_socio", SqlDbType.Int).Value = idSocio;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public bool ActualizarSocio(Socios socio)
    {
        const string consulta = """
            UPDATE dbo.Socios
            SET nombre = @nombre,
                apellido = @apellido,
                correo = @correo,
                fecha_registro = @fecha_registro
            WHERE id_socio = @id_socio;
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_socio", SqlDbType.Int).Value = socio.IdSocio;
        comando.Parameters.Add("@nombre", SqlDbType.NVarChar, 100).Value = socio.Nombre;
        comando.Parameters.Add("@apellido", SqlDbType.NVarChar, 100).Value = socio.Apellido;
        comando.Parameters.Add("@correo", SqlDbType.NVarChar, 150).Value = socio.Correo;
        comando.Parameters.Add("@fecha_registro", SqlDbType.Date).Value =
            socio.FechaRegistro is null ? DBNull.Value : socio.FechaRegistro.Value.Date;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public List<Prestamos> ListarPrestamos()
    {
        const string consulta = """
            SELECT id_prestamo, id_libros, id_socio, fecha_prestamo, fecha_devolucion
            FROM dbo.Prestamos
            ORDER BY id_prestamo;
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        conexion.Open();

        using var lector = comando.ExecuteReader();
        var prestamos = new List<Prestamos>();
        while (lector.Read())
        {
            prestamos.Add(new Prestamos
            {
                IdPrestamo = lector.GetInt32(0),
                IdLibros = lector.GetInt32(1),
                IdSocio = lector.GetInt32(2),
                FechaPrestamo = lector.IsDBNull(3) ? null : lector.GetDateTime(3),
                FechaDevolucion = lector.IsDBNull(4) ? null : lector.GetDateTime(4)
            });
        }

        return prestamos;
    }

    public bool RegistrarPrestamo(Prestamos prestamo)
    {
        const string consulta = """
            INSERT INTO dbo.Prestamos (id_libros, id_socio, fecha_prestamo, fecha_devolucion)
            VALUES (@id_libros, @id_socio, @fecha_prestamo, @fecha_devolucion);
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_libros", SqlDbType.Int).Value = prestamo.IdLibros;
        comando.Parameters.Add("@id_socio", SqlDbType.Int).Value = prestamo.IdSocio;
        comando.Parameters.Add("@fecha_prestamo", SqlDbType.Date).Value =
            prestamo.FechaPrestamo is null ? DBNull.Value : prestamo.FechaPrestamo.Value.Date;
        comando.Parameters.Add("@fecha_devolucion", SqlDbType.Date).Value =
            prestamo.FechaDevolucion is null ? DBNull.Value : prestamo.FechaDevolucion.Value.Date;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public bool EliminarPrestamo(int idPrestamo)
    {
        const string consulta = "DELETE FROM dbo.Prestamos WHERE id_prestamo = @id_prestamo;";

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_prestamo", SqlDbType.Int).Value = idPrestamo;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }

    public bool ActualizarPrestamo(Prestamos prestamo)
    {
        const string consulta = """
            UPDATE dbo.Prestamos
            SET id_libros = @id_libros,
                id_socio = @id_socio,
                fecha_prestamo = @fecha_prestamo,
                fecha_devolucion = @fecha_devolucion
            WHERE id_prestamo = @id_prestamo;
            """;

        using var conexion = new SqlConnection(ConexioBd.CadenaConexion);
        using var comando = new SqlCommand(consulta, conexion);
        comando.Parameters.Add("@id_prestamo", SqlDbType.Int).Value = prestamo.IdPrestamo;
        comando.Parameters.Add("@id_libros", SqlDbType.Int).Value = prestamo.IdLibros;
        comando.Parameters.Add("@id_socio", SqlDbType.Int).Value = prestamo.IdSocio;
        comando.Parameters.Add("@fecha_prestamo", SqlDbType.Date).Value =
            prestamo.FechaPrestamo is null ? DBNull.Value : prestamo.FechaPrestamo.Value.Date;
        comando.Parameters.Add("@fecha_devolucion", SqlDbType.Date).Value =
            prestamo.FechaDevolucion is null ? DBNull.Value : prestamo.FechaDevolucion.Value.Date;
        conexion.Open();

        return comando.ExecuteNonQuery() == 1;
    }
}
