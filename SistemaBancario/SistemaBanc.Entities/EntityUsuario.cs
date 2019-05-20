using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBanc.Entities
{
    public class EntityUsuario
    {
        public int id_usuario { get; set; }
        public string usuario1 { get; set; }
        public string contrasenia { get; set; }
        public int id_cliente { get; set; }
    }
}
