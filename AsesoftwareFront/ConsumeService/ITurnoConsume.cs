using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace ConsumeService
{
    public interface ITurnoConsume
    {
        Task<bool> SaveTurnos(Turnos model);
        Task<List<Servicios>> GetServicios();
        Task<List<TurnoInfo>> GetTurnos();
    }
}
