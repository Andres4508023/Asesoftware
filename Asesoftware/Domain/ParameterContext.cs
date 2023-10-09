using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Domain
{
   public class ParameterContext : DbContext
    {
        public ParameterContext(DbContextOptions<ParameterContext> options) : base(options)
        {
        }
        public DbSet<Servicios>Servicios { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<Comercios> Comercios { get; set; }
        public DbSet<TurnoInfo> TurnoInfo { get; set; }

    }
}
