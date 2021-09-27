using System;
using System.Web;
using System.Globalization;

namespace ARP.Ejemplo.WebExterno
{
    public class Global : System.Web.HttpApplication
    {
        #region Methods (5)

        // Private Methods (5) 

        private readonly string _plantillamensaje = "Mensaje: {0}\nEXC: {1}";

        private void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        private void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception excepcionNoManejada = Server.GetLastError();
                if (excepcionNoManejada != null)
                {
                    // Obtiene excepción real
                    if (excepcionNoManejada is HttpUnhandledException)
                    {
                        excepcionNoManejada = excepcionNoManejada.InnerException;
                    }

                    //Omitir excepciones por viewstate invalido
                    bool escribirExcepcionEnLog = true;
                    if (excepcionNoManejada.Message.Contains("Invalid viewstate"))
                    {
                        escribirExcepcionEnLog = false;
                    }

                    //Omitir excepciones por desconexión del cliente
                    if (excepcionNoManejada.Message.Contains("The client disconnected"))
                    {
                        escribirExcepcionEnLog = false;
                    }

                    if (escribirExcepcionEnLog)
                    {
                        //// Registra excepción en log de eventos con Enterprise Library
                        string mensajeExcepcion = String.Format(CultureInfo.CurrentCulture, _plantillamensaje, excepcionNoManejada.Message, excepcionNoManejada.ToString());
                        System.Diagnostics.EventLog.WriteEntry("ARPEjemplo - WebExterno", mensajeExcepcion);
                    }
                }
            }
            catch
            {
                // Cualquier excepción al manejar el error se ignora
            }
        }

        private void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
        }

        private void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer
            // or SQLServer, the event is not raised.
        }

        private void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        #endregion Methods
    }
}