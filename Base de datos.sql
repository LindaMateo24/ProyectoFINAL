create database DBPRESTAMO;
go
use DBPRESTAMO;
go
CREATE TABLE "CLIENTES" (
	"IdCliente"	INTEGER NOT NULL UNIQUE identity(1,1),
	"NombreCompleto"	varchar(max) NOT NULL,
	"TipoDocumento"	varchar(max) NOT NULL,
	"NumeroDocumento"	varchar(max) NOT NULL,
	"Direccion"	varchar(max) NOT NULL,
	"Ciudad"	varchar(max) NOT NULL,
	"Correo"	varchar(max) NOT NULL,
	"NumeroTelefono"	varchar(max) NOT NULL,
	PRIMARY KEY("IdCliente" )
)
go 
CREATE TABLE "CUOTA" (
	"IdCuota"	INTEGER NOT NULL UNIQUE identity(1,1),
	"IdPrestamo"	INTEGER NOT NULL,
	"NumeroCuota"	INTEGER NOT NULL,
	"FechaPagoCuota"	varchar(max) NOT NULL,
	"MontoCuota"	varchar(max) NOT NULL,
	"EstadoCuota"	varchar(max) NOT NULL,
	"FechaPago"	varchar(max) NOT NULL DEFAULT '',
	"ProximoPago"	INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("IdCuota")
)
go
CREATE TABLE "DATOS" (
	"IdDato"	INTEGER NOT NULL UNIQUE identity(1,1),
	"RazonSocial"	varchar(max) NOT NULL,
	"RUC"	varchar(max) NOT NULL,
	"Representante"	varchar(max) NOT NULL,
	"Correo"	varchar(max) NOT NULL,
	"Telefono"	varchar(max) NOT NULL,
	"Ciudad"	varchar(max) NOT NULL,
	"Logo"	VARBINARY(MAX),
	PRIMARY KEY("IdDato")
)
go
CREATE TABLE "PRESTAMO" (
	"IdPrestamo"	INTEGER NOT NULL UNIQUE identity(1,1),
	"NumeroOperacion"	varchar(max) NOT NULL,
	"FechaRegistro"	varchar(max) NOT NULL,
	"IdTipoPago"	INTEGER NOT NULL,
	"IdTipoMoneda"	INTEGER NOT NULL,
	"FechaInicio"	varchar(max) NOT NULL,
	"FechaFin"	varchar(max) NOT NULL,
	"MontoPrestamo"	varchar(max) NOT NULL,
	"Interes"	varchar(max) NOT NULL,
	"NumeroCuotas"	INTEGER NOT NULL,
	"MontoCuota"	varchar(max) NOT NULL,
	"TotalIntereses"	varchar(max) NOT NULL,
	"MontoTotal"	varchar(max) NOT NULL,
	"NombreCliente"	varchar(max) NOT NULL,
	"TipoDocumento"	varchar(max) NOT NULL,
	"NumeroDocumento"	varchar(max) NOT NULL,
	"Direccion"	varchar(max) NOT NULL,
	"Ciudad"	varchar(max) NOT NULL,
	"Correo"	varchar(max) NOT NULL,
	"NumeroTelefono"	varchar(max) NOT NULL,
	"Estado"	varchar(max) NOT NULL DEFAULT 'EN CURSO',
	"Clausula"	varchar(max) DEFAULT '',
	PRIMARY KEY("IdPrestamo")
)
go
CREATE TABLE "TIPO_MONEDA" (
	"IdTipoMoneda"	INTEGER NOT NULL UNIQUE identity(1,1),
	"Divisa"	varchar(max) NOT NULL,
	"Abreviatura"	varchar(max) NOT NULL,
	PRIMARY KEY("IdTipoMoneda")
)
go
CREATE TABLE "TIPO_PAGO" (
	"IdTipoPago"	INTEGER NOT NULL UNIQUE identity(1,1),
	"Descripcion"	varchar(max) NOT NULL,
	"Valor"	INTEGER NOT NULL,
	"AplicaDias"	INTEGER NOT NULL,
	PRIMARY KEY("IdTipoPago")
)
go
CREATE TABLE "USUARIO" (
	"IdUsuario"	INTEGER NOT NULL UNIQUE,
	"NombreUsuario"	varchar(max) NOT NULL,
	"Clave"	varchar(max) NOT NULL,
	PRIMARY KEY("IdUsuario")
)
