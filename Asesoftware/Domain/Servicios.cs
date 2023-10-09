using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
   public class Servicios
    {
        [Key]
        public int Id_Servicios { get; set; }
        public int IdComercio { get; set; }
        public string Nom_Servicios { get; set; }
        public TimeSpan? Hora_Apertura { get; set; }
        public TimeSpan? Hora_Cierre { get; set; }
        public string Duracion { get; set; }
    }
}
