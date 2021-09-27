using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using ARP.Ejemplo.WebExterno.Comun.Controladores;
using System.Configuration;
using BCSC.Comun.Encripcion;
using ARP.Ejemplo.Comun.Entidades;

namespace ARP.Ejemplo.WebExterno.Comun
{
    public enum TIPO_MENSAJE
    {
        Error,
        Aviso,
        Ok
    }

    public class WebPage : System.Web.UI.Page
    {
        #region Properties (2)

        public SeguridadSvc.SeguridadClient SeguridadClient { get; private set; }

        public string TextoMensaje { get; set; }

        #endregion Properties

        #region Methods (5)

        // Public Methods (2) 

        /// <summary>
        /// Elimina las tildes para el nombre del archivo
        /// </summary>
        /// <param name="pCadena"></param>
        /// <returns></returns>
        public string EliminarTildes(string pCadena)
        {
            pCadena = pCadena.Replace('á', 'a');
            pCadena = pCadena.Replace('é', 'e');
            pCadena = pCadena.Replace('í', 'i');
            pCadena = pCadena.Replace('ó', 'o');
            pCadena = pCadena.Replace('ú', 'u');
            pCadena = pCadena.Replace('Á', 'A');
            pCadena = pCadena.Replace('É', 'E');
            pCadena = pCadena.Replace('Í', 'I');
            pCadena = pCadena.Replace('Ó', 'O');
            pCadena = pCadena.Replace('Ú', 'U');
            return pCadena;
        }

        /// <summary>
        /// Obtiene el usuario Windows sin dominio
        /// </summary>
        /// <returns>Retorna el Usuario Windows sin dominio</returns>
        public string ObtenerUsuarioWindowsSinDominio()
        {
            string usuarioLimpio;
            if (Request.ServerVariables["AUTH_USER"].ToString().Contains(@"\") == true)
            {
                usuarioLimpio = Request.ServerVariables["AUTH_USER"].ToString().Substring(Request.ServerVariables["AUTH_USER"].ToString().IndexOf(@"\") + 1);
            }
            else
            {
                usuarioLimpio = Request.ServerVariables["AUTH_USER"].ToString();
            }
            return usuarioLimpio;
        }

        // Protected Methods (2) 
        protected virtual void Page_Load(object pSender, EventArgs pEventArgs)
        {
            if (Page.IsPostBack == false)
            {
                if ((User.Identity.IsAuthenticated == false))
                {
                    Response.Redirect(ResolveUrl("~/Inicio/InicioWin.aspx"));
                }

                if (Sesion.ObtenerValorSession<bool>(ValoresSesion.EsExterno) == false)
                {
                    if (string.IsNullOrEmpty(Request.ServerVariables["LOGON_USER"]))
                    {
                        Response.Redirect(ResolveUrl("~/Inicio/InicioWin.aspx"));
                    }
                    string dominioConUsuario = Request.ServerVariables["LOGON_USER"];
                    Autenticacion autenticacion = new Autenticacion()
                    {
                        Login = Encriptar(dominioConUsuario),
                        EsDominio = true
                    };
                    using (SeguridadSvc.SeguridadClient seguridadClient = new SeguridadSvc.SeguridadClient())
                    {
                        ResultadoAutenticacion resultadoAutenticacion = seguridadClient.AutenticarUsuario(autenticacion);

                        if (resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Ok)
                        {
                            Sesion.AsignarValorSession<UsuarioExterno>(ValoresSesion.Usuario, resultadoAutenticacion.Usuario);
                        }
                    }
                }
            }
            if (this.SeguridadClient == null)
            {
                this.SeguridadClient = new SeguridadSvc.SeguridadClient();
            }
        }

        private string Encriptar(string pTexto)
        {
            return Encripcion.Encriptar(pTexto, ConfigurationManager.AppSettings.Get("VectorDeInicializacion"), ConfigurationManager.AppSettings.Get("ClaveDeEncripcion"));
        }

        protected virtual void Page_UnLoad(EventArgs pEventArgs)
        {
            if (this.SeguridadClient != null)
            {
                if (this.SeguridadClient.State != System.ServiceModel.CommunicationState.Closed)
                {
                    this.SeguridadClient.Close();
                }
            }
        }

        // Private Methods (1) 

        private void AplicarRolloverBotones(Control pControl)
        {
            if (pControl.HasControls() == false)
            {
                return;
            }
            foreach (Control control in pControl.Controls)
            {
                if (control.GetType().Name == "ImageButton")
                {
                    string imageUrl = ResolveUrl((control as ImageButton).ImageUrl);
                    (control as ImageButton).Attributes.Add("onMouseOver", String.Format("this.src='{0}'", imageUrl.Replace(".png", "_sel.png")));
                    (control as ImageButton).Attributes.Add("onMouseOut", String.Format("this.src='{0}'", imageUrl));
                }
                AplicarRolloverBotones(control);
            }
        }

        #endregion Methods

        #region Ordenamiento de listas para interfaz grafica (GridView)

        internal List<T> OrdenarLista<T>(List<T> pLista, string pOrdenarPor, string pDireccionOrden)
        {
            return default(List<T>);
        }

        private object GetPropertyValue<T>(T pObjeto, string pOrdenarPor)
        {
            PropertyInfo prop = pObjeto.GetType().GetProperty(pOrdenarPor);
            return prop.GetValue(pObjeto, null);
        }

        #endregion

        #region Permisos del usuario

        /// <summary>
        /// Roles del usuario
        /// </summary>
        /// <returns>Lista con los roles del usuario actual</returns>
        internal List<string> ObtenerRolesUsuario(string pLoginUsuario)
        {
            List<String> roles = Sesion.ObtenerValorSession<List<string>>(ValoresSesion.RolesUsuario);
            if (roles == null)
            {
                Roles.SEG2000Provider segProvider = new Roles.SEG2000Provider();
                segProvider.ApplicationName = ConfigurationManager.AppSettings.Get("ObjetoBaseSEG2000");
                roles = segProvider.GetRolesForUser(pLoginUsuario).ToList<string>();
                if (roles.Count == 0)
                {
                    Response.Redirect(ResolveUrl("~/ErrorAcceso.htm"), false);
                }
                else
                {
                    Sesion.AsignarValorSession<List<string>>(ValoresSesion.RolesUsuario, roles);
                }
            }
            return roles;
        }

        /// <summary>
        /// Permisos del usuario
        /// </summary>
        /// <returns>Lista con los permisos del usuario actual</returns>
        internal List<string> ObtenerPermisosUsuario(string pLoginUsuario)
        {
            List<String> permisos = Sesion.ObtenerValorSession<List<string>>(ValoresSesion.PermisosUsuario);
            if (permisos == null)
            {
                Roles.OptionsProvider optionsProvider = new Roles.OptionsProvider();
                permisos = optionsProvider.GetOptionsForUser(pLoginUsuario);
                if (permisos.Count == 0)
                {
                    Response.Redirect(ResolveUrl("~/ErrorAcceso.htm"), false);
                }
                else
                {
                    Sesion.AsignarValorSession<List<string>>(ValoresSesion.PermisosUsuario, permisos);
                }
            }
            return permisos;
        }

        #endregion
    }
}