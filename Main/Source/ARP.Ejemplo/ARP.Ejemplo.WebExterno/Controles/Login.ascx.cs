using System;
using System.Web.Security;
using ARP.Ejemplo.Comun.Entidades;
using ARP.Ejemplo.WebExterno.Comun;
using BCSC.Comun.Encripcion;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web;
using ARP.Ejemplo.WebExterno.Comun.Controladores;

namespace ARP.Ejemplo.WebExterno.Controles
{
    public partial class Login : UserControl
    {
        #region Methods (4)

        // Protected Methods (3) 

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string dominioConUsuario = String.Format(@"{0}\{1}", ddlDominios.SelectedItem.Value, txtUsuario.Text);
                Autenticacion autenticacion = new Autenticacion()
                {
                    Contrasena = Encriptar(txtContrasena.Text),
                    Login = Encriptar(dominioConUsuario),
                    EsDominio = false
                };
                using (SeguridadSvc.SeguridadClient seguridadClient = new SeguridadSvc.SeguridadClient())
                {
                    ResultadoAutenticacion resultadoAutenticacion = seguridadClient.AutenticarUsuario(autenticacion);

                    if (resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Ok)
                    {
                        FormsAuthentication.SetAuthCookie(dominioConUsuario, false);
                        Sesion.AsignarValorSession<UsuarioExterno>(ValoresSesion.Usuario, resultadoAutenticacion.Usuario);
                        Sesion.AsignarValorSession<string>(ValoresSesion.UrlContraseña, resultadoAutenticacion.UrlCambioContrasena);
                        Response.Redirect(ResolveUrl("~/Default.aspx"));
                    }
                    else if (resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error)
                    {
                        if (resultadoAutenticacion.UsuarioAutenticado == false)
                        {
                            if (resultadoAutenticacion.UsuarioActivo == false)
                            {
                                lblError.Text = MensajesAutenticacion.UsuarioInactivo;
                                return;
                            }
                            else if (resultadoAutenticacion.UsuarioBloqueado == true)
                            {
                                lblError.Text = MensajesAutenticacion.UsuarioBloqueado;
                                return;
                            }
                            else
                            {
                                lblError.Text = MensajesAutenticacion.UsuarioNoExiste;
                            }
                        }
                        else
                        {
                            if (resultadoAutenticacion.UsuarioObligarCambioContrasena == true)
                            {
                                Response.Redirect(resultadoAutenticacion.UrlCambioContrasena);
                            }
                        }
                    }
                }
            }
        }

        protected void lbkRecordarContrasena_Click(object sender, EventArgs e)
        {
            string urlObtenida = string.Empty;
            using (SeguridadSvc.SeguridadClient seguridadClient = new SeguridadSvc.SeguridadClient())
            {
                urlObtenida = seguridadClient.ObtenerUrl(new ContrasenaCambio() { TipoUsuario = TIPO_USUARIO.Externo }).UrlObtenida;
            }
            Response.Redirect(urlObtenida);
        }


        // Private Methods (1) 

        private string Encriptar(string pTexto)
        {
            return Encripcion.Encriptar(pTexto, ConfigurationManager.AppSettings.Get("VectorDeInicializacion"), ConfigurationManager.AppSettings.Get("ClaveDeEncripcion"));
        }

        #endregion Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Sesion.AsignarValorSession<bool>(ValoresSesion.EsExterno, true);
            DeshabilitarCachePagina();
            ValidarUsuarioAutenticado();
            if (Page.IsPostBack == false)
            {
                CargarDominios();
                CargarValidaciones();
            }
        }

        /// <summary>
        /// Valida si ya se encuentra algun usuario autenticado 
        /// y en caso afirmativo redirecciona a la pagina Default.aspx
        /// </summary>
        private void ValidarUsuarioAutenticado()
        {
            if (Page.User.Identity.IsAuthenticated == true)
            {
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            }
        }

        /// <summary>
        /// No permite que la pagina se guarde en cache para que siempre 
        /// genere postback y realice la validación de usuario autenticado
        /// </summary>
        private void DeshabilitarCachePagina()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
        }

        private void CargarValidaciones()
        {
            txtUsuario.Attributes.Add("onkeypress", "return validarTextoAlfaNumerico(event)");
        }

        private void CargarDominios()
        {
            string[] dominios = ConfigurationManager.AppSettings.Get("Dominios").Split(';');
            ddlDominios.DataSource = dominios;
            ddlDominios.DataBind();
            ddlDominios.Items.Insert(0, new ListItem(Mensajes.ListItem_Texto, Mensajes.ListItem_Valor));
        }
    }
}