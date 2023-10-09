create database umbraco

use test
go

create table Comercios(
IdComercio int primary key identity (1,1),
Nom_Comercio varchar(100),
Aforo_Maximo int
)

create table Servicios(
Id_Servicios int primary key identity(1,1),
IdComercio int,
Nom_Servicios varchar(100),
Hora_Apertura time,
Hora_Cierre time,
duracion varchar(30)
)

INSERT INTO Servicios (IdComercio, Nom_Servicios, Hora_Apertura, Hora_Cierre, duracion)
VALUES 
(1, 'Servicio Belleza', '09:00:00', '17:00:00', '60'),
(2, 'Compras', '08:30:00', '17:30:00', '90')

create table Turnos(
Id_Turno int primary key identity(1,1),
Id_Servicios int,
Fecha_turno datetime,
Fecha_inicio datetime,
Fecha_Fin datetime,
estado bit
)

INSERT INTO Turnos (Id_Servicios, Fecha_turno, Fecha_inicio, Fecha_Fin, estado)
VALUES 
(1, '2023-10-06 09:00:00', '2023-10-06 09:00:00', '2023-10-06 09:30:00', 1),
(2, '2023-10-07 10:30:00', '2023-10-07 10:30:00', '2023-10-07 11:00:00', 1)


-----------------------------------------------------------------------------
ALTER PROCEDURE GenerarTurnosDiarios
@FechaInicio datetime,
@FechaFin datetime,
@IdServicio int
AS

BEGIN

DECLARE @HoraApertura time, @HoraCierre time, @Duracion int

SELECT @HoraApertura = Hora_Apertura, @HoraCierre = Hora_Cierre, @Duracion = Duracion
FROM Servicios
WHERE Id_Servicios = @IdServicio

DECLARE @MinutosDisponibles INT
DECLARE @NumTurnos INT

SET @MinutosDisponibles = DATEDIFF(MINUTE, @HoraApertura, @HoraCierre)

SET @NumTurnos = @MinutosDisponibles / @Duracion

	declare @FechaTurnoInicio datetime 
	SET @FechaTurnoInicio = DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00:00', @HoraApertura), @FechaInicio)
	
	declare @FechaTurnoFin datetime
	SET @FechaTurnoFin = DATEADD(MINUTE, DATEDIFF(MINUTE, '00:00:00', @HoraCierre), @FechaFin)

--	select @FechaTurnoInicio, @FechaTurnoFin

DECLARE @FechaActual datetime
SET @FechaActual = @FechaTurnoInicio

DECLARE @FechaActualFin datetime
SET @FechaActualFin = DATEADD(MINUTE, @Duracion, @FechaActual)

WHILE DATEADD(MINUTE, @Duracion, @FechaActual) <= DATEADD(MINUTE, @Duracion, @FechaTurnoFin)
BEGIN
    
	IF CONVERT(time, @FechaActual) >= @HoraApertura AND CONVERT(time, @FechaActual) <= @HoraCierre
    BEGIN
		INSERT INTO Turnos (Id_Servicios, Fecha_turno, Fecha_inicio, Fecha_fin, estado)
		VALUES (@IdServicio, @FechaActual, @FechaActual, @FechaActualFin, 1)	
    END

    SET @FechaActual = DATEADD(MINUTE, @Duracion, @FechaActual)
	SET @FechaActualFin = DATEADD(MINUTE, @Duracion, @FechaActual)
	
END
end

-----------------------------------------------------------------------------

Exec GenerarTurnosDiarios
@FechaInicio = '2023-07-10',
@FechaFin = '2023-08-10',
@IdServicio = 1

select * from Turnos
select * from Servicios



-----------------------------------------------------------

ALTER PROCEDURE SP_CONSULTA_TURNOS
AS
BEGIN
SELECT
	Tu.Id_Turno,
	CONVERT(DATE, Tu.Fecha_turno) AS Fecha,
    CONVERT(VARCHAR(2), DATEPART(HOUR, Tu.Fecha_Inicio)) + ':' +
    RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(MINUTE, Tu.Fecha_Inicio)), 2) AS HoraInicio,
	CONVERT(VARCHAR(2), DATEPART(HOUR, Tu.Fecha_Fin)) + ':' +
    RIGHT('0' + CONVERT(VARCHAR(2), DATEPART(MINUTE, Tu.Fecha_Fin)), 2) AS HoraFin,
	Ser.Nom_Servicios as NomServicios, Tu.estado 
FROM Turnos as Tu
INNER JOIN Servicios as Ser 
on Ser.Id_Servicios = Tu.Id_Servicios
END

