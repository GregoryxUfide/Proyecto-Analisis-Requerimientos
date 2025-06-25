using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class UbicacionProductoController : Controller
    {
        IUbicacionProductoHelper _UbicacionProductoHelper;

        public UbicacionProductoController(IUbicacionProductoHelper UbicacionProductoHelper)
        {
            _UbicacionProductoHelper = UbicacionProductoHelper;
        }


        public ActionResult Index()
        {
            return View(_UbicacionProductoHelper.GetAll());
        }

        public ActionResult Details(int id)
        {
            UbicacionProductoViewModel UbicacionProducto = _UbicacionProductoHelper.GetById(id);
            return View(UbicacionProducto);
        }

        public ActionResult Create()
        {
            UbicacionProductoViewModel UbicacionProducto = new UbicacionProductoViewModel();
            return View(UbicacionProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UbicacionProductoViewModel UbicacionProducto)
        {
            try
            {
                _UbicacionProductoHelper.AddUbicacionProducto(UbicacionProducto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            UbicacionProductoViewModel UbicacionProducto = _UbicacionProductoHelper.GetById(id);
            return View(UbicacionProducto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UbicacionProductoViewModel UbicacionProducto)
        {
            try
            {
                _UbicacionProductoHelper.EditUbicacionProducto(UbicacionProducto);
                return RedirectToAction("Details", new { id = UbicacionProducto.IdUbicacionProducto });
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            UbicacionProductoViewModel UbicacionProducto = _UbicacionProductoHelper.GetById(id);
            return View(UbicacionProducto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UbicacionProductoViewModel UbicacionProducto)
        {
            try
            {
                _UbicacionProductoHelper.DeleteUbicacionProducto(UbicacionProducto.IdUbicacionProducto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
