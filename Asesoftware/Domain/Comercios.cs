using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
   public class Comercios
    {
        [Key]
        public int IdComercio { get; set; }
        public string Nom_Comercio { get; set; }
        public int Aforo_Maximo { get; set; }

    }
}
