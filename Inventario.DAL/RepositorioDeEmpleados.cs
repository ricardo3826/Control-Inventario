using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using System;
using LiteDB;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Inventario.DAL
{
    public class RepositorioDeEmpleados : IRepositorio<Empleado>
    {
        private string DbName = "Inventario.db";
        private string TableName = "Empleados";

        public List<Empleado> Leer {
            get
            {
                List<Empleado> datos = new List<Empleado>();
                using(var db= new LiteDatabase(DbName))
                {
                    datos = db.GetCollection<Empleado>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Empleado entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();//Guid Asigna una cadena de 37 digitos para crear el Id para los empleados
            try {
                using (var db= new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar( Empleado entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    coleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Empleado>(TableName);
                    r = coleccion.DeleteMany(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
            
    }
}

