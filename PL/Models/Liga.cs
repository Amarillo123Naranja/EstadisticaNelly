using System.Security.Policy;

namespace PL.Models
{
    public class Liga
    {
        public int IdLiga { get; set; } 

        public string Nombre { get; set; }  

        public string Logo { get; set; }  
        
        public Object L { get; set; }
        public Models.Pais Pais { get; set; }


        public List<Object> Ligas { get; set; } 

    }
}
