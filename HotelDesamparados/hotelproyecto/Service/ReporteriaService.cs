using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace hotelproyecto.Service
{
    public class ReporteriaService
    {
        private readonly ReporteriaData _data;

        public ReporteriaService(ReporteriaData data)
        {
            _data = data;
        }

        #region "Index de Tablas Reporteria"
        public List<ReporteriaViewModel> ObtenerTablasDisponibles()
        {
            return new List<ReporteriaViewModel>
            {
                new() { NombreTabla = "Roles", DescripcionTabla = "Lista de roles del sistema" },
                new() { NombreTabla = "Usuario", DescripcionTabla = "Lista de usuarios del sistema" },
                new() { NombreTabla = "Empleado", DescripcionTabla = "Reporte de empleados" },
                new() { NombreTabla = "Contabilidad", DescripcionTabla = "Reporte de contabilidad" },
                new() { NombreTabla = "Habitacion", DescripcionTabla = "Reporte de habitaciones" },
                new() { NombreTabla = "Reserva", DescripcionTabla = "Reporte de reservas" },
                new() { NombreTabla = "PuntoVenta", DescripcionTabla = "Reporte de punto de venta" },
                new() { NombreTabla = "Producto", DescripcionTabla = "Lista de productos registrados" },
                new() { NombreTabla = "UbicacionProducto", DescripcionTabla = "Relación entre productos y ubicaciones" }                                                                                                           
            };
        }
        #endregion

        #region "Tabla Productos"

        public async Task<List<Producto>> ObtenerDatosProductosAsync()
        {
            return await _data.ListarProductosAsync();
        }

        public byte[] GenerarPdfDeProductos(List<Producto> productos)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Reporte de Productos")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Black)
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn(2); 
                            columns.RelativeColumn(2);  
                            columns.ConstantColumn(60);  
                            columns.ConstantColumn(60); 
                            columns.ConstantColumn(90); 
                            columns.RelativeColumn(2);  
                            columns.ConstantColumn(50); 
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Nombre").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Descripción").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Ubicado").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Cantidad").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Caducidad").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Marca").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                        });

                        foreach (var p in productos)
                        {
                            table.Cell().Element(CellCuerpo).Text(p.IdProducto.ToString());
                            table.Cell().Element(CellCuerpo).Text(p.NombreProducto ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(p.DescripcionProducto ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(p.IdUbicacionProducto.ToString());
                            table.Cell().Element(CellCuerpo).Text(p.CantidadProducto.ToString());
                            table.Cell().Element(CellCuerpo).Text(
                                p.CaducidadProducto.HasValue ? p.CaducidadProducto.Value.ToString("yyyy-MM-dd") : "N/A"
                            );
                            table.Cell().Element(CellCuerpo).Text(p.MarcaProducto ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(p.EstadoProducto ? "Activo" : "Inactivo");
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container.Background("#2c5d61")
                                    .PaddingVertical(5).PaddingHorizontal(5)
                                    .AlignCenter().ShowOnce()
                                    .BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container.Background("#f5ece7")
                                    .PaddingVertical(5).PaddingHorizontal(5)
                                    .AlignCenter().ShowOnce()
                                    .BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        #endregion

        #region "Tabla Ubicación de Productos"

        public async Task<List<UbicacionProducto>> ObtenerDatosUbicacionesAsync()
        {
            return await _data.ListarUbicacionesAsync();
        }

        public byte[] GenerarPdfDeUbicaciones(List<UbicacionProducto> ubicaciones)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Reporte de Ubicaciones de Producto")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Black)
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(60);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(4);
                            columns.ConstantColumn(50);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Nombre").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Descripción").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                        });

                        foreach (var u in ubicaciones)
                        {
                            table.Cell().Element(CellCuerpo).Text(u.IdUbicacionProducto.ToString());
                            table.Cell().Element(CellCuerpo).Text(u.NombreUbicacionProducto);
                            table.Cell().Element(CellCuerpo).Text(u.DescripcionUbicacionProducto);
                            table.Cell().Element(CellCuerpo).Text(u.Estado ? "Activo" : "Inactivo");
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container
                            .Background("#2c5d61")
                            .PaddingVertical(5)
                            .PaddingHorizontal(5)
                            .AlignCenter().ShowOnce()
                            .BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container
                            .Background("#f5ece7")
                            .PaddingVertical(5)
                            .PaddingHorizontal(5).
                            AlignCenter().ShowOnce()
                            .BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }
        #endregion

        #region "Tabla Usuarios"

        public async Task<List<Usuario>> ObtenerDatosUsuariosAsync()
        {
            return await _data.ListarUsuariosAsync();
        }

        public byte[] GenerarPdfDeUsuarios(List<Usuario> usuarios)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Reporte de Usuarios")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Black)
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);  // ID
                            columns.RelativeColumn(2);   // Nombre
                            columns.RelativeColumn(2);   // Apellidos
                            columns.RelativeColumn(3);   // Gmail
                            columns.RelativeColumn(2);   // Username
                            columns.ConstantColumn(50);  // Estado
                            columns.ConstantColumn(40);  // Rol
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Nombre").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Apellidos").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Correo").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Username").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("RolId").FontColor(Colors.White);
                        });

                        foreach (var u in usuarios)
                        {
                            table.Cell().Element(CellCuerpo).Text(u.Id.ToString());
                            table.Cell().Element(CellCuerpo).Text(u.Nombre);
                            table.Cell().Element(CellCuerpo).Text(u.Apellidos);
                            table.Cell().Element(CellCuerpo).Text(u.Gmail);
                            table.Cell().Element(CellCuerpo).Text(u.Username);
                            table.Cell().Element(CellCuerpo).Text(u.Estado ? "Activo" : "Inactivo");
                            table.Cell().Element(CellCuerpo).Text(u.RolId.ToString());
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container
                                .Background("#2c5d61")
                                .PaddingVertical(5)
                                .PaddingHorizontal(5)
                                .AlignCenter()
                                .ShowOnce()
                                .BorderBottom(1)
                                .BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container
                                .Background("#f5ece7")
                                .PaddingVertical(5)
                                .PaddingHorizontal(5)
                                .AlignCenter()
                                .ShowOnce()
                                .BorderBottom(1)
                                .BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        #endregion

        #region "Tabla Roles"

        public async Task<List<Rol>> ObtenerDatosRolesAsync()
        {
            return await _data.ListarRolesAsync();
        }

        public byte[] GenerarPdfDeRoles(List<Rol> roles)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Reporte de Roles")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Black)
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(60);
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(4);
                            columns.ConstantColumn(50);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Nombre").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Descripción").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                        });

                        foreach (var u in roles)
                        {
                            table.Cell().Element(CellCuerpo).Text(u.Id.ToString());
                            table.Cell().Element(CellCuerpo).Text(u.Nombre);
                            table.Cell().Element(CellCuerpo).Text(u.Descripcion);
                            table.Cell().Element(CellCuerpo).Text(u.Estado ? "Activo" : "Inactivo");
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container
                            .Background("#2c5d61")
                            .PaddingVertical(5)
                            .PaddingHorizontal(5)
                            .AlignCenter().ShowOnce()
                            .BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container
                            .Background("#f5ece7")
                            .PaddingVertical(5)
                            .PaddingHorizontal(5).
                            AlignCenter().ShowOnce()
                            .BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }
        #endregion

        #region Conta
        public async Task<List<Contabilidad>> ObtenerDatosContabilidadAsync()
        {
            return await _data.ListarContabilidadAsync();
        }
        public byte[] GenerarPdfDeContabilidad(List<Contabilidad> lista)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Reporte de Contabilidad").FontSize(20).Bold().AlignCenter();
                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);   // ID
                            columns.RelativeColumn();     // Fecha
                            columns.RelativeColumn();     // Monto
                            columns.RelativeColumn(2);    // Detalle
                            columns.RelativeColumn(2);    // Comentario
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Fecha").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Monto").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Detalle").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Comentario").FontColor(Colors.White);
                        });

                        foreach (var item in lista)
                        {
                            table.Cell().Element(CellCuerpo).Text(item.IdContabilidad.ToString());
                            table.Cell().Element(CellCuerpo).Text(item.Fecha?.ToString("yyyy-MM-dd") ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(item.Monto.ToString("C"));
                            table.Cell().Element(CellCuerpo).Text(item.Detalle);
                            table.Cell().Element(CellCuerpo).Text(item.Comentario);
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container.Background("#2c5d61").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container.Background("#f5ece7").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        #endregion

        #region Empleados
        public async Task<List<Empleado>> ObtenerDatosEmpleadosAsync()
        {
            return await _data.ListarEmpleadosAsync();
        }
        public byte[] GenerarPdfDeEmpleados(List<Empleado> empleados)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Reporte de Empleados")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Black)
                        .AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);   // ID
                            columns.RelativeColumn(2);    // NumeroEmpleado
                            columns.RelativeColumn();     // Salario
                            columns.RelativeColumn(2);    // Username
                            columns.RelativeColumn(2);    // Rol
                            columns.ConstantColumn(60);   // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Número").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Salario").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Usuario").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Rol").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                        });

                        foreach (var e in empleados)
                        {
                            table.Cell().Element(CellCuerpo).Text(e.Id.ToString());
                            table.Cell().Element(CellCuerpo).Text(e.NumeroEmpleado);
                            table.Cell().Element(CellCuerpo).Text(e.SalarioEmpleado.ToString("C"));
                            table.Cell().Element(CellCuerpo).Text(e.Usuario?.Username ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(e.Usuario?.Rol?.Nombre ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(e.Estado ? "Activo" : "Inactivo");
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container.Background("#2c5d61")
                                     .Padding(5)
                                     .AlignCenter()
                                     .BorderBottom(1)
                                     .BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container.Background("#f5ece7")
                                     .Padding(5)
                                     .AlignCenter()
                                     .BorderBottom(1)
                                     .BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        #endregion

        #region Habitaciones
        public async Task<List<Habitacion>> ObtenerDatosHabitacionesAsync()
        {
            return await _data.ListarHabitacionesAsync();
        }
        public byte[] GenerarPdfDeHabitaciones(List<Habitacion> habitaciones)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Reporte de Habitaciones").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);    // ID
                            columns.ConstantColumn(40);    // Capacidad
                            columns.ConstantColumn(60);    // Precio
                            columns.ConstantColumn(60);    // NumHabitación
                            columns.ConstantColumn(40);    // Camas
                            columns.RelativeColumn(2);     // Extras
                            columns.RelativeColumn(2);     // Comentarios
                            columns.ConstantColumn(60);    // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Cap").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Precio").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Habitación").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Camas").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Extras").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Comentarios").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                        });

                        foreach (var h in habitaciones)
                        {
                            table.Cell().Element(CellCuerpo).Text(h.Id.ToString());
                            table.Cell().Element(CellCuerpo).Text(h.Capacidad.ToString());
                            table.Cell().Element(CellCuerpo).Text(h.Precio.ToString("C"));
                            table.Cell().Element(CellCuerpo).Text(h.NumHabitacion.ToString());
                            table.Cell().Element(CellCuerpo).Text(h.NumCamas.ToString());
                            table.Cell().Element(CellCuerpo).Text(h.Extras ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(h.Comentarios ?? "N/A");
                            table.Cell().Element(CellCuerpo).Text(h.Estado ? "Activo" : "Inactivo");
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container.Background("#2c5d61").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container.Background("#f5ece7").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        #endregion

        #region Reservas
        public async Task<List<Reserva>> ObtenerDatosReservasAsync()
        {
            return await _data.ListarReservasAsync();
        }
        public byte[] GenerarPdfDeReservas(List<Reserva> reservas)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Reporte de Reservas").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);    // ID
                            columns.RelativeColumn();      // FechaInicio
                            columns.RelativeColumn();      // FechaFinal
                            columns.RelativeColumn();      // Nombre
                            columns.RelativeColumn();      // Teléfono
                            columns.RelativeColumn();      // Correo
                            columns.ConstantColumn(50);    // HabitaciónId
                            columns.ConstantColumn(60);    // Estado
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Inicio").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Final").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Nombre").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Teléfono").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Correo").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Hab").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Estado").FontColor(Colors.White);
                        });

                        foreach (var r in reservas)
                        {
                            table.Cell().Element(CellCuerpo).Text(r.IdReserva.ToString());
                            table.Cell().Element(CellCuerpo).Text(r.FechaInicio.ToString("yyyy-MM-dd"));
                            table.Cell().Element(CellCuerpo).Text(r.FechaFinal.ToString("yyyy-MM-dd"));
                            table.Cell().Element(CellCuerpo).Text(r.Nombre);
                            table.Cell().Element(CellCuerpo).Text(r.Telefono);
                            table.Cell().Element(CellCuerpo).Text(r.Correo);
                            table.Cell().Element(CellCuerpo).Text(r.HabitacionId.ToString());
                            table.Cell().Element(CellCuerpo).Text(r.Estado ? "Activa" : "Cancelada");
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container.Background("#2c5d61").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container.Background("#f5ece7").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }


        #endregion

        #region Puntos de Venta

        public async Task<List<PuntoVenta>> ObtenerDatosPuntoVentaAsync()
        {
            return await _data.ListarPuntosVentaAsync();
        }
        public byte[] GenerarPdfDePuntoVenta(List<PuntoVenta> ventas)
        {
            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Text("Reporte de Punto de Venta").FontSize(20).Bold().AlignCenter();

                    page.Content().PaddingVertical(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);   // ID
                            columns.RelativeColumn(2);    // Descripción
                            columns.RelativeColumn();     // Método
                            columns.ConstantColumn(60);   // Descuento
                            columns.ConstantColumn(60);   // ReservaId
                            columns.ConstantColumn(60);   // EmpleadoId
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellEncabezado).Text("ID").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Descripción").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Método").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Descuento").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Reserva").FontColor(Colors.White);
                            header.Cell().Element(CellEncabezado).Text("Empleado").FontColor(Colors.White);
                        });

                        foreach (var p in ventas)
                        {
                            table.Cell().Element(CellCuerpo).Text(p.Id.ToString());
                            table.Cell().Element(CellCuerpo).Text(p.DescripcionVenta);
                            table.Cell().Element(CellCuerpo).Text(p.Metodo_Pago);
                            table.Cell().Element(CellCuerpo).Text(p.Descuento.ToString("P"));
                            table.Cell().Element(CellCuerpo).Text(p.ReservaId.ToString());
                            table.Cell().Element(CellCuerpo).Text(p.EmpleadoId.ToString());
                        }

                        static IContainer CellEncabezado(IContainer container) =>
                            container.Background("#2c5d61").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Medium);

                        static IContainer CellCuerpo(IContainer container) =>
                            container.Background("#f5ece7").Padding(5).AlignCenter().BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
                    });

                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("Generado el ").FontSize(10);
                        txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).SemiBold().FontSize(10);
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        #endregion

    }
}
