using System;
using System.Web.Security;
using ARP.Ejemplo.WebExterno.Comun;
using ARP.Ejemplo.WebExterno.Comun.Controladores;

namespace ARP.Ejemplo.WebExterno.Controles
{
    public partial class ValidacionWin : WebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarUsuarioWindows();
        }

        private void ValidarUsuarioWindows()
        {
            Sesion.AsignarValorSession<bool>(ValoresSesion.EsExterno, false);
            FormsAuthentication.RedirectFromLoginPage(Request.ServerVariables["LOGON_USER"], false);
        }
    }

}