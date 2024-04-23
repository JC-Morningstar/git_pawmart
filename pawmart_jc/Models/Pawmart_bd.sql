-- Crear la base de datos
CREATE DATABASE Pawmart_BD;
GO

-- Usar la base de datos recién creada
USE Pawmart_BD;
GO

-- Crear la tabla Producto
CREATE TABLE Producto (
    ID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(255),
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(10, 2),
    Existencia INT,
    Fecha_creacion DATETIME2 DEFAULT GETDATE(),
    Tipo_mascota NVARCHAR(50),
    Imagen VARBINARY(MAX),
    Otras_caracteristicas NVARCHAR(MAX)
);
GO

-- Crear la tabla Clientes
CREATE TABLE Clientes (
    ID INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100),
    Apellido NVARCHAR(100),
    Correo_electronico NVARCHAR(255),
    Contraseña NVARCHAR(255),
    Direccion_envio NVARCHAR(255),
    Otros_datos_contacto NVARCHAR(MAX)
);
GO

-- Crear la tabla Pedidos
CREATE TABLE Pedidos (
    ID INT PRIMARY KEY IDENTITY,
    ID_cliente INT,
    Fecha_hora_pedido DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (ID_cliente) REFERENCES Clientes(ID)
);
GO

-- Crear la tabla Detalles_del_Pedido
CREATE TABLE Detalles_del_Pedido (
    ID INT PRIMARY KEY IDENTITY,
    ID_pedido INT,
    ID_producto INT,
    Cantidad INT,
    Precio_unitario DECIMAL(10, 2),
    FOREIGN KEY (ID_pedido) REFERENCES Pedidos(ID),
    FOREIGN KEY (ID_producto) REFERENCES Producto(ID)
);
GO
