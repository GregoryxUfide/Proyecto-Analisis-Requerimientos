INSERT INTO Usuarios (Nombre, Apellidos, Gmail, Username, Contrasena, Estado, RolId)
VALUES ('Administrador', 'Principal', 'admin@hotel.com', 'admin', '$2a$11$1wBu0UtHoNd9ALUS0ft7w.bcD/Wej3N86Aaf4.sXkV7gYvPPBYUZO', 1, 
(
    SELECT Id FROM Roles WHERE Nombre = 'Admin'
));
clave 123456
Insert Into Roles (Nombre, Descripcion, Estado)
values('Admin','Administrador del Sistema','1');
Insert Into Roles (Nombre, Descripcion, Estado)
values('Usuario','Cliente del Sistema','1');
