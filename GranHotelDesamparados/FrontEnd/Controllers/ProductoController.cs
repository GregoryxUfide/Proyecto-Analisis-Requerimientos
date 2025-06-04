using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ProductoController : Controller
    {
        IProductoHelper _ProductoHelper;
        //Agrego UbicacionProducto para que se pueda mostrar en la vista


        public ProductoController(IProductoHelper ProductoHelper)
        {
            _ProductoHelper = ProductoHelper;
        }


        public ActionResult Index()
        {
            return View(_ProductoHelper.GetAll());
        }

        public ActionResult Details(int id)
        {
            ProductoViewModel Producto = _ProductoHelper.GetById(id);
            return View(Producto);
        }

        public ActionResult Create()
        {
            ProductoViewModel Producto = new ProductoViewModel();
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
