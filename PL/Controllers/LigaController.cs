using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PL.Models;

namespace PL.Controllers
{
    public class LigaController : Controller
    {
        public IActionResult GetAll()
        {
            Models.Liga liga = new Models.Liga();   

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var responseTask = client.GetAsync("liga");
                responseTask.Wait();    

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<List<Liga>>();
                    readTask.Wait();

                    liga.Ligas = new List<Object>();

                    foreach(var resultLiga in readTask.Result) 
                    {
                        Models.Liga aux = new Models.Liga();

                        aux.IdLiga = resultLiga.IdLiga;

                        aux.Nombre = resultLiga.Nombre; 

                        aux.Logo = resultLiga.Logo;

                        aux.Pais = new Models.Pais();

                        aux.Pais.IdPais = resultLiga.Pais.IdPais;

                        aux.Pais.Nombre = resultLiga.Pais.Nombre;

                        liga.Ligas.Add(aux);
                    
                    
                    }

                }
            }

            return View(liga);
        }


        [HttpGet]
        public IActionResult Form(int? IdLiga)
        {
            Models.Liga liga = new Models.Liga();
            liga.Pais = new Models.Pais();
            if(IdLiga != null)
            {
                using(var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");
                    var responseTask = client.GetAsync("liga/getbyid/" + IdLiga);
                    responseTask.Wait();    

                    var resultServicio = responseTask.Result;   

                    if(resultServicio.IsSuccessStatusCode) 
                    {
                        var readTask = resultServicio.Content.ReadAsStringAsync();
                        dynamic resultJson = JArray.Parse(readTask.Result);
                        readTask.Wait();

                        liga.IdLiga = resultJson[0]["idLiga"];
                        liga.Nombre = resultJson[0]["nombre"];
                        liga.Logo = resultJson[0]["logo"];
                        liga.Pais.IdPais = resultJson[0]["pais"]["idPais"];
                    }

                }

            }
            else
            {
                return View();
            }

            return View(liga);
        }


        [HttpPost]
        public IActionResult Form(Models.Liga liga)
        {
            if(liga.IdLiga == 0)
            {
                using(var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");
                    var postTask = client.PostAsJsonAsync("liga", liga);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View(liga);
                    }
                
                }


            }
            else
            {

                
                using(var client = new HttpClient())
                {
                    int IdLiga = liga.IdLiga;
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");
                    var putTask = client.PutAsJsonAsync("liga/update/" + IdLiga, liga);
                    putTask.Wait(); 

                    var resultServicio = putTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View(liga);  
                    }

                }
            }
            

        }

        public IActionResult Delete(int IdLiga)
        {
            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var deletedTask = client.DeleteAsync("liga/delete/" + IdLiga);
                deletedTask.Wait();

                var resultServicio = deletedTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return RedirectToAction("GetAll");
                }

            }

        }


      

    }
}
