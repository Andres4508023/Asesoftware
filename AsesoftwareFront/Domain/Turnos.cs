using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Turnos
    {
        [Key]
        public int Id_Turno { get; set; }
        public int Id_Servicios { get; set; }
        public DateTime? Fecha_turno { get; set; }
        public DateTime? Fecha_inicio { get; set; }
        public DateTime? Fecha_Fin { get; set; }
        public byte Estado { get; set; }
    }
}
