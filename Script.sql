IF DB_ID(N'BibliotecaBD') IS NULL
    CREATE DATABASE BibliotecaBD;
GO

USE BibliotecaBD;
GO

IF OBJECT_ID(N'dbo.Libros', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Libros
    (
        id_libros INT IDENTITY(1,1) PRIMARY KEY,
        titulo NVARCHAR(150) NOT NULL,
        autor NVARCHAR(100) NOT NULL,
        anio_publicacion INT NOT NULL,
        stock INT NOT NULL CONSTRAINT DF_Libros_stock DEFAULT 1
    );
END;
GO

IF OBJECT_ID(N'dbo.Socios', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Socios
    (
        id_socio INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(100) NOT NULL,
        apellido NVARCHAR(100) NOT NULL,
        correo NVARCHAR(150) NOT NULL,
        fecha_registro DATE NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Prestamos', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Prestamos
    (
        id_prestamo INT IDENTITY(1,1) PRIMARY KEY,
        id_libros INT NOT NULL,
        id_socio INT NOT NULL,
        fecha_prestamo DATE NULL,
        fecha_devolucion DATE NULL,
        CONSTRAINT FK_Prestamos_Libros FOREIGN KEY (id_libros)
            REFERENCES dbo.Libros (id_libros),
        CONSTRAINT FK_Prestamos_Socios FOREIGN KEY (id_socio)
            REFERENCES dbo.Socios (id_socio)
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Libros)
BEGIN
    INSERT INTO dbo.Libros (titulo, autor, anio_publicacion, stock)
    VALUES
        (N'cien anios de soledad', N'Gabriel Garcia Marquez', 1967, 3),
        (N'don quijote de la Mancha', N'Miguel de Cervantes', 1605, 2),
        (N'1984', N'George Orwell', 1948, 1),
        (N'el principito', N'antoine de Saint-Exupery', 1943, 4),
        (N'Los Buddenbrook', N'Thomas Mann', 1901, 4),
        (N'el cuaderno dorado', N'Doris Lessing', 1962, 5),
        (N'Paraiso', N'Abdulrazak Gurnah', 2021, 4),
        (N'La Colmena', N'Camilo Cela', 1951, 1),
        (N'voces de chernobil', N'Svetlana Alexievich', 2015, 5),
        (N'Ensayo sobre la ceguera', N'Jose Saramago', 1995, 5);
END;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Socios)
BEGIN
    INSERT INTO dbo.Socios (nombre, apellido, correo)
    VALUES
        (N'juan', N'perez', N'jperez@gmail.com'),
        (N'jorge', N'lopez', N'jorgelp@gmail.com'),
        (N'romelia', N'casas', N'romcasas@gmail.com'),
        (N'roberta', N'mallku', N'rmallku@gmail.com'),
        (N'ricardo', N'gottret', N'ricgot@gmail.com'),
        (N'julia', N'zenteno', N'jzenteno@gmail.com'),
        (N'ivan', N'lopez', N'ivan_lopez@gmail.com'),
        (N'alfredo', N'laura', N'alfalura476@gmail.com'),
        (N'marcelo', N'coaquira', N'mcoaquira@gmail.com'),
        (N'adrian', N'quisbert', N'aquisbert@gmail.com');
END;
GO
