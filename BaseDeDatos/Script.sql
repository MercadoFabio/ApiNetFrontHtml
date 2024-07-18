-- Crear la base de datos
CREATE DATABASE DbAa579fProg3w2Context;
GO

-- Usar la base de datos recién creada
USE DbAa579fProg3w2Context;
GO

-- Crear tablas
CREATE TABLE TiposObra (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL
);

CREATE TABLE Obras (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    DatosVarios NVARCHAR(MAX) NOT NULL,
    IdTipoObra UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Obras_TiposObra FOREIGN KEY (IdTipoObra) REFERENCES TiposObra(Id)
);

CREATE TABLE Albaniles (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Nombre NVARCHAR(255) NOT NULL,
    Apellido NVARCHAR(255) NOT NULL,
    Dni NVARCHAR(50) NOT NULL,
    Telefono NVARCHAR(50) NULL,
    Calle NVARCHAR(255) NULL,
    Numero NVARCHAR(50) NULL,
    CodPost NVARCHAR(50) NULL,
    Activo BIT NOT NULL,
    FechaAlta DATETIME NULL
);

CREATE TABLE AlbanilesXObra (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    IdAlbanil UNIQUEIDENTIFIER NOT NULL,
    IdObra UNIQUEIDENTIFIER NOT NULL,
    TareaArealizar NVARCHAR(MAX) NOT NULL,
    FechaAlta DATETIME NULL,
    CONSTRAINT FK_AlbanilesXObra_Albanil FOREIGN KEY (IdAlbanil) REFERENCES Albaniles(Id),
    CONSTRAINT FK_AlbanilesXObra_Obra FOREIGN KEY (IdObra) REFERENCES Obras(Id)
);

-- Inserciones de ejemplo

-- Insertar en TiposObra
INSERT INTO TiposObra (Id, Nombre) VALUES
(NEWID(), 'Edificio');

-- Insertar en Obras
DECLARE @EdificioId UNIQUEIDENTIFIER = (SELECT Id FROM TiposObra WHERE Nombre = 'Edificio');

INSERT INTO Obras (Id, Nombre, DatosVarios, IdTipoObra) VALUES
(NEWID(), 'Obra1', 'Datos varios de la obra 1', @EdificioId),
(NEWID(), 'Obra2', 'Datos varios de la obra 2', @EdificioId);

-- Insertar en Albaniles
INSERT INTO Albaniles (Id, Nombre, Apellido, Dni, Telefono, Calle, Numero, CodPost, Activo, FechaAlta) VALUES
(NEWID(), 'Juan', 'Perez', '12345678', '123456789', 'Calle 1', '123', '1000', 1, GETDATE()),
(NEWID(), 'Ana', 'Garcia', '87654321', '987654321', 'Calle 2', '456', '2000', 1, GETDATE());

-- Insertar en AlbanilesXObra
DECLARE @Albanil1Id UNIQUEIDENTIFIER = (SELECT Id FROM Albaniles WHERE Nombre = 'Juan' AND Apellido = 'Perez');
DECLARE @Albanil2Id UNIQUEIDENTIFIER = (SELECT Id FROM Albaniles WHERE Nombre = 'Ana' AND Apellido = 'Garcia');
DECLARE @Obra1Id UNIQUEIDENTIFIER = (SELECT Id FROM Obras WHERE Nombre = 'Obra1');
DECLARE @Obra2Id UNIQUEIDENTIFIER = (SELECT Id FROM Obras WHERE Nombre = 'Obra2');

INSERT INTO AlbanilesXObra (Id, IdAlbanil, IdObra, TareaArealizar, FechaAlta) VALUES
(NEWID(), @Albanil1Id, @Obra1Id, 'Tarea 1 a realizar', GETDATE()),
(NEWID(), @Albanil2Id, @Obra2Id, 'Tarea 2 a realizar', GETDATE());
