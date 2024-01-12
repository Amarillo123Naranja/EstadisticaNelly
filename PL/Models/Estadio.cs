namespace PL.Models
{
    public class Estadio
    {

        public int IdEstadio { get; set; }  

        public string Nombre { get; set; }  

        public string Foto { get; set; }    

        public List<Object> Estadios { get; set; }  

        public Models.Pais Pais { get; set; }




    }
}
