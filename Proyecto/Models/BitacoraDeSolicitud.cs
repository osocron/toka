using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Proyecto.Models.EntityModel;

namespace Proyecto.Models
{
    public class BitacoraConAutos
    {
        public int IdSolicitud { get; set; }
        public List<AutoConSeleccion> AutosConSeleccion { get; set; }
    }

    public class AutoConSeleccion
    {
        public Autos Auto { get; set; }
        public bool Seleccionado { get; set; }
    }
}