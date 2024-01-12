using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public  class PaisController : Controller
    {
        public IActionResult GetAll()
        {
            Models.Pais pais = new Models.Pais();

            using(var client  = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var responseTask = client.GetAsync("pais");
                responseTask.Wait();

                var resultServicio = responseTask.Result;   

                if(resultServicio.IsSuccessStatusCode) 
                {
                    var readTask = resultServicio.Content.ReadAsAsync<List<Pais>>();
                    readTask.Wait();

                    pais.Paises = new List<Object>();

                    foreach (var resultPais in readTask.Result)
                    {
                        Models.Pais aux = new Models.Pais();

                        aux.IdPais = resultPais.IdPais;

                        aux.Nombre = resultPais.Nombre;

                        pais.Paises.Add(aux);


                    }

                }


            
            }

            return View(pais);
        }
    }
}
