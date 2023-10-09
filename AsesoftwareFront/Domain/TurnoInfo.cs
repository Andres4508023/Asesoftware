using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
   public class TurnoInfo
    {
        [Key]
        public int Id_Turno { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string NomServicios { get; set; }
        public Boolean? Estado { get; set; }
    }
}
