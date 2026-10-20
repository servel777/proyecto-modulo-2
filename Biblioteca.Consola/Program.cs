using System.Globalization;
using Biblioteca.Datos;
using Biblioteca.Entidades;

var datos = new ProductosDatos();
var salir = false;

while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("======= Biblioteca =======");
    Console.WriteLine("1. Listar libros");
    Console.WriteLine("2. Registrar libro");
    Console.WriteLine("3. Salir");
    Console.WriteLine("4. Eliminar libro");
    Console.WriteLine("5. Actualizar libro");
    Console.WriteLine("6. Listar socios");
    Console.WriteLine("7. Registrar socio");
    Console.WriteLine("8. Eliminar socio");
    Console.WriteLine("9. Actualizar socio");
    Console.WriteLine("10. Listar prestamos");
    Console.WriteLine("11. Registrar prestamo");
    Console.WriteLine("12. Eliminar prestamo");
    Console.WriteLine("13. Actualizar prestamo");
    Console.Write("Seleccione una opcion: ");

    if (!int.TryParse(Console.ReadLine(), out int opcion))
    {
        Console.WriteLine("Ingrese una opcion numerica.");
        continue;
    }

    try
    {
        switch (opcion)
        {
            case 1:
                MostrarLibros(datos);
                break;
            case 2:
                RegistrarLibro(datos);
                break;
            case 3:
                salir = true;
                Console.WriteLine("Saliendo del programa.");
                break;
            case 4:
                EliminarLibro(datos);
                break;
            case 5:
                ActualizarLibro(datos);
                break;
            case 6:
                MostrarSocios(datos);
                break;
            case 7:
                RegistrarSocio(datos);
                break;
            case 8:
                EliminarSocio(datos);
                break;
            case 9:
                ActualizarSocio(datos);
                break;
            case 10:
                MostrarPrestamos(datos);
                break;
            case 11:
                RegistrarPrestamo(datos);
                break;
            case 12:
                EliminarPrestamo(datos);
                break;
            case 13:
                ActualizarPrestamo(datos);
                break;
            default:
                Console.WriteLine("Seleccione una opcion entre 1 y 13.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"No se pudo completar la operacion: {ex.Message}");
    }
}

static void MostrarLibros(ProductosDatos datos)
{
    List<Libros> libros = datos.ListarLibros();
    Console.WriteLine();
    Console.WriteLine("============ Libros ============");
    if (libros.Count == 0)
    {
        Console.WriteLine("No hay libros registrados.");
        return;
    }

    foreach (Libros libro in libros)
    {
        Console.WriteLine($"Id: {libro.IdLibros}");
        Console.WriteLine($"Titulo: {libro.Titulo}");
        Console.WriteLine($"Autor: {libro.Autor}");
        Console.WriteLine($"Anio de publicacion: {libro.AnioPublicacion}");
        Console.WriteLine($"Stock: {libro.Stock}");
        Console.WriteLine("--------------------------------");
    }

    Console.WriteLine($"Total de libros: {libros.Count}");
}

static void RegistrarLibro(ProductosDatos datos)
{
    var libro = new Libros
    {
        Titulo = LeerTexto("Titulo: "),
        Autor = LeerTexto("Autor: "),
        AnioPublicacion = LeerEntero("Anio de publicacion: ", 1),
        Stock = LeerEntero("Stock: ", 0)
    };

    Console.WriteLine(datos.RegistrarLibro(libro)
        ? "Libro registrado correctamente."
        : "No se registro el libro.");
}

static void EliminarLibro(ProductosDatos datos)
{
    int id = LeerEntero("Id del libro que desea eliminar: ", 1);
    if (!ConfirmarEliminacion())
    {
        Console.WriteLine("Eliminacion cancelada.");
        return;
    }

    Console.WriteLine(datos.EliminarLibro(id)
        ? "Libro eliminado correctamente."
        : "No existe el libro o tiene prestamos asociados y no se puede eliminar.");
}

static void ActualizarLibro(ProductosDatos datos)
{
    var libro = new Libros
    {
        IdLibros = LeerEntero("Id del libro que desea actualizar: ", 1),
        Titulo = LeerTexto("Nuevo titulo: "),
        Autor = LeerTexto("Nuevo autor: "),
        AnioPublicacion = LeerEntero("Nuevo anio de publicacion: ", 1),
        Stock = LeerEntero("Nuevo stock: ", 0)
    };

    Console.WriteLine(datos.ActualizarLibro(libro)
        ? "Libro actualizado correctamente."
        : "No existe un libro con ese id.");
}

static void MostrarSocios(ProductosDatos datos)
{
    List<Socios> socios = datos.ListarSocios();
    Console.WriteLine();
    Console.WriteLine("============ Socios ============");
    if (socios.Count == 0)
    {
        Console.WriteLine("No hay socios registrados.");
        return;
    }

    foreach (Socios socio in socios)
    {
        Console.WriteLine($"Id: {socio.IdSocio}");
        Console.WriteLine($"Nombre: {socio.Nombre}");
        Console.WriteLine($"Apellido: {socio.Apellido}");
        Console.WriteLine($"Correo: {socio.Correo}");
        Console.WriteLine($"Fecha de registro: {FormatearFecha(socio.FechaRegistro)}");
        Console.WriteLine("--------------------------------");
    }

    Console.WriteLine($"Total de socios: {socios.Count}");
}

static void RegistrarSocio(ProductosDatos datos)
{
    var socio = new Socios
    {
        Nombre = LeerTexto("Nombre: "),
        Apellido = LeerTexto("Apellido: "),
        Correo = LeerCorreo(),
        FechaRegistro = LeerFechaOpcional("Fecha de registro (AAAA-MM-DD, vacio si no aplica): ")
    };

    Console.WriteLine(datos.RegistrarSocio(socio)
        ? "Socio registrado correctamente."
        : "No se registro el socio.");
}

static void EliminarSocio(ProductosDatos datos)
{
    int id = LeerEntero("Id del socio que desea eliminar: ", 1);
    if (!ConfirmarEliminacion())
    {
        Console.WriteLine("Eliminacion cancelada.");
        return;
    }

    Console.WriteLine(datos.EliminarSocio(id)
        ? "Socio eliminado correctamente."
        : "No existe el socio o tiene prestamos asociados y no se puede eliminar.");
}

static void ActualizarSocio(ProductosDatos datos)
{
    var socio = new Socios
    {
        IdSocio = LeerEntero("Id del socio que desea actualizar: ", 1),
        Nombre = LeerTexto("Nuevo nombre: "),
        Apellido = LeerTexto("Nuevo apellido: "),
        Correo = LeerCorreo(),
        FechaRegistro = LeerFechaOpcional("Nueva fecha de registro (AAAA-MM-DD, vacio si no aplica): ")
    };

    Console.WriteLine(datos.ActualizarSocio(socio)
        ? "Socio actualizado correctamente."
        : "No existe un socio con ese id.");
}

static void MostrarPrestamos(ProductosDatos datos)
{
    List<Prestamos> prestamos = datos.ListarPrestamos();
    Console.WriteLine();
    Console.WriteLine("============ Prestamos ============");
    if (prestamos.Count == 0)
    {
        Console.WriteLine("No hay prestamos registrados.");
        return;
    }

    foreach (Prestamos prestamo in prestamos)
    {
        Console.WriteLine($"Id del prestamo: {prestamo.IdPrestamo}");
        Console.WriteLine($"Id del libro: {prestamo.IdLibros}");
        Console.WriteLine($"Id del socio: {prestamo.IdSocio}");
        Console.WriteLine($"Fecha del prestamo: {FormatearFecha(prestamo.FechaPrestamo)}");
        Console.WriteLine($"Fecha de devolucion: {FormatearFecha(prestamo.FechaDevolucion)}");
        Console.WriteLine("--------------------------------");
    }

    Console.WriteLine($"Total de prestamos: {prestamos.Count}");
}

static void RegistrarPrestamo(ProductosDatos datos)
{
    var prestamo = new Prestamos
    {
        IdLibros = LeerEntero("Id del libro: ", 1),
        IdSocio = LeerEntero("Id del socio: ", 1),
        FechaPrestamo = LeerFechaObligatoria("Fecha del prestamo (AAAA-MM-DD): "),
        FechaDevolucion = LeerFechaOpcional("Fecha de devolucion (AAAA-MM-DD, vacio si aun no se devuelve): ")
    };

    Console.WriteLine(datos.RegistrarPrestamo(prestamo)
        ? "Prestamo registrado correctamente."
        : "No se registro el prestamo.");
}

static void EliminarPrestamo(ProductosDatos datos)
{
    int id = LeerEntero("Id del prestamo que desea eliminar: ", 1);
    if (!ConfirmarEliminacion())
    {
        Console.WriteLine("Eliminacion cancelada.");
        return;
    }

    Console.WriteLine(datos.EliminarPrestamo(id)
        ? "Prestamo eliminado correctamente."
        : "No existe un prestamo con ese id.");
}

static void ActualizarPrestamo(ProductosDatos datos)
{
    var prestamo = new Prestamos
    {
        IdPrestamo = LeerEntero("Id del prestamo que desea actualizar: ", 1),
        IdLibros = LeerEntero("Nuevo id del libro: ", 1),
        IdSocio = LeerEntero("Nuevo id del socio: ", 1),
        FechaPrestamo = LeerFechaObligatoria("Nueva fecha del prestamo (AAAA-MM-DD): "),
        FechaDevolucion = LeerFechaOpcional("Nueva fecha de devolucion (AAAA-MM-DD, vacio si aun no se devuelve): ")
    };

    Console.WriteLine(datos.ActualizarPrestamo(prestamo)
        ? "Prestamo actualizado correctamente."
        : "No existe un prestamo con ese id.");
}

static string LeerTexto(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string valor = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(valor))
        {
            return valor;
        }

        Console.WriteLine("El valor no puede estar vacio.");
    }
}

static string LeerCorreo()
{
    while (true)
    {
        string correo = LeerTexto("Correo: ");
        if (correo.Contains('@') && correo.IndexOf('@') > 0 &&
            correo.IndexOf('@') < correo.LastIndexOf('.') &&
            correo.LastIndexOf('.') < correo.Length - 1)
        {
            return correo;
        }

        Console.WriteLine("Ingrese un correo valido.");
    }
}

static int LeerEntero(string mensaje, int minimo)
{
    while (true)
    {
        Console.Write(mensaje);
        if (int.TryParse(Console.ReadLine(), out int valor) && valor >= minimo)
        {
            return valor;
        }

        Console.WriteLine($"Ingrese un numero entero igual o mayor que {minimo}.");
    }
}

static DateTime LeerFechaObligatoria(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        if (DateTime.TryParseExact(
            Console.ReadLine(),
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime fecha))
        {
            return fecha.Date;
        }

        Console.WriteLine("Ingrese una fecha con formato AAAA-MM-DD.");
    }
}

static DateTime? LeerFechaOpcional(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string valor = Console.ReadLine()?.Trim() ?? string.Empty;
        if (valor.Length == 0)
        {
            return null;
        }

        if (DateTime.TryParseExact(
            valor,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime fecha))
        {
            return fecha.Date;
        }

        Console.WriteLine("Ingrese una fecha con formato AAAA-MM-DD o deje el campo vacio.");
    }
}

static string FormatearFecha(DateTime? fecha) =>
    fecha?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) ?? "Sin fecha";

static bool ConfirmarEliminacion()
{
    Console.Write("Confirma la eliminacion? (s/n): ");
    return (Console.ReadLine() ?? string.Empty).Trim()
        .Equals("s", StringComparison.OrdinalIgnoreCase);
}
