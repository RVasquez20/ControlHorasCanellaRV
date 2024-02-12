using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeHorasCanellaRV
{
    internal class RegistrosSemanales
    {
        public List<Registro> Registros { get; set; } = new List<Registro>();
        public DateTime FechaInicioSemana { get; set; }
    }
}
