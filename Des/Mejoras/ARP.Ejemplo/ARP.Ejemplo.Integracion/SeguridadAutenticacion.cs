using ARP.Ejemplo.Comun.Entidades;
using ARP.Ejemplo.Integracion.SeguridadAutenticacionSvc;

namespace ARP.Ejemplo.Integracion
{
    public class SeguridadAutenticacion
    {
        #region Fields (1)

        /// <summary>
        /// Cliente del servicio de AutenticacionSeguridad
        /// </summary>
        private static SeguridadAutenticacionClient _seguridadAutenticacionClient = null;

        #endregion Fields

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Metodo para Autenticar un usuario
        /// </summary>
        /// <returns></returns>
          public static ResultadoAutenticacion AutenticarUsuario(Autenticacion pAutenticacion)
        {
            try
            {
                _seguridadAutenticacionClient = new SeguridadAutenticacionClient();
                DatosIniciales datosIniciales = new DatosIniciales()
                {
                    Contrasena = pAutenticacion.Contrasena,
                    UsuarioLogin = pAutenticacion.Login,
                    TipoRed = pAutenticacion.EsDominio == true ? TIPO_RED.Intranet : TIPO_RED.Extranet
                };

                Resultado resultado = _seguridadAutenticacionClient.AutenticarUsuario(datosIniciales);

                ResultadoAutenticacion resultadoAutenticacion = new ResultadoAutenticacion();

                resultadoAutenticacion.RespuestaOperacion = new Respuesta()
                {
                    CodigoResultado = resultado.CodigoResultado == CODIGO_RESULTADO_TRANSACCION.Ok ? CODIGO_RESULTADO.Ok : CODIGO_RESULTADO.Error,
                    DetalleResultado = resultado.MensajeRespuesta
                };

                if (resultado.Usuario != null)
                {
                    resultadoAutenticacion.UsuarioAutenticado = resultado.Autenticado;
                    resultadoAutenticacion.UsuarioBloqueado = resultado.Usuario.Bloqueado;
                    resultadoAutenticacion.UsuarioActivo = resultado.Usuario.Activo;
                    resultadoAutenticacion.UsuarioConfirmoRegistro = resultado.Usuario.RegistroConfirmado;
                    resultadoAutenticacion.UsuarioContrasenaCaduca = resultado.Usuario.ContrasenaCaduca;
                    resultadoAutenticacion.UsuarioObligarCambioContrasena = resultado.Usuario.ObligarCambioContrasena == null ? false : (bool)resultado.Usuario.ObligarCambioContrasena;
                    resultadoAutenticacion.UrlCambioContrasena = resultado.Usuario.UrlCambioContrasena;
                    resultadoAutenticacion.Usuario = new UsuarioExterno()
                    {
                        Id = resultado.Usuario.Id,
                        PrimerNombre = resultado.Usuario.PrimerNombre,
                        SegundoNombre = resultado.Usuario.SegundoNombre,
                        PrimerApellido = resultado.Usuario.PrimerApellido,
                        SegundoApellido = resultado.Usuario.SegundoApellido,
                        NombreCompleto = resultado.Usuario.NombreCompleto,
                        Cargo = resultado.Usuario.Cargo
                    };
                }

                return resultadoAutenticacion;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (_seguridadAutenticacionClient != null)
                {
                    _seguridadAutenticacionClient.Close();
                }
            }
        }

        #endregion Methods
    }
}