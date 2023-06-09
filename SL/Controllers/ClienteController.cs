using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace SL.Controllers
{
    public class ClienteController : ApiController
    {
        // GET: Cliente
        [HttpGet]
        [Route("api/Cliente/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Cliente cliente = new ML.Cliente();

            ML.Result result = BL.Cliente.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
      
        [HttpGet]
        [Route("api/Cliente/GetById/{IdCliente}")]
        public IHttpActionResult GetById(int IdCliente)
        {
            ML.Cliente cliente = new ML.Cliente();
            ML.Result result = BL.Cliente.GetById(IdCliente);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/Cliente/Add")]
        public IHttpActionResult Add([FromBody] ML.Cliente cliente)
        {
            ML.Result result = BL.Cliente.Add(cliente);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/Cliente/Update/{IdCliente}")]
        public IHttpActionResult Update([FromBody] ML.Cliente cliente)
        {

            ML.Result result = BL.Cliente.Update(cliente);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/Cliente/Delete/{IdCliente}")]
        public IHttpActionResult Delete(int IdCliente)
        {
            ML.Cliente cliente = new ML.Cliente();
            cliente.IdCliente = IdCliente;
            ML.Result result = BL.Cliente.Delete(IdCliente);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

    }
}