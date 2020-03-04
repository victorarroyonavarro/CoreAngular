using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAngular.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public List<Direccion> Direcciones { get; set; }
    }
}
