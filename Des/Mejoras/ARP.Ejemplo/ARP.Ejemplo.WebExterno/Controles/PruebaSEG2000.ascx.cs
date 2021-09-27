using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ARP.Roles;

namespace ARP.Ejemplo.WebExterno.Controles
{
    public partial class PruebaSEG2000 : Comun.WebUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarPermisos();
        }

        private void ValidarPermisos()
        {
            string usuario = "G651Temp";
            OptionsProvider optionsProvider = new OptionsProvider();
            List<string> permisos = optionsProvider.GetOptionsForUser(usuario);
            lblPermisoUno.Text = permisos.IndexOf("PermisoUno") >= 0 ? "Tiene el permiso uno." : "";
            lblPermisoDos.Text = permisos.IndexOf("PermisoDos") >= 0 ? "Tiene el permiso dos." : "";
            lblPermisoTres.Text = permisos.IndexOf("PermisoTres") >= 0 ? "Tiene el permiso tres." : "";
        }
    }
}