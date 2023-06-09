using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClienteController : Controller
    {


        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Cliente cliente = new ML.Cliente();

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string str = System.Configuration.ConfigurationManager.AppSettings["WebApi"];
                    client.BaseAddress = new Uri(str);

                    var responseTask = client.GetAsync("Cliente/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Cliente resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    cliente.Clientes = result.Objects;
                }

            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
                result.Ex = Ex;
            }
            return View(cliente);
        }


        [HttpGet]
        public ActionResult Form(int? IdCliente)
        {

            ML.Result resultClientes = BL.Cliente.GetAll();
            ML.Cliente cliente = new ML.Cliente();

            if (resultClientes.Correct)
            {
                cliente.Clientes = resultClientes.Objects;
            }
            if (IdCliente == null)
            {               
                return View(cliente);
            }
            else
            {
                ML.Result result = new ML.Result();
                using (var client = new HttpClient())
                    try
                    {
                        client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);
                        var responseTask = client.GetAsync("Cliente/GetById/" + IdCliente);
                        responseTask.Wait();

                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)

                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            ML.Cliente resultItemList = new ML.Cliente();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Cliente>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;


                            cliente = (ML.Cliente)result.Object;//unboxing
                            cliente.Clientes = resultClientes.Objects;


                            //update
                            return View(cliente);
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio al consultar la información del cliente";
                            return View("Modal");
                        }
                    }
                    catch (Exception Ex)
                    {
                        result.Correct = false;
                        result.ErrorMessage = Ex.Message;
                        result.Ex = Ex;
                    }

                return View(cliente);
            }
        }


        [HttpPost] 
        public ActionResult Form(ML.Cliente cliente)
        {
            if (cliente.IdCliente != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Cliente>("Cliente/Update/" + cliente.IdCliente, cliente);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha modificado el registro";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha modificado el registro";
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Cliente>("Cliente/Add", cliente);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se ha agregado el registro";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha agregado el registro";
                        return PartialView("Modal");
                    }
                }
            }
        }

        public ActionResult Delete(int IdCliente)
        {
            ML.Result resultListClientes = new ML.Result();
            ML.Cliente cliente = new ML.Cliente();
            cliente.IdCliente = IdCliente;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

                var postTask = client.GetAsync("Cliente/Delete/" + IdCliente);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultListClientes = BL.Cliente.GetAll();
                    ViewBag.Message = "Se ha eliminado el registro";
                    return PartialView("Modal");
                }
            }
            resultListClientes = BL.Cliente.GetAll();
            return View("GetAll", resultListClientes);
        }






        // GET: Cliente //TAMBIEN FUNCIONA ASI 


        //public ActionResult GetAll()
        //{
        //    ML.Cliente cliente = new ML.Cliente();
        //    ML.Result result = BL.Cliente.GetAll();

        //    if (result.Correct)
        //    {
        //        cliente.Clientes = result.Objects;
        //        return View(cliente);
        //    }
        //    else
        //    {
        //        return View(cliente);
        //    }

        //}
        //[HttpGet]
        //public ActionResult Form(int? IdCliente)
        //{

        //    ML.Cliente cliente = new ML.Cliente();
        //    ML.Result resultClientes = BL.Cliente.GetAll();


        //    if (resultClientes.Correct)
        //    {
        //        cliente.Clientes = resultClientes.Objects;
        //    }
        //    if (IdCliente == null)
        //    {
        //        return View(cliente);
        //    }
        //    else
        //    {

        //        ML.Result result = BL.Cliente.GetById(IdCliente.Value);
        //        if (result.Correct)
        //        {
        //            cliente = (ML.Cliente)result.Object;
        //            cliente.Clientes = resultClientes.Objects;
        //            return View(cliente);
        //        }
        //        else
        //        {
        //            return View();
        //        }
        //    }
        //}
        //[HttpPost]
        //public ActionResult Form(ML.Cliente cliente)
        //{
        //    ML.Result result = new ML.Result();
        //    if (cliente.IdCliente != null)
        //    {
        //        result = BL.Cliente.Update(cliente);
        //        ViewBag.Message = "Se ha modificado el registro";
        //    }
        //    else
        //    {
        //        result = BL.Cliente.Add(cliente);
        //        ViewBag.Message = "Se ha agregado el registro";
        //    }
        //    if (result.Correct)
        //    {
        //        return PartialView("Modal");
        //    }
        //    else
        //    {
        //        return PartialView("Modal");
        //    }
        //}
        //public ActionResult Delete(int IdCliente)
        //{
        //    ML.Result result = BL.Cliente.Delete(IdCliente);
        //    if (result.Correct)
        //    {
        //        ViewBag.Message = "Se ha eliminado el registro";
        //        return PartialView("Modal");
        //    }
        //    else {
        //        ViewBag.Message = "No se ha eliminado el registro";
        //        return PartialView("Modal");
        //    }
        //}
    }
}