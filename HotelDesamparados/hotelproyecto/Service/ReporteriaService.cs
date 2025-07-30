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
                new() { NombreTabla = "Producto", DescripcionTabla = "Lista de productos registrados" },
                new() { NombreTabla = "UbicacionProducto", DescripcionTabla = "Relación entre productos y ubicaciones" },
                new() { NombreTabla = "Usuario", DescripcionTabla = "Lista de usuarios del sistema" },
                new() { NombreTabla = "Roles", DescripcionTabla = "Lista de roles del sistema" }

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




        // Métodos similares para Usuarios, UbicacionProductos, etc.
    }
}
