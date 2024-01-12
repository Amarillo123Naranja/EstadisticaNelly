using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PL.Models;

namespace PL.Controllers
{
    public class EquipoController : Controller
    {
        public IActionResult GetAll()
        {

            Models.Equipo equipo = new Models.Equipo(); 

            using(var client  = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var responseTask = client.GetAsync("equipo/getall");
                responseTask.Wait();    

                var resultServicio = responseTask.Result;

                if(resultServicio.IsSuccessStatusCode) 
                {
                    var readTask = resultServicio.Content.ReadAsAsync<List<Equipo>>();
                    readTask.Wait();

                    equipo.Equipos = new List<Object>();

                    foreach(var resultEquipo in readTask.Result) 
                    {
                        Equipo aux = new Equipo();  

                        aux.IdEquipo = resultEquipo.IdEquipo;   

                        aux.Nombre = resultEquipo.Nombre;   

                        aux.Pais = new Pais();

                        aux.Pais.IdPais = resultEquipo.Pais.IdPais;

                        aux.Pais.Nombre = resultEquipo.Pais.Nombre;

                        equipo.Equipos.Add(aux);    
                    
                    }

                }

            }


            return View(equipo);
        }


        public IActionResult Form(int? IdEquipo)
        {
            Models.Equipo equipo = new Models.Equipo();
            if(IdEquipo != 0)
            {
                using(var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");

                    var responseTask = client.GetAsync("equipo/getbyid/" + IdEquipo);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsStringAsync();
                        dynamic resultJson = JArray.Parse(readTask.Result);
                        readTask.Wait(); 
                        
                      
                    }

                
                
                }
            }



            return View();
        }





    }
}
