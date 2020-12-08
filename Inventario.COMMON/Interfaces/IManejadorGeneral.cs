using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;

namespace Inventario.COMMON.Interfaces
{
    public interface IManejadorGeneral<T> where T:Base
    {
        bool Agregar(T entidad);
        List<T> Listar { get; }

        bool Eliminar(string id);

        bool Modificar(T entidad);
        T BuscarPorId(string id);

    }
}
