namespace ARP.Ejemplo.WebExterno.Comun
{
    public class WebUserControl : System.Web.UI.UserControl
    {
        #region Properties (1)

        public WebPage WebPage
        {
            get
            {
                return this.Page as WebPage;
            }
        }

        #endregion Properties
    }
}