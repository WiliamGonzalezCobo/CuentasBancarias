using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBanc.Entities
{
    public class EntityCuenta
    {
        public int id_Cuenta { get; set; }

        public int saldo { get; set; }

        public DateTime fecha_creacion { get; set; }

        public int id_Cliente { get; set; }
    }
}
