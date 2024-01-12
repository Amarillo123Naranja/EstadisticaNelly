namespace PL.Models
{
    public class Jugador
    {

        public int IdJugador { set; get; }
        
        public string Nombre { set; get; }  

        public string ApellidoPaterno { set; get; } 

        public string ApellidoMaterno { set; get; }

        public string Foto { set; get; }    

        public string Nacionalidad { set; get; }  

        public DateTime FechaNacimiento { set; get; }   
        
        public List<Object> Jugadores { set; get; } 

        public Models.Pais Pais { set; get; }






    }
}
