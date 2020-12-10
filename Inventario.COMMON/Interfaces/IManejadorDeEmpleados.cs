using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using System.Collections;

namespace Inventario.COMMON.Interfaces
{
    public interface IManejadorDeEmpleados:IManejadorGeneral<Empleado>
    {
        List<Empleado> EmpleadosPorArea(string Area);
        
    }
}
