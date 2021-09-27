using System;

namespace ARP.Ejemplo.WebExterno.Comun.Controladores
{
    public enum ValoresSesion
    {
        PermisosUsuario,
        RolesUsuario,
        Usuario,
        EsExterno,
        UrlContraseña
    }

    public static class Sesion
    {
        private static System.Web.SessionState.HttpSessionState _httpSessionState;

        internal static void ObtenerValorSession<T>(ValoresSesion pValoresSesion, ref T pValor)
        {
            _httpSessionState = System.Web.HttpContext.Current.Session;
            if (_httpSessionState[pValoresSesion.ToString()] != null)
            {
                try
                {
                    pValor = (T)_httpSessionState[pValoresSesion.ToString()];
                }
                catch
                { }
            }
            else
            {
                _httpSessionState[pValoresSesion.ToString()] = pValor;
            }
        }

        internal static T ObtenerValorSession<T>(ValoresSesion pValoresSesion)
        {
            T pValor = default(T);
            _httpSessionState = System.Web.HttpContext.Current.Session;
            if (_httpSessionState[pValoresSesion.ToString()] != null)
            {
                try
                {
                    pValor = (T)_httpSessionState[pValoresSesion.ToString()];
                }
                catch
                { }
            }
            else
            {
                _httpSessionState[pValoresSesion.ToString()] = pValor;
            }

            return pValor;
        }

        internal static void ObtenerValorSession(ValoresSesion pValoresSesion, ref System.Web.UI.WebControls.Label pLabel)
        {
            _httpSessionState = System.Web.HttpContext.Current.Session;
            if (_httpSessionState[pValoresSesion.ToString()] != null)
            {
                pLabel.Text = _httpSessionState[pValoresSesion.ToString()].ToString();
            }
        }

        internal static void ObtenerValorSession(ValoresSesion pValoresSesion, ref System.Web.UI.WebControls.TextBox pTextBox)
        {
            _httpSessionState = System.Web.HttpContext.Current.Session;
            if (_httpSessionState[pValoresSesion.ToString()] != null)
            {
                pTextBox.Text = _httpSessionState[pValoresSesion.ToString()].ToString();
            }
        }

        internal static void ObtenerValorSession(ValoresSesion pValoresSesion, ref System.Web.UI.WebControls.DropDownList pDropDownList)
        {
            _httpSessionState = System.Web.HttpContext.Current.Session;
            if (_httpSessionState[pValoresSesion.ToString()] != null)
            {
                pDropDownList.SelectedValue = _httpSessionState[pValoresSesion.ToString()].ToString();
            }
        }

        internal static void AsignarValorSession<T>(ValoresSesion pValoresSesion, T pValor)
        {
            _httpSessionState = System.Web.HttpContext.Current.Session;
            _httpSessionState[pValoresSesion.ToString()] = pValor;
        }

        internal static void AsignarValorSession<T>(ValoresSesion pValoresSesion, T pValor, bool pValidar)
        {
            _httpSessionState = System.Web.HttpContext.Current.Session;
            if (pValidar)
            {
                if (_httpSessionState[pValoresSesion.ToString()] != null)
                {
                    if (String.IsNullOrEmpty(_httpSessionState[pValoresSesion.ToString()].ToString()))
                    {
                        _httpSessionState[pValoresSesion.ToString()] = pValor;
                    }
                }
                else
                {
                    _httpSessionState[pValoresSesion.ToString()] = pValor;
                }
            }
            else
            {
                _httpSessionState[pValoresSesion.ToString()] = pValor;
            }
        }
    }
}