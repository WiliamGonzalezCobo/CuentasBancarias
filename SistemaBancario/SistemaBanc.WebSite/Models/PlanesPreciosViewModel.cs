namespace SistemaEncuestas.WebSite.Models
{
    #region Namespaces

    using System.Collections.Generic;

    #endregion

    public class PlanesPreciosViewModel
    {
        #region Constructor

        public PlanesPreciosViewModel()
        {
            Items = new List<string>();
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public bool Promocion { get; set; }

        public List<string> Items { get; set; }

        #endregion
    }
}