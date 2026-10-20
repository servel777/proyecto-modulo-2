namespace Biblioteca.Datos;

public static class ConexioBd
{
    public static string CadenaConexion =>
        Environment.GetEnvironmentVariable("BIBLIOTECA_CONNECTION_STRING")
        ?? "Server=localhost;Database=BibliotecaBD;Trusted_Connection=True;TrustServerCertificate=True;";
}
