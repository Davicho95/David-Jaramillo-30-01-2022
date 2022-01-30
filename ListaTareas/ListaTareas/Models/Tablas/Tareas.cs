using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaTareas.Models.Tablas
{
    [Table("Tareas")]
    public class Tareas
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Completa { get; set; }
    }
}
