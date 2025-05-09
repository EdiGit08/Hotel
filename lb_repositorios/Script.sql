CREATE DATABASE db_hotel;
GO

USE db_hotel;

CREATE TABLE [Habitaciones] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                           [Nombre] NVARCHAR(100) NOT NULL,
                           [Camas] INT NOT NULL DEFAULT 1,
                           [Estado] BIT NOT NULL DEFAULT 0
);
GO

CREATE TABLE [Recepcionistas] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                            [Carnet] NVARCHAR(10) NOT NULL UNIQUE ,
                            [Nombre] NVARCHAR(100) UNIQUE NOT NULL,
                            [Salario] FLOAT NOT NULL DEFAULT 0
);
GO

CREATE TABLE [Opiniones] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                        [Opcion] NVARCHAR(30) DEFAULT NULL,
                        [Cantidad] SMALLINT DEFAULT 0,
);
GO

CREATE TABLE [Clientes] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                       [Cedula] NVARCHAR(20) NOT NULL UNIQUE,
                       [Nombre] NVARCHAR(65) NOT NULL,
                       [Opinion] INT NOT NULL REFERENCES [Opiniones] ([Id])
);
GO

CREATE TABLE [Reservas] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                       [Codigo] NVARCHAR(20) NOT NULL UNIQUE,
                       [Fecha_entrada] SMALLDATETIME NOT NULL,
                       [Fecha_salida] SMALLDATETIME NOT NULL,
                       [Recepcionista] INT NOT NULL REFERENCES [Recepcionistas] ([Id]),
                       [Cliente] INT NOT NULL REFERENCES [Clientes] ([Id]),
                       [Habitacion] INT NOT NULL REFERENCES [Habitaciones] ([Id])
);
GO

CREATE TABLE [Servicios] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                        [Tipo] NVARCHAR(50) NOT NULL,
                        [Tarifa] FLOAT NOT NULL,
						[Descripcion] NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE [Servicios_Reservas] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                                [Servicio] INT NOT NULL REFERENCES [Servicios] ([Id]),
                                [Reserva] INT NOT NULL REFERENCES [Reservas] ([Id])
);
GO

CREATE TABLE [Promociones] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
                          [Descuento] SMALLINT NOT NULL DEFAULT 0,
                          [Fecha_Inicio] SMALLDATETIME NOT NULL,
                          [Fecha_Fin] SMALLDATETIME NOT NULL 
);
GO

CREATE TABLE [Pagos] ([Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
					[Total] FLOAT NOT NULL,
					[Medio] NVARCHAR(50) NOT NULL,
					[Reserva] INT NOT NULL REFERENCES [Reservas] ([Id]),
					[Promocion] INT NOT NULL REFERENCES [Promociones] ([Id])
);
GO

INSERT INTO [Habitaciones] ([Nombre], [Estado]) VALUES
('M-502', 1),
('H-306', 1),
('L-609', 1),
('J-807', 1),
('K-901', 1);

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
('1002', 'Marisol Alvarez', 5),
('7005', 'Camilo Arango', 2),
('4003', 'Esteban Ramirez', 3),
('5006', 'Juan Giraldo', 4),
('5009', 'Camila Ramirez', 5),
('9001', 'Manuela Tobon', 1),
('3006', 'Miguel Lopez', 4);

INSERT INTO [Reservas] ([Codigo], [Fecha_entrada], [Fecha_salida], [Cliente], [Recepcionista], [Habitacion]) VALUES
('A501', '2024-12-14 18:00', '2025-01-06 13:00', 1, 1, 1),
('A903', '2003-03-16 11:00', '2003-03-17 12:00', 2, 2, 2),
('A807', '2022-12-13 14:15', '2022-12-17 17:30', 1, 1, 3),
('A306', '2024-12-14 16:00', '2025-01-02 12:00', 3, 3, 1),
('A100', '2019-10-02 11:30', '2019-11-03 13:20', 4, 4, 4),
('A208', '2020-02-11 12:00', '2020-02-15 13:00', 5, 4, 4),
('A600', '2022-04-12 08:00', '2022-05-14 14:45', 6, 2, 2),
('A701', '2010-07-13 09:30', '2010-08-14 12:00', 7, 4, 5);

INSERT INTO [Servicios] ([Tipo], [Tarifa], [Descripcion]) VALUES
('Restaurante', 28000, 'Servicio de restaurante'),
('Piscina', 13000, 'Acceso a la piscina'),
('Limpieza', 23000, 'Servicio de limpieza'),
('Discoteca', 65000, 'Entrada a la discoteca');

INSERT INTO [Servicios_Reservas] ([Servicio], [Reserva]) VALUES
(1, 1),
(2, 2),
(2, 3),
(3, 4),
(1, 5),
(4, 6),
(2, 7),
(4, 8);

INSERT INTO [Promociones] ([Descuento], [Fecha_inicio], [Fecha_fin]) VALUES
(0, '2022-06-10', '2022-06-19'),
(10, '2019-11-01', '2020-03-01'),
(15, '2025-01-05', '2025-01-20'),
(22, '2022-12-10', '2023-01-15'),
(38, '2003-03-01', '2003-03-19'),
(50, '2022-05-10', '2022-05-29');

INSERT INTO [Pagos] ([Total], [Medio], [Reserva], [Promocion]) VALUES
(500000, 'Efectivo', 1, 3),
(250000, 'Tarjeta', 2, 5),
(6000000, 'Efectivo', 3, 4),
(120000, 'Transferencia', 4, 1),
(8000000, 'Tarjeta', 5, 2),
(6000000, 'Efectivo', 6, 2),
(250000, 'Efectivo', 7, 6),
(1000000, 'Tarjeta', 8, 1);
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
GO
                                                                    