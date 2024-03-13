
CREATE DATABASE DB_LIBRERIA;

USE DB_LIBRERIA;

---- ENTIDADES
BEGIN

-- Usuarios.
CREATE TABLE Clientes (
    id UNIQUEIDENTIFIER DEFAULT NEWID(),
    nombre VARCHAR(64),
    apellido VARCHAR(64),
    correo VARCHAR(100) UNIQUE,
    matricula INT UNIQUE,
    foto_perfil VARCHAR(255) -- Por si acaso: https://es.stackoverflow.com/questions/253549/como-guardar-la-imagen-de-un-perfildel-usuario-en-una-bd-en-mysql
);

---- Ahora atendemos a las entidades correspondientes a los libros.

-- Categorias.
CREATE TABLE Categorias (
    id UNIQUEIDENTIFIER DEFAULT NEWID(),
    nombre VARCHAR(64)
);

-- Autores.
CREATE TABLE Autores (
    id UNIQUEIDENTIFIER DEFAULT NEWID(),
    nombre VARCHAR(64),
	apellido VARCHAR(64),
	pais VARCHAR(64),
    descripcion VARCHAR(255)
);

-- Libros.
-- Tiene relacion con el autor y una categoria.
CREATE TABLE Libros (
    id UNIQUEIDENTIFIER DEFAULT NEWID(),
    nombre VARCHAR(64),
	paginas int,
	descripcion VARCHAR(64),
	edicion int,
	--editorial VARCHAR(64),
    fecha_publicacion DATE,
    autor_id UNIQUEIDENTIFIER,
    categoria_id UNIQUEIDENTIFIER,
	editorial_id UNIQUEIDENTIFIER
	CONSTRAINT fk_libro_categoria FOREIGN KEY (categoria_id) REFERENCES Categorias(id),
	CONSTRAINT fk_libro_libro FOREIGN KEY (autor_id) REFERENCES Autores(id),
	CONSTRAINT fk_libro_editorial FOREIGN KEY(editorial_id) References Editorial(id)
);

-- Prestamos.
-- Tiene relacion con un libro y el usuario que lo solicito.
CREATE TABLE Prestamos (
    id UNIQUEIDENTIFIER DEFAULT NEWID(),
    fecha_prestamo DATE,
	fecha_entrega DATE,
    estado VARCHAR(20),
    cliente_id UNIQUEIDENTIFIER,
    libro_id UNIQUEIDENTIFIER,
	CONSTRAINT fk_prestamo_usuario FOREIGN KEY (cliente_id) REFERENCES Clientes(id),
	CONSTRAINT fk_prestamo_libro FOREIGN KEY (libro_id) REFERENCES Libros(id)
);

-- Reportes.
-- Una unica relacion con el usuario puede ayudarnos a recoger otros datos,
-- pero si ven necesaria alguna otra relacion, se puede discutir.
CREATE TABLE Reportes (
    id UNIQUEIDENTIFIER DEFAULT NEWID(),
    fecha_reporte DATE,
    cliente_id UNIQUEIDENTIFIER,
	CONSTRAINT fk_reporte_usuario FOREIGN KEY (cliente_id) REFERENCES Clientes(id),
);

Create Table Editorial(
	id UNIQUEIDENTIFIER DEFAULT NEWID(),
	nombre varchar(64)

);

END;

---- PROCEDIMIENTOS ALMACENADOS : Usuario
BEGIN

-- Usuario : Crear.
CREATE PROCEDURE spUsuarioCrear
    @nombre VARCHAR(64),
    @apellido VARCHAR(64),
    @correo VARCHAR(100),
    @contrasena VARCHAR(32),
    @matricula INT,
    @tipo_usuario VARCHAR(18),
    @foto_perfil VARCHAR(255)
AS
BEGIN
	-- NOTA: En esta primera parte se podrian añadir condicionales para definir si el usuario o matricula ya existen en la DB
	-- Si estan de acuerdo con esa cuestion, claro, o si es mejor hacerlo en el proyecto con C#
    INSERT INTO tbUsuarios(nombre, apellido, correo, contrasena, matricula, tipo_usuario, foto_perfil)
    VALUES (@nombre, @apellido, @correo, @contrasena, @matricula, @tipo_usuario, @foto_perfil);
END;

-- Usuario : Actualizar.
CREATE PROCEDURE spUsuarioActualizar
	@guid_id UNIQUEIDENTIFIER,
    @nombre VARCHAR(64),
    @apellido VARCHAR(64),
    @correo VARCHAR(100),
    @contrasena VARCHAR(32),
    @matricula INT,
    @tipo_usuario VARCHAR(18),
    @foto_perfil VARCHAR(255)
AS
BEGIN
	-- En esta primera parte se podrian añadir condicionales para definir si el usuario o matricula ya existen en la DB
    UPDATE tbUsuarios
    SET nombre = @nombre, apellido = @apellido, correo = @correo,
	contrasena = @contrasena, matricula = @matricula, tipo_usuario = @tipo_usuario, foto_perfil = @foto_perfil
	WHERE id = @guid_id;
END;

-- Usuario : Buscar.
-- Con el Where se intentan usar los argumentos para que se pueda filtrar la busqueda por nombre o matricula.
CREATE PROCEDURE spUsuarioBuscar
    @usuario_nombre VARCHAR(64) = NULL,
    @usuario_matricula INT = NULL,
    @libro_nombre VARCHAR(64) = NULL
AS
BEGIN
    SELECT *
    FROM tbUsuarios
    WHERE (nombre LIKE '%' + COALESCE(@usuario_nombre, nombre) + '%' OR @usuario_nombre IS NULL)
    OR (matricula = COALESCE(@usuario_matricula, matricula) OR @usuario_matricula IS NULL);
END;

-- Usuario : Eliminar.
-- Probabilidad de que aqui podria hacerse un metodo general para que cada entidad use la misma funcion
-- ya dentro del proyecto C#, asi hay menos codigo aqui.
CREATE PROCEDURE spUsuarioEliminar
    @guid_id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE tbUsuarios WHERE id = @guid_id;
END;

---- NOTAS:

-- 1
-- Puede que sea necesario un procedimiento almacenado para obtener el * Historial de Préstamos *. Para cumplir con el
-- rendimiento, talvez deberiamos evaluar si es mas rapido un procedimiento almacenado o una funcion / metodo
-- mas riguroso dentro del proyecto con C#.

-- 2
-- Falta hacer los procedimientos almacenados del resto de entidades.
-- Ahora, si no les gusta tanto codigo, talvez podriamos hacer una funcion / metodo generico dentro del proyecto C#
-- Que pueda abarcar los distintos procedimientos almacenados, con una cantidad de lineas de codigo menor.

END

---- PROCEDIMIENTOS ALMACENADOS : Categoria
BEGIN

-- Crear
CREATE PROCEDURE spCategoriaCrear
    @nombre VARCHAR(64)
AS
BEGIN
    INSERT INTO tbCategorias(nombre) VALUES (@nombre);
END;

-- Buscar
CREATE PROCEDURE spCategoriaBuscar
    @nombre VARCHAR(64)
AS
BEGIN
    SELECT * FROM tbCategorias WHERE nombre LIKE @nombre;
END;

-- Modificar
CREATE PROCEDURE spCategoriaModificar
    @guid_id UNIQUEIDENTIFIER,
    @nombre VARCHAR(64)
AS
BEGIN
    UPDATE tbCategorias SET nombre = @nombre WHERE id = @guid_id;
END;

-- Eliminar
CREATE PROCEDURE spCategoriaEliminar
    @guid_id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM tbCategorias WHERE id = @guid_id;
END;

END

---- PROCEDIMIENTOS ALMACENADOS : Autor
BEGIN

-- Crear
CREATE PROCEDURE spAutorCrear
    @nombre VARCHAR(64),
    @descripcion VARCHAR(255)
AS
BEGIN
    INSERT INTO tbAutores(nombre, descripcion) VALUES (@nombre, @descripcion);
END;

-- Buscar
CREATE PROCEDURE spAutorBuscar
    @nombre VARCHAR(64)
AS
BEGIN
    SELECT * FROM tbAutores WHERE nombre LIKE @nombre;
END;

-- Modificar
CREATE PROCEDURE spAutorModificar
    @guid_id UNIQUEIDENTIFIER,
    @nombre VARCHAR(64),
    @descripcion VARCHAR(255)
AS
BEGIN
    UPDATE tbAutores SET nombre = @nombre, descripcion = @descripcion WHERE id = @guid_id;
END;

-- Eliminar
CREATE PROCEDURE spAutorEliminar
    @guid_id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM tbAutores WHERE id = @guid_id;
END;

END

---- PROCEDIMIENTOS ALMACENADOS : Libro
BEGIN

-- Crear
CREATE PROCEDURE spLibroCrear
    @nombre VARCHAR(64),
    @fecha_publicacion DATE,
    @autor_id UNIQUEIDENTIFIER,
    @categoria_id UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO tbLibros(nombre, fecha_publicacion, autor_id, categoria_id) VALUES (@nombre, @fecha_publicacion, @autor_id, @categoria_id);
END;

-- Buscar
CREATE PROCEDURE spLibroBuscar
    @nombre VARCHAR(64)
AS
BEGIN
    SELECT * FROM tbLibros WHERE nombre LIKE @nombre;
END;

-- Modificar
CREATE PROCEDURE spLibroModificar
    @guid_id UNIQUEIDENTIFIER,
    @nombre VARCHAR(64),
    @fecha_publicacion DATE,
    @autor_id UNIQUEIDENTIFIER,
    @categoria_id UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE tbLibros SET nombre = @nombre, fecha_publicacion = @fecha_publicacion, autor_id = @autor_id, categoria_id = @categoria_id
	WHERE id = @guid_id;
END;

-- Eliminar
CREATE PROCEDURE spLibroEliminar
    @guid_id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM tbLibros WHERE id = @guid_id;
END;

END

---- PROCEDIMIENTOS ALMACENADOS : Prestamos
BEGIN

-- Crear
CREATE PROCEDURE spPrestamosCrear
    @fecha_prestamo DATE,
    @estado VARCHAR(20),
    @usuario_id UNIQUEIDENTIFIER,
    @libro_id UNIQUEIDENTIFIER
AS
BEGIN
    INSERT INTO tbPrestamos(fecha_prestamo, estado, usuario_id, libro_id) VALUES (@fecha_prestamo, @estado, @usuario_id, @libro_id);
END;

-- Buscar
CREATE PROCEDURE spPrestamosBuscarUsuarioMatricula
    @usuario_id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM tbPrestamos WHERE usuario_id = @usuario_id;
END;

-- Modificar
CREATE PROCEDURE spPrestamosActualizar
    @guid_id UNIQUEIDENTIFIER,
    @fecha_prestamo DATE,
    @estado VARCHAR(20),
    @usuario_id UNIQUEIDENTIFIER,
    @libro_id UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE tbPrestamos SET fecha_prestamo = @fecha_prestamo, estado = @estado, usuario_id = @usuario_id, libro_id = @libro_id WHERE id = @guid_id;
END;

-- Eliminar
CREATE PROCEDURE spPrestamosEliminar
    @guid_id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM tbPrestamos WHERE id = @guid_id;
END;

END

---- PROCEDIMIENTOS ALMACENADOS : Reportes
BEGIN

-- Crear
CREATE PROCEDURE spReportesCrear
    @usuario_id UNIQUEIDENTIFIER,
    @fecha_reporte DATE
AS
BEGIN
    INSERT INTO tbReportes(fecha_reporte, usuario_id) VALUES (@fecha_reporte, @usuario_id);
END;

-- Buscar
CREATE PROCEDURE spReportesBuscarUsuarioID
    @usuario_id UNIQUEIDENTIFIER
AS
BEGIN
    SELECT * FROM tbReportes WHERE usuario_id = @usuario_id;
END;

-- Modificar
CREATE PROCEDURE spReportesActualizar
    @guid_id UNIQUEIDENTIFIER,
    @fecha_reporte DATE,
    @usuario_id UNIQUEIDENTIFIER
AS
BEGIN
    UPDATE tbReportes SET fecha_reporte = @fecha_reporte, usuario_id = @usuario_id WHERE id = @guid_id;
END;

-- Eliminar
CREATE PROCEDURE spReportesDelete
    @guid_id UNIQUEIDENTIFIER
AS
BEGIN
    DELETE FROM tbReportes WHERE id = @guid_id;
END;

END