using ARP.Ejemplo.Comun.Entidades;
using ARP.Ejemplo.Integracion.SeguridadUsuariosSvc;

namespace ARP.Ejemplo.Integracion
{
	public class SeguridadUsuarios
	{
		#region Public Members (3) 

		#region Methods (3) 

		/// <summary>
		/// Metodo para realizar el cambio de contraseña
		/// </summary>
		/// <param name="pCambioContrasena">Objeto con la información de cambio de cotraseña</param>
		/// <returns></returns>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 23/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este metodo ejecuta el cambio de contraseña con los datos dirtectamente contra
		/// servicio web sin pasar por la URL que deberia devolver en el caso del llamado por pantalla.
		/// Este metodo solo se llama con el fin de realizar las pruebas unitarias correspondientes.
		/// </remarks>
		public static Respuesta CambiarContrasena(ContrasenaCambio pCambioContrasena)
		{
			try
			{
				_seguridadUsuariosClient = new SeguridadUsuariosClient();

				CambioContrasena cambioContrasena = new CambioContrasena()
				{
					ContrasenaAnterior = pCambioContrasena.ContrasenaAnterior,
					IdUsuario = pCambioContrasena.IdUsuario,
					PreguntaSecreta = pCambioContrasena.PreguntaSecreta,
					RespuestaSecreta = pCambioContrasena.RespuestaSecreta,
					LoginUsuario = pCambioContrasena.LoginUsuario
				};
				ResultadoOperacion resultadoOperacion = _seguridadUsuariosClient.CambiarContrasena(cambioContrasena);
                
				return new Respuesta() 
				{
					CodigoObtenido = resultadoOperacion.CodigoObtenido, 
					CodigoResultado = resultadoOperacion.CodigoResultado == SeguridadUsuariosSvc.CODIGO_RESULTADO.Ok ? Comun.Entidades.CODIGO_RESULTADO.Ok : Comun.Entidades.CODIGO_RESULTADO.Error, 
					DetalleResultado = resultadoOperacion.DetalleResultado 
				};
			}
			catch
			{
				throw;
			}
			finally
			{
				if (_seguridadUsuariosClient != null)
				{
					_seguridadUsuariosClient.Close();
				}
			}
		}

		/// <summary>
		/// Metodo para obtener la url de cambio de contraseña
		/// </summary>
		/// <param name="pCambioContraseña">Objeto que recibe el tipo de entorno </param>
		/// <returns></returns>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 23/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este metodo recibe unicamente el dato de entorno en el que trabaja el 
		/// usuario (Extranet = usuario externo, Intranet = usuario interno) y realiza un llamado al servicio
		/// de adminSeguridad para obtener la url a donde el aplicativo debe redireccionar para que el usuario
		/// realice el cambio de contraseña
		/// </remarks>
		public static ContrasenaCambio ObtenerUrl(ContrasenaCambio pCambioContrasena)
		{
			try
			{
				_seguridadUsuariosClient = new SeguridadUsuariosClient();
                pCambioContrasena.UrlObtenida = _seguridadUsuariosClient.ObtenerUrlCambioContrasena(pCambioContrasena.TipoUsuario == TIPO_USUARIO.Externo ? TIPO_RED.Extranet : TIPO_RED.Intranet).UrlCambioContrasena;
				return pCambioContrasena;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (_seguridadUsuariosClient != null)
				{
					_seguridadUsuariosClient.Close();
				}
			}
		}

		/// <summary>
		/// Metodo para validar pregunta y respuesta secreta
		/// </summary>
		/// <param name="pCambioContraseña">los datos a validar </param>
		/// <returns></returns>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 23/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este metodo se encarga de enviar la informaion de login de usuario,
		/// Pregunta y Respuesta secreta y validar que sean correctos
		/// </remarks>
		public static ContrasenaCambio ValidarPreguntaRespuestaSecreta(ContrasenaCambio pCambioContrasena)
		{
			try
			{
				_seguridadUsuariosClient = new SeguridadUsuariosClient();
				CambioContrasena cambioContrasena = new CambioContrasena() 
				{ 
					LoginUsuario = pCambioContrasena.LoginUsuario,
					PreguntaSecreta = pCambioContrasena.PreguntaSecreta,
					RespuestaSecreta = pCambioContrasena.RespuestaSecreta
				};
				pCambioContrasena.IdUsuario = _seguridadUsuariosClient.ValidarPreguntaRespuestaSecreta(cambioContrasena).Id;
				return pCambioContrasena;
			}
			catch
			{
				throw;
			}
			finally
			{
				if (_seguridadUsuariosClient != null)
				{
					_seguridadUsuariosClient.Close();
				}
			}
		}

		#endregion Methods 

		#endregion Public Members 

		#region Non-Public Members (1) 

		#region Fields (1) 

		/// <summary>
		/// Cliente del servicio de SeguridadUsuarios
		/// </summary>
		private static SeguridadUsuariosClient _seguridadUsuariosClient = null;

		#endregion Fields 

		#endregion Non-Public Members 
	}
}