using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;

namespace Inventario.BIZ
{
    public class ManejadorDeMateriales : IManejadorDeMateriales
    {
        IRepositorio<Materiales> repositorio;
        public ManejadorDeMateriales(IRepositorio<Materiales> repo)
        {
            repositorio = repo;
        }
        public List<Materiales> Listar => repositorio.Leer;

        public bool Agregar(Materiales entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Materiales BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Delete(id);
        }

        public List<Materiales> MaterialesPorCategoria(string categoria)
        {
            return Listar.Where(e => e.Categoria == categoria).ToList();
        }

        public bool Modificar(Materiales entidad)
        {
          return repositorio.Editar(entidad);
        }
    }
}

