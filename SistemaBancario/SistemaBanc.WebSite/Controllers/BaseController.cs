namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using SistemaBanc.LogicaNegocio;
    using System.Web.Mvc;
    using SistemaBanc.Utilidades.Settings;
    using SistemaBanc.LogicaNegocio.WebApiClient;

    #endregion

    public class BaseController : Controller
    {
        #region Attributes

        private readonly string _urlServicio = SettingsManager.URLServicio;

        private  UsuarioClient _clienteUsuarios;
        //private AuditoriaIngresoClient _clientAuditoriaIngreso;
        //private EmpresaClient _clienteEmpresa;
        //private EncuestaClient _clienteEncuesta;
        //private MetadataClient _clienteMetadata;
        //private PagoPlanClient _clientePagoPlan;
        //private PlanEncuestaClient _clientePlanEncuesta;
        //private RespuestaClient _clienteRespuesta;
        //private UsuarioEmpresaClient _clienteUsuario;

        #endregion

        #region Properties

        public UsuarioClient ClienteUsuario
        {
            get
            {
                if (_clienteUsuarios == null)
                {
                    _clienteUsuarios = new UsuarioClient(_urlServicio);
                }

                return _clienteUsuarios;
            }
        }

        //public AuditoriaIngresoClient ClienteAuditoriaIngreso
        //{
        //    get
        //    {
        //        if (_clientAuditoriaIngreso == null)
        //        {
        //            _clientAuditoriaIngreso = new AuditoriaIngresoClient(_urlServicio);
        //        }

        //        return _clientAuditoriaIngreso;
        //    }
        //}

        //public EmpresaClient ClienteEmpresa
        //{
        //    get
        //    {
        //        if (_clienteEmpresa == null)
        //        {
        //            _clienteEmpresa = new EmpresaClient(_urlServicio);
        //        }

        //        return _clienteEmpresa;
        //    }
        //}

        //public EncuestaClient ClienteEncuesta
        //{
        //    get
        //    {
        //        if (_clienteEncuesta == null)
        //        {
        //            _clienteEncuesta = new EncuestaClient(_urlServicio);
        //        }

        //        return _clienteEncuesta;
        //    }
        //}

        //public MetadataClient ClienteMetadata
        //{
        //    get
        //    {
        //        if (_clienteMetadata == null)
        //        {
        //            _clienteMetadata = new MetadataClient(_urlServicio);
        //        }

        //        return _clienteMetadata;
        //    }
        //}

        //public PagoPlanClient ClientePagoPlan
        //{
        //    get
        //    {
        //        if (_clientePagoPlan == null)
        //        {
        //            _clientePagoPlan = new PagoPlanClient(_urlServicio);
        //        }

        //        return _clientePagoPlan;
        //    }
        //}

        //public PlanEncuestaClient ClientePlanEncuesta
        //{
        //    get
        //    {
        //        if (_clientePlanEncuesta == null)
        //        {
        //            _clientePlanEncuesta = new PlanEncuestaClient(_urlServicio);
        //        }

        //        return _clientePlanEncuesta;
        //    }
        //}

        //public RespuestaClient ClienteRespuesta
        //{
        //    get
        //    {
        //        if (_clienteRespuesta == null)
        //        {
        //            _clienteRespuesta = new RespuestaClient(_urlServicio);
        //        }

        //        return _clienteRespuesta;
        //    }
        //}

        //public UsuarioEmpresaClient ClienteUsuario
        //{
        //    get
        //    {
        //        if (_clienteUsuario == null)
        //        {
        //            _clienteUsuario = new UsuarioEmpresaClient(_urlServicio);
        //        }

        //        return _clienteUsuario;
        //    }
        //}

        #endregion
    }
}