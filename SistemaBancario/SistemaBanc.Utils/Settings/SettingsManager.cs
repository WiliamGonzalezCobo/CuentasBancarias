namespace SistemaBanc.Utilidades.Settings
{
    #region Namespaces

    using System.Configuration;

    #endregion

    public class SettingsManager
    {
        #region Public Methods

        public static string URLServicio
        {
            get { return GetSettings("URLServicio"); }
        }

        public static string SMTP
        {
            get { return GetSettings("SMTP"); }
        }

        public static string Puerto
        {
            get { return GetSettings("Puerto"); }
        }

        public static string UsuarioCorreo
        {
            get { return GetSettings("UsuarioCorreo"); }
        }

        public static string ContrasenaCorreo
        {
            get { return GetSettings("ContrasenaCorreo"); }
        }

        public static string CorreoContacto
        {
            get { return GetSettings("correoContacto"); }
        }

        #endregion

        #region Private Methods

        private static string GetSettings(string key)
        {
            return ConfigurationManager.AppSettings[key]==null?"":ConfigurationManager.AppSettings[key].ToString();
        }

        #endregion
    }
}
