﻿using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using System.Linq;
using Inventario.COMMON.Entidades;
using Inventario.COMMON.Interfaces;


namespace Inventario.DAL
{
    public class RepositorioDeChecks:IRepositorio<Check>
    {
        private string DbName = @"C:\Inventario\Inventario.db";
        private string TableName = "Check";

        public List<Check> Leer
        {
            get
            {
                List<Check> datos = new List<Check>();
                using (var db = new LiteDatabase(DbName))
                {
                    datos = db.GetCollection<Check>(TableName).FindAll().ToList();
                }
                return datos;
            }

        }
        public bool Crear(Check entidad)
        {
            entidad.Id = Guid.NewGuid().ToString();//Guid Asigna una cadena de 37 digitos para crear el Id para los empleados
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Check>(TableName);
                    coleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Editar(Check entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DbName))
                {
                    var coleccion = db.GetCollection<Check>(TableName);
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
                    var coleccion = db.GetCollection<Check>(TableName);
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

