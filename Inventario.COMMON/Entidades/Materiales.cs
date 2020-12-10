﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.COMMON.Entidades
{
    public class Materiales:Base
    {
        public string Nombre { get; set; }

        public string Categoria { get; set; }

        public string Descripcion { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Categoria, Nombre);
        }
    }
}
