using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionProductoController : ControllerBase
    {
        IUbicacionProductoService _UbicacionProductoService;


        public UbicacionProductoController(IUbicacionProductoService UbicacionProductoService)
        {
            this._UbicacionProductoService = UbicacionProductoService;
        }


        // GET: api/<UbicacionProductoController>
        [HttpGet]
        public ActionResult Get()
        {
            var UbicacionProductos = _UbicacionProductoService.GetUbicacionProductos();
            return Ok(UbicacionProductos);
        }

        // GET api/<UbicacionProductoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var UbicacionProducto = _UbicacionProductoService.GetById(id);
            return Ok(UbicacionProducto);
        }

        // POST api/<UbicacionProductoController>
        [HttpPost]
        public void Post([FromBody] UbicacionProductoDTO UbicacionProductoDTO)
        {
            _UbicacionProductoService.Add(UbicacionProductoDTO);
        }

        // PUT api/<UbicacionProductoController>/5
        [HttpPut]
        public void Put([FromBody] UbicacionProductoDTO UbicacionProductoDTO)
        {
            _UbicacionProductoService.Update(UbicacionProductoDTO);
        }

        // DELETE api/<UbicacionProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _UbicacionProductoService.Delete(id);
        }
    }
}
