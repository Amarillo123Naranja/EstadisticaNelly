using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PL.Models;

namespace PL.Controllers
{
    public class JugadorController : Controller
    {

        [HttpGet]   
        public IActionResult GetAll()
        {

            Models.Jugador jugador = new Models.Jugador();  

            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var responseTask = client.GetAsync("jugador");
                responseTask.Wait();

                var resultServicio = responseTask.Result;

                if (resultServicio.IsSuccessStatusCode)
                {
                    var readTask = resultServicio.Content.ReadAsAsync<List<Jugador>>();
                    readTask.Wait();

                    jugador.Jugadores = new List<object>();

                    foreach (var resultJugador in readTask.Result)
                    {
                        Models.Jugador aux = new Models.Jugador();

                        aux.IdJugador = resultJugador.IdJugador;

                        aux.Nombre = resultJugador.Nacionalidad;

                        aux.ApellidoPaterno = resultJugador.ApellidoPaterno;

                        aux.ApellidoMaterno = resultJugador.ApellidoMaterno;

                        aux.Foto = resultJugador.Foto;

                        aux.Nacionalidad = resultJugador.Nacionalidad;

                        aux.Pais = new Models.Pais();

                        aux.Pais.IdPais = resultJugador.Pais.IdPais;

                        aux.Pais.Nombre = resultJugador.Pais.Nombre;

                        aux.FechaNacimiento = resultJugador.FechaNacimiento;

                        jugador.Jugadores.Add(aux); 

                    }

                }
            
            
            }


            return View(jugador);
        }


        [HttpGet]
        public IActionResult Form(int? IdJugador)
        {
            Jugador jugador = new Jugador();

            if(IdJugador != 0)
            {
                using(var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");
                    var responseTask = client.GetAsync("jugador/" + IdJugador);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsStringAsync();
                        dynamic resultJson = JArray.Parse(readTask.Result);
                        readTask.Wait();

                        jugador.IdJugador = resultJson[0]["idJugador"];
                        jugador.Nombre = resultJson[0]["nombre"];
                        jugador.ApellidoPaterno = resultJson[0]["apellidoPaterno"];
                        jugador.ApellidoMaterno = resultJson[0]["apellidoMaterno"];
                        jugador.Foto = resultJson[0]["foto"];
                        jugador.Pais.IdPais = resultJson[0]["nacionalidad"]["idPais"];
                        jugador.Pais.Nombre = resultJson[0]["nacionalidad"]["nombre"];
                    }
                
                
                }
            }
            else
            {
                return View();
            }


            return View(jugador);
        }



        [HttpPost]
        public IActionResult Form(Jugador jugador)
        {

            if(jugador.IdJugador == 0)
            {
                using(var client  = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");
                    var postTask = client.PostAsJsonAsync("jugador", jugador);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View(jugador);
                    }

                
                
                }
            }
            else
            {
                using(var client = new HttpClient()) 
                {
                    int IdJugador = jugador.IdJugador;
                    client.BaseAddress = new Uri("http://192.168.0.123/api/");

                    var putTask = client.PutAsJsonAsync("jugador/" + IdJugador, jugador);
                    putTask.Wait(); 

                    var resultServicio = putTask.Result;    

                    if(resultServicio.IsSuccessStatusCode) 
                    {
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View(jugador);   
                    }

                
                }
            }



        }



        public IActionResult Delete(int IdJugador)
        {
            using(var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("http://192.168.0.123/api/");
                var deletedTask = client.DeleteAsync("jugador/" + IdJugador);

                deletedTask.Wait();

                var resultServicio = deletedTask.Result;    

                if(resultServicio.IsSuccessStatusCode) 
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
