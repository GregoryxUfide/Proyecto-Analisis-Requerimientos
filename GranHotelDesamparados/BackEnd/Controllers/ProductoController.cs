using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        IProductoService _ProductoService;


        public ProductoController(IProductoService productoService)
        {
            this._ProductoService = productoService;
        }


        // GET: api/<ProductoController>
        [HttpGet]
        public ActionResult Get()
        {
            var Productos = _ProductoService.GetProductos();
            return Ok(Productos);
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var Producto = _ProductoService.GetById(id);
            return Ok(Producto);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public void Post([FromBody] ProductoDTO ProductoDTO)
        {
            _ProductoService.Add(ProductoDTO);
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public void Put([FromBody] ProductoDTO ProductoDTO)
        {
            _ProductoService.Update(ProductoDTO);
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ProductoService.Delete(id);
        }
    }
}
