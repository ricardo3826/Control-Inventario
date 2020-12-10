using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using System.Collections;

namespace Inventario.BIZ
{
    public class ManejadorDeEmpleados : IManejadorDeEmpleados
    {
        IRepositorio<Empleado> repositorio;
        public ManejadorDeEmpleados(IRepositorio<Empleado> repo)
        {
            repositorio = repo;
        }
        public List<Empleado> Listar => repositorio.Leer.OrderBy(p => p.Nombre).ToList();

        public bool Agregar(Empleado entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Empleado BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public List<Empleado> EmpleadosPorArea(string area)
        {
           return Listar.Where(e => e.Area == area).ToList();
        }

        public bool Modificar(Empleado entidad)
        {
            return repositorio.Editar(entidad);

        }
    }
}
