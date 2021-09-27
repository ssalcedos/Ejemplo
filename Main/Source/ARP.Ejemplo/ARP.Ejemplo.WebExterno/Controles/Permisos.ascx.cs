using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARP.Ejemplo.WebExterno.Comun;

namespace ARP.Ejemplo.WebExterno.Controles
{
    public partial class Permisos : WebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {

                string loginSinDominio = WebPage.User.Identity.Name.Split('\\')[1];

                CargarPerfiles(loginSinDominio);

                CargarPermisos(loginSinDominio);
            }
        }

        private void CargarPerfiles(string pLoginSinDominio)
        {
            List<string> perfiles = WebPage.ObtenerRolesUsuario(pLoginSinDominio);

            gvwPerfiles.DataSource = perfiles;
            gvwPerfiles.DataBind();

            if (perfiles.Count() > 0)
            {
                gvwPerfiles.HeaderRow.Visible = false;
            }
        }

        private void CargarPermisos(string pLoginSinDominio)
        {
            List<string> permisos = WebPage.ObtenerPermisosUsuario(pLoginSinDominio);

            gvwPermisos.DataSource = permisos;
            gvwPermisos.DataBind();

            if (permisos.Count() > 0)
            {
                gvwPermisos.HeaderRow.Visible = false;
            }
        }
    }
}