CREATE DATABASE dueno_mascota
ON PRIMARY 
(
    NAME = 'dueno_mascota_data',  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\dueno_mascota.mdf',  
    SIZE = 100MB,  
    FILEGROWTH = 30% 
)
LOG ON  
(
    NAME = 'dueno_mascota_log',  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\dueno_mascota.ldf',  
    SIZE = 50MB,  
    FILEGROWTH = 15%
);
GO

USE dueno_mascota
GO

CREATE TABLE persona (
    id_persona INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    nombre NVARCHAR(50) NOT NULL, 
	apellido NVARCHAR(50) NOT NULL, 
	identificacion NVARCHAR(50) NOT NULL,
    fecha_nacimiento DATE NOT NULL,
	edad INT NOT NULL,
    estatura DECIMAL(4,2) NOT NULL,         
    direccion NVARCHAR(100) NOT NULL,                             
    telefono NVARCHAR(15) NOT NULL                             
);

GO
CREATE TABLE mascota (
    id_mascota INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(50) NOT NULL,     
    especie NVARCHAR(30) NOT NULL,                     
    raza NVARCHAR(50) NOT NULL,                          
    color NVARCHAR(30) NOT NULL,                        
    edad INT NOT NULL,                                   
    id_persona INT NOT NULL
);

ALTER TABLE mascota
ADD CONSTRAINT FK_dueno_mascota
FOREIGN KEY(id_persona)
REFERENCES persona(id_persona)

GO

CREATE PROCEDURE sp_crud_persona
(
    @accion CHAR(1) = NULL,
    @idP INT = NULL,
    @nombre VARCHAR(50) = NULL,
	@apellido VARCHAR(50) = NULL,
	@cedula NVARCHAR(10) = NULL,
    @fnac DATE = NULL,
	@edad INT = NULL,
    @estatura DECIMAL(4, 2) = NULL,
    @direccion VARCHAR(100) = NULL,
    @telefono NVARCHAR(10) = NULL
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Registrar
        IF @accion = 'I'
        BEGIN
			INSERT INTO persona (nombre, apellido, identificacion, fecha_nacimiento, edad, estatura, direccion, telefono)
			VALUES (@nombre, @apellido, @cedula, @fnac, @edad, @estatura, @direccion, @telefono);
        END

        -- Actualizar
        ELSE IF @accion = 'U'
        BEGIN
            UPDATE persona SET nombre = @nombre, apellido = @apellido, identificacion = @cedula, fecha_nacimiento = @fnac, edad = @edad, 
			estatura = @estatura, direccion = @direccion, telefono = @telefono WHERE id_persona = @idP;
        END

        -- Eliminar
        ELSE IF @accion = 'D'
        BEGIN
            DELETE FROM persona WHERE id_persona = @idP;
        END

        -- Consulta por ID
        ELSE IF @accion = 'C'
        BEGIN
            SELECT * FROM persona WHERE id_persona = @idP;
        END

        -- Consultar
        ELSE IF @accion = 'G'
        BEGIN
            SELECT * FROM persona;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE sp_crud_mascota
(
    @accion CHAR(1) = NULL,
    @idM INT = NULL,
    @nombre NVARCHAR(50) = NULL,
    @especie NVARCHAR(30) = NULL,
    @raza NVARCHAR(50) = NULL,
    @color NVARCHAR(30) = NULL,
    @edad INT = NULL,
    @idP INT = NULL
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Registrar
        IF @accion = 'I'
        BEGIN
			INSERT INTO mascota (nombre, especie, raza, color, edad, id_persona) VALUES (@nombre, @especie, @raza, @color, @edad, @idP);
        END

        -- Actualizar
        ELSE IF @accion = 'U'
        BEGIN
            UPDATE mascota SET nombre = @nombre, especie = @especie, raza = @raza, color = @color, edad = @edad WHERE id_mascota = @idM;
        END

        -- Eliminar
        ELSE IF @accion = 'D'
        BEGIN
            DELETE FROM mascota WHERE id_mascota = @idM;
        END

        -- Consulta por ID
        ELSE IF @accion = 'C'
        BEGIN
            SELECT m.id_mascota, m.nombre as nombre_mascota, m.especie, m.raza, m.color, m.edad, p.*, concat(p.nombre, ' ', p.apellido) as dueno FROM mascota m 
			INNER JOIN persona p ON m.id_persona = p.id_persona WHERE m.id_mascota = @idM;
        END

        -- Consultar
        ELSE IF @accion = 'G'
        BEGIN
            SELECT m.id_mascota, m.nombre as nombre_mascota, m.especie, m.raza, m.color, m.edad, p.*, concat(p.nombre, ' ', p.apellido) as dueno FROM mascota m 
			INNER JOIN persona p ON m.id_persona = p.id_persona 
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


