CREATE DATABASE db_hotel;
GO

USE db_hotel;

CREATE TABLE [Habitaciones] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Camas] INT NOT NULL DEFAULT 1,
    [Estado] BIT NOT NULL DEFAULT 0,
	[Imagen] NVARCHAR(100) NOT NULL,
);
GO

CREATE TABLE [Recepcionistas] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Carnet] NVARCHAR(10) NOT NULL UNIQUE ,
    [Nombre] NVARCHAR(100) NOT NULL,
    [Salario] FLOAT NOT NULL DEFAULT 0
);
GO

CREATE TABLE [Opiniones] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Opcion] NVARCHAR(30) DEFAULT NULL,
    [Cantidad] SMALLINT DEFAULT 0,
);
GO

CREATE TABLE [Clientes] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Cedula] NVARCHAR(20) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(65) NOT NULL,
	[Opinion] INT NOT NULL REFERENCES [Opiniones] ([Id])
);
GO

CREATE TABLE [Reservas] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Codigo] NVARCHAR(20) NOT NULL UNIQUE,
    [Fecha_entrada] SMALLDATETIME NOT NULL,
    [Fecha_salida] SMALLDATETIME NOT NULL,
    [Recepcionista] INT NOT NULL REFERENCES [Recepcionistas] ([Id]),
    [Cliente] INT NOT NULL REFERENCES [Clientes] ([Id]),
    [Habitacion] INT NOT NULL REFERENCES [Habitaciones] ([Id])
);
GO

CREATE TABLE [Servicios] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [Tipo] NVARCHAR(50) NOT NULL,
    [Tarifa] FLOAT NOT NULL,
	[Descripcion] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [Servicios_Reservas] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Codigo] NVARCHAR(50) NOT NULL,
    [Servicio] INT NOT NULL REFERENCES [Servicios] ([Id]),
    [Reserva] INT NOT NULL REFERENCES [Reservas] ([Id])
);
GO

CREATE TABLE [Promociones] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Codigo] NVARCHAR(50) NOT NULL,
    [Descuento] SMALLINT NOT NULL DEFAULT 0,
    [Fecha_Inicio] SMALLDATETIME NOT NULL,
    [Fecha_Fin] SMALLDATETIME NOT NULL 
);
GO

CREATE TABLE [Pagos] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Codigo] NVARCHAR(50) NOT NULL,
	[Total] FLOAT NOT NULL,
	[Medio] NVARCHAR(50) NOT NULL,
	[Reserva] INT NOT NULL REFERENCES [Reservas] ([Id]),
	[Promocion] INT NOT NULL REFERENCES [Promociones] ([Id])
);
GO

CREATE TABLE [Roles](
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [Usuarios](
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Nombre] NVARCHAR(50) NOT NULL UNIQUE,
	[Contrasena] NVARCHAR(50) NOT NULL,
	[Cliente] INT NOT NULL REFERENCES [Clientes] ([Id]),
	[Rol] INT NOT NULL REFERENCES [Roles] ([Id])
);
GO

CREATE TABLE [Auditorias] (
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Codigo] NVARCHAR(50) NOT NULL,
	[Accion] NVARCHAR(255) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL
);
GO

CREATE TABLE [Permisos](
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Tipo] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [Roles_Permisos](
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Codigo] NVARCHAR(50) NOT NULL,
	[Permiso] INT NOT NULL REFERENCES [Permisos] ([Id]),
	[Rol] INT NOT NULL REFERENCES [Roles] ([Id])
);
GO

INSERT INTO [Roles] ([Nombre]) VALUES 
('Administrador'), 
('Usuario'),
('Recepcionista');

INSERT INTO [Permisos] ([Tipo]) VALUES 
('Leer'),
('Insertar'),
('Consultar'),
('Modificar'),
('Eliminar');

INSERT INTO [Roles_Permisos] ([Codigo], [Permiso], [Rol]) VALUES 
('R001', 1, 1),  
('R002', 2, 1),  
('R003', 3, 1),  
('R004', 4, 1),  
('R005', 5, 1),
('R006', 1, 2),  
('R007', 2, 2),  
('R008', 1, 3),  
('R009', 2, 3),  
('R010', 3, 3),
('R011', 4, 3),
('R012', 5, 3);

INSERT INTO [Habitaciones] ([Nombre], [Camas], [Estado], [Imagen]) VALUES
('M-502', 1, 1, '/images/photo1.jpg'),
('H-306', 1, 1, '/images/photo2.jpg'),
('L-609', 1, 0, '/images/photo3.jpg'),
('J-807', 1, 1, '/images/photo4.jpg'),
('K-901', 2, 1, '/images/photo5.jpg');

INSERT INTO [Recepcionistas] ([Carnet], [Nombre], [Salario]) VALUES
('6003', 'Juan Cardona', 3000.00),
('6004', 'Camila Ramirez', 3200.00),
('6008', 'Sofia Gomez', 2800.00),
('6009', 'Tatiana Hernandez', 3500.00);

INSERT INTO [Opiniones] ([Opcion], [Cantidad]) VALUES
('Muy mal', 1),
('Mal', 1),
('Normal', 1),
('Bien', 2),
('Muy bien', 1);

INSERT INTO [Clientes] ([Cedula], [Nombre], [Opinion]) VALUES
('1002', 'Marisol Alvarez', 5)

INSERT INTO [Reservas] ([Codigo], [Fecha_entrada], [Fecha_salida], [Cliente], [Recepcionista], [Habitacion]) VALUES
('A501', '2024-12-14 18:00', '2025-01-06 13:00', 1, 1, 1);

INSERT INTO [Servicios] ([Tipo], [Tarifa], [Descripcion]) VALUES
('Restaurante', 28000, 'Servicio de restaurante'),
('Piscina', 13000, 'Acceso a la piscina'),
('Limpieza', 23000, 'Servicio de limpieza'),
('Discoteca', 65000, 'Entrada a la discoteca');

INSERT INTO [Servicios_Reservas] ([Codigo], [Servicio], [Reserva]) VALUES
('SR001', 1, 1);

INSERT INTO [Promociones] ([Codigo], [Descuento], [Fecha_inicio], [Fecha_fin]) VALUES
('P001', 0, '2022-06-10', '2022-06-19'),
('P002', 10, '2019-11-01', '2020-03-01'),
('P003', 15, '2025-01-05', '2025-01-20'),
('P004', 22, '2022-12-10', '2023-01-15'),
('P005', 38, '2003-03-01', '2003-03-19'),
('P006', 50, '2022-05-10', '2022-05-29');

INSERT INTO [Pagos] ([Codigo], [Total], [Medio], [Reserva], [Promocion]) VALUES
('PG001', 500000, 'Efectivo', 1, 3);
GO

INSERT INTO [Usuarios] ([Nombre], [Contrasena], [Rol], [Cliente]) VALUES
('Admin', '123', 1, 1)
GO

SELECT [Id], [Cedula], [Nombre], [Opinion] FROM [Clientes];

SELECT [Id],[Nombre], [Camas], [Estado] FROM [Habitaciones];

SELECT [Id], [Opcion], [Cantidad] FROM [Opiniones];

SELECT [Id], [Total], [Medio], [Reserva], [Promocion] FROM [Pagos];

SELECT [Id], [Descuento] ,[Fecha_Inicio], [Fecha_Fin] FROM [Promociones];

SELECT [Id], [Carnet], [Nombre], [Salario] FROM [Recepcionistas];

SELECT [Id], [Codigo], [Fecha_entrada], [Fecha_salida],[Recepcionista],[Cliente],[Habitacion] FROM [Reservas];

SELECT [Id], [Servicio], [Reserva] FROM [Servicios_Reservas];

SELECT [Id], [Tipo] ,[Tarifa], [Descripcion] FROM [Servicios];

SELECT [Id], [Nombre] FROM [Roles];

SELECT [Id], [Nombre], [Contrasena], [Rol], [Cliente] FROM [Usuarios];

SELECT [Id], [Codigo], [Accion], [Fecha] FROM [Auditorias];

SELECT [Id], [Tipo] FROM [Permisos];

SELECT [Id], [Codigo], [Permiso], [Rol] FROM [Roles_Permisos];
GO