namespace PL.Models
{
    public class Equipo
    {
        public int IdEquipo { get; set; }

        public string Nombre { get; set; }  

        public List<Object> Equipos { get; set; }   

        public Models.Pais Pais { get; set; }


    }
}
