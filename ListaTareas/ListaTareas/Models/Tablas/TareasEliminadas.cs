using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaTareas.Models.Tablas
{
    [Table("TareasEliminadas")]
    public class TareasEliminadas
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
