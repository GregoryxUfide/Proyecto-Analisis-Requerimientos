using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ProductoController : Controller
    {
        IProductoHelper _ProductoHelper;
        IUbicacionProductoHelper _UbicacionProductoHelper; 


        public ProductoController(IProductoHelper ProductoHelper, IUbicacionProductoHelper ubicacionProductoHelper)
        {
            _ProductoHelper = ProductoHelper;
            _UbicacionProductoHelper = ubicacionProductoHelper;
        }


        public ActionResult Index()
        {
            return View(_ProductoHelper.GetAll());
        }

        public ActionResult Details(int id)
        {
            ProductoViewModel Producto = _ProductoHelper.GetById(id);
            Producto.NombreUbicacionProducto = _UbicacionProductoHelper
                                                .GetById(Producto.IdUbicacionProducto)
                                                .NombreUbicacionProducto;
            return View(Producto);
        }

        public ActionResult Create()
        {
            ProductoViewModel Producto = new ProductoViewModel();
            Producto.UbicacionProductos = _UbicacionProductoHelper.GetAll();
            return View(Producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoViewModel Producto)
        {
            try
            {
                _ProductoHelper.AddProducto(Producto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ProductoViewModel Producto = _ProductoHelper.GetById(id);
            Producto.UbicacionProductos = _UbicacionProductoHelper.GetAll();
            return View(Producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoViewModel Producto)
        {
            try
            {
                _ProductoHelper.EditProducto(Producto);
                return RedirectToAction("Details", new { id = Producto.IdProducto });
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            ProductoViewModel Producto = _ProductoHelper.GetById(id);
            Producto.NombreUbicacionProducto = _UbicacionProductoHelper
                                                .GetById(Producto.IdUbicacionProducto)
                                                .NombreUbicacionProducto;
            return View(Producto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductoViewModel Producto)
        {
            try
            {
                _ProductoHelper.DeleteProducto(Producto.IdProducto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
