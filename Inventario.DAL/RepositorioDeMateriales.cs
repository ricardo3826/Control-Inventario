using System;
using System.Collections.Generic;
using System.Text;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;
using System.Linq;
using LiteDB;

namespace Inventario.DAL
{
    public class RepositorioDeMateriales
    {
        private string DbName = "Inventario.db";
        private string TableName = "Materiales";

        public List<Materiales> Leer
        {
            get
            {
                List<Materiales> datos = new List<Materiales>();
                using (var db = new LiteDatabase(DbName))
                {
                    datos = db.GetCollection<Materiales>(TableName).FindAll().ToList();
                }
                return datos;
            } 
        }
            
        public bool Crear(Materiales entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();//Guid Asigna una cadena de 37 digitos para crear el Id para los empleados
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Materiales>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Editar(Materiales entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Materiales>(TableName);
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
                    var coleccion = db.GetCollection<Materiales>(TableName);
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

