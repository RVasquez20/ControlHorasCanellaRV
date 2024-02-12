using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDeHorasCanellaRV
{
    internal class Registro
    {
        public string Codigo { get; set; }
        public string Colaborador { get; set; }
        public string JefeProyecto { get; set; }
        public string CodigoProyecto { get; set; }
        public string Proyecto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public double CantidadHoras { get; set; }
        public string Etapa { get; set; }
        public string TipoActividad { get; set; }
        public string Descripcion { get; set; }
    }
}
