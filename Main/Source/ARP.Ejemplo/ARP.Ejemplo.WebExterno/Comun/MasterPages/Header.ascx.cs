using System;
using ARP.Ejemplo.Comun.Entidades;
using ARP.Ejemplo.WebExterno.Comun.Controladores;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using ARP.Ejemplo.WebExterno.Comun.Controladores;
using BCSC.Comun.Encripcion;
using ARP.Ejemplo.WebExterno.SeguridadSvc;

namespace ARP.Ejemplo.WebExterno.Comun.MasterPages
{
    public partial class Header : WebUserControl
    {
        #region Methods (1)

        // Protected Methods (1) 

        protected void Page_Load(object pSender, EventArgs pEventArgs)
        {
            if (Sesion.ObtenerValorSession<UsuarioExterno>(ValoresSesion.Usuario) != null)
            {
                pnlInformation.Visible = true;
                UsuarioExterno usuario = Sesion.ObtenerValorSession<UsuarioExterno>(ValoresSesion.Usuario);
                lblNombreFuncionario.Text = usuario.NombreCompleto;
                lblCargoUsuario.Text = usuario.Cargo;
                Perfiles_Lista.InnerHtml = String.Format("<ul>{0}</ul>", ObtenerRolesUsuario(WebPage.User.Identity.Name.Split('\\')[1]));
                AsignarUrlCambioContrasena();
            }
        }

        private string ObtenerRolesUsuario(string pLogin)
        {
            List<string> perfiles = WebPage.ObtenerRolesUsuario(pLogin);

            StringBuilder sbPerfiles = new StringBuilder();

            foreach (string perfil in perfiles)
            {
                sbPerfiles.AppendFormat("<li>{0}</li>", perfil);
            }
            return sbPerfiles.ToString();
        }


        private void AsignarUrlCambioContrasena()
        {
            lnkCambiarContraseña.Visible = String.IsNullOrEmpty(Sesion.ObtenerValorSession<string>(ValoresSesion.UrlContraseña)) == false;
            lnkCambiarContraseña.NavigateUrl = Sesion.ObtenerValorSession<string>(ValoresSesion.UrlContraseña);
        }
        #endregion Methods
    }
}