using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;

namespace Inventario.BIZ
{
    public class ManejadorDeCheck : IManejadorDeCheck
    {

        IRepositorio<Check> repositorio;
        public ManejadorDeCheck(IRepositorio<Check> repo)
        {
            this.repositorio = repo;
        }

        public List<Check> Listar => repositorio.Leer;

        public bool Agregar(Check entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Check BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public bool Modificar(Check entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
