CREATE PROCEDURE sp_CrearReserva
    @FechaInicio DATETIME,
    @FechaFinal DATETIME,
    @Nombre NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Correo NVARCHAR(100),
    @HabitacionId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Reservas(FechaInicio, FechaFinal, Nombre, Telefono, Correo, HabitacionId, Estado)
    VALUES (@FechaInicio, @FechaFinal, @Nombre, @Telefono, @Correo, @HabitacionId, 1);
END