using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    public class EstadioController : Controller
    {
        public IActionResult GetAll()
        {
            Models.Estadio estadio = new Models.Estadio();

            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var resposeTask = client.GetAsync("estadio");
                resposeTask.Wait(); 

                var resultServicio = resposeTask.Result;

                if(resultServicio.IsSuccessStatusCode) 
                {
                    var readTask = resultServicio.Content.ReadAsAsync<List<Estadio>>();
                    readTask.Wait();

                    estadio.Estadios = new List<Object>();

                    foreach(var resultEstadio in readTask.Result) 
                    {
                        Estadio aux = new Estadio();    

                        aux.IdEstadio = resultEstadio.IdEstadio;    

                        aux.Nombre = resultEstadio.Nombre;

                        aux.Foto = resultEstadio.Foto;  

                        aux.Pais = new Pais();

                        aux.Pais.IdPais = resultEstadio.Pais.IdPais;

                        aux.Pais.Nombre = resultEstadio.Pais.Nombre;

                        estadio.Estadios.Add(aux);  
                    
                    
                    }
                
                
                }
            

            
            }

            return View(estadio);
        }

    }
}
