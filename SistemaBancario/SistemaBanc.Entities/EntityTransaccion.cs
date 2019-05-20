using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBanc.Entities
{
    public class EntityTransaccion
    {
        public int id_transaccion { get; set; }

        public int valor { get; set; }

        public DateTime fecha { get; set; }

        public int id_tipo_transaccion { get; set; }

        public int id_Cuenta { get; set; }

        public int id_CuentaDestino { get; set; }

        public int id_Cliente { get; set; }

}
}
