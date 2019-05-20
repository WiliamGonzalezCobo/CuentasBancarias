using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaBanc.Utilidades.Settings;
using SistemaBanc.LogicaNegocio.WebApiClient;

namespace SistemaBanc.WebCliente.Controllers
{
    public class BaseController : Controller
    {
        #region Attributes

        private readonly string _urlServicio = SettingsManager.URLServicio;
        private  UsuarioClient _clienteUsuario;
        private  CuentaClient _clienteCuenta;
        private  TransaccionClient _clienteTransaccion;

        #endregion

        #region Properties

        public UsuarioClient ClienteUsuario
        {
            get
            {
                if (_clienteUsuario == null)
                {
                    _clienteUsuario = new UsuarioClient(_urlServicio);
                }

                return _clienteUsuario;
            }
        }

        public CuentaClient ClienteCuenta
        {
            get
            {
                if (_clienteCuenta == null)
                {
                    _clienteCuenta = new CuentaClient(_urlServicio);
                }

                return _clienteCuenta;
            }
        }

        public TransaccionClient ClienteTransaccion
        {
            get
            {
                if (_clienteTransaccion == null)
                {
                    _clienteTransaccion = new TransaccionClient(_urlServicio);
                }

                return _clienteTransaccion;
            }
        }

        #endregion

    }

}