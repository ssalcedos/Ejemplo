using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARP.Ejemplo.Negocio;
using ARP.Ejemplo.Comun.Entidades;
using BCSC.Comun.Encripcion;
using System.Configuration;

namespace ARP.Ejemplo.Pruebas
{
	[TestClass]
	public class CambioDeContrasena
	{
		#region Public Members (21) 

		#region Constructors (1) 

		public CambioDeContrasena() { }

		#endregion Constructors 
		#region Properties (1) 

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return _testContextInstance;
			}
			set
			{
				_testContextInstance = value;
			}
		}

		#endregion Properties 
		#region Methods (19) 

		/// <summary>
		/// Probar que cambie la clave cumpliendo con todas las restricciones
		/// de acuerdo a la configuracion del usuario
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: A cada usuario se le puede configurar que su cumpla con ciertas restricciones,
		/// Este usuario las tiene todas y la prueba debe ser cambio de clave exitoso
		/// </remarks>
		[TestMethod]
		public void ClaveCumpleRestriccionesExitosamente()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("Abc123!!"),
				IdUsuario = 47
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Ok
				&& respuesta.DetalleResultado.Contains("Se ha actualizado la contraseña."));
		}

		/// <summary>
		/// Probar que obligue la longitud minima de caracteres (8)
		/// de a cuerdo a la configuracion del usuario
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este usuario entre otras restricciones debe tener 
		/// una longitud minima (8) la clave nueva que digite.
		/// </remarks>
		[TestMethod]
		public void ClaveNoCumpleLongitudMinima()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("a"),
				IdUsuario = 47
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains("La contraseña debe contener por lo menos"));
		}

		/// <summary>
		/// Probar que obligue a la clave a contener caracteres especiales
		/// de a cuerdo a la configuracion del usuario
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este usuario entre otras restricciones debe tener 
		/// Caracteres especiales la clave nueva que digite.
		/// </remarks>
		[TestMethod]
		public void ClaveNoCumpleRetriccionCaracteresEspeciales()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("aaaaAAAA"),
				IdUsuario = 47
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains("La contraseña debe contener por lo menos 1 carácter especial:"));
		}

		/// <summary>
		/// Probar que obligue a la clave a contener mayusculas de acuerdo
		/// a la configuracion del usuario
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este usuario entre otras restricciones debe tener 
		/// mayusculas la clave nueva que digite.
		/// </remarks>
		[TestMethod]
		public void ClaveNoCumpleRetriccionMayusculas()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("aaaa123!"),
				IdUsuario = 47
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains("La contraseña debe contener al menos una mayúscula."));
		}

		/// <summary>
		/// Probar que obligue a la clave a contener minusculas de acuerdo
		/// a la configuracion del usuario
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este usuario entre otras restricciones debe tener 
		/// minusculas la clave nueva que digite.
		/// </remarks>
		[TestMethod]
		public void ClaveNoCumpleRetriccionMinusculas()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("AAAA123!"),
				IdUsuario = 47
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains("La contraseña debe contener al menos una minúscula."));
		}

		/// <summary>
		/// Probar que obligue a la clave a contener nuemeros de acuerdo
		/// a la configuracion del usuario
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este usuario entre otras restricciones debe tener 
		/// numeros la clave nueva que digite.
		/// </remarks>
		[TestMethod]
		public void ClaveNoCumpleRetriccionNumeros()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("aaaaAAAA"),
				IdUsuario = 47
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains("La contraseña debe contener al menos un número."));
		}

		/// <summary>
		/// Probar el cambio de clave de un usuario nuevo sea exitoso
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Este usuario no debe cumplir ninguna de las restricciones
		/// </remarks>
		[TestMethod]
		public void ClaveNoDebeCumplirRestriccionesExitosamente()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("contra123"),
				IdUsuario = 48
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Ok
				&& respuesta.DetalleResultado.Contains("Se ha actualizado la contraseña."));
		}

		/// <summary>
		/// Probar que no permita usa una clave que se alla asignado entre
		/// las ultimas 12 (el numero de contraseñas a tener en cuenta es
		/// parametrizable en el servicio)
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción:La restriccion para todos los usuarios es que la clave que va a 
		/// asignar no sea alla usado anteriormente. Este usuario ya habia usado la clave que va a asignar
		/// </remarks>
		[TestMethod]
		public void ClaveUsadaAnteriormente()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("Carlos12318"),
				ContrasenaNueva = Encriptar("Carlos1239"),
				IdUsuario = 2
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains("La contraseña ingresada ya fue usada, por favor cámbiela."));
		}

		/// <summary>
		/// Probar que no permita usa una clave que se alla asignado entre
		/// las ultimas 12 (el numero de contraseñas a tener en cuenta es
		/// parametrizable en el servicio) y que valide si la clave anterior es erronea
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción:La restriccion para todos los usuarios es que la clave que va a 
		/// asignar no sea alla usado anteriormente. Este usuario ya habia usado la clave que va a asignar
		/// pero digito mal su contraseña anterior
		/// </remarks>
		[TestMethod]
		public void ClaveUsadaAnteriormenteClaveAnteriorErronea()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("Carlos987"),
				ContrasenaNueva = Encriptar("Carlos1239"),
				IdUsuario = 2
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains(""));
		}

		/// <summary>
		/// Obtener Url de cambio de contraseña para usuario externo
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Obtiene la url a la que debe redirigir el aplicativo para 
		/// cambio de contraseña de usuarios externos
		/// </remarks>
		[TestMethod]
		public void ObtenerUrlExterno()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				TipoUsuario = TIPO_USUARIO.Externo
			};
			contrasenaCambio = seguridad.ObtenerUrl(contrasenaCambio);
			Assert.IsTrue(string.IsNullOrEmpty(contrasenaCambio.UrlObtenida) == false);
		}

		/// <summary>
		/// Obtener Url de cambio de contraseña para usuario interno
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Obtiene la url a la que debe redirigir el aplicativo para 
		/// cambio de contraseña de usuarios internos
		/// </remarks>
		[TestMethod]
		public void ObtenerUrlInterno()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				TipoUsuario = TIPO_USUARIO.Interno
			};
			contrasenaCambio = seguridad.ObtenerUrl(contrasenaCambio);
			Assert.IsTrue(string.IsNullOrEmpty(contrasenaCambio.UrlObtenida) == false);
		}

		/// <summary>
		/// Probar que valida exitosamente la pregunta y la respuesta secreta
		/// para el login indicado y permite el cambio de clave
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario desea recordar su clave el primer paso es validar
		/// pregunta y respuesta secreta del login del usuario y luego permitir el cambio de clave
		/// </remarks>
		[TestMethod]
		public void RecordarContrasena()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				LoginUsuario = @"seguridad\g221temp",
				PreguntaSecreta = "primer apellido?",
				RespuestaSecreta = "Piza"
			};

			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);

			if (respuesta.CodigoResultado == CODIGO_RESULTADO.Ok)
			{
				contrasenaCambio.ContrasenaNueva = Encriptar("Contra124!");
				contrasenaCambio.IdUsuario = 2;
				respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			}
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Ok
				&& respuesta.DetalleResultado.Contains("Se ha actualizado la contraseña."));
		}

		/// <summary>
		/// Probar que valida la pregunta secreta erronea
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario desea recordar su clave el primer paso es validar
		/// pregunta y respuesta secreta del login del usuario. Este metodo valida que responda 
		/// error cuando la pregunta es erronea
		/// </remarks>
		[TestMethod]
		public void RecordarContrasenaPregErronea()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				LoginUsuario = @"seguridad\g221temp",
				PreguntaSecreta = "primer apellido????",
				RespuestaSecreta = "Piza"
			};

			contrasenaCambio = seguridad.ValidarPreguntaRespuestaSecreta(contrasenaCambio);
			Assert.IsTrue(contrasenaCambio.IdUsuario == 0);
		}

		/// <summary>
		/// Probar que valida la pregunta y la respuesta secreta erroneos
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario desea recordar su clave el primer paso es validar
		/// pregunta y respuesta secreta del login del usuario. Este metodo valida que responda 
		/// error cuando la pregunta y la respuesta secreta son erroneos
		/// </remarks>
		[TestMethod]
		public void RecordarContrasenaPregRespErroneos()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				LoginUsuario = @"seguridad\g221temp",
				PreguntaSecreta = "primer apellido????",
				RespuestaSecreta = "Pizaaaa"
			};

			contrasenaCambio = seguridad.ValidarPreguntaRespuestaSecreta(contrasenaCambio);
			Assert.IsTrue(contrasenaCambio.IdUsuario == 0);
		}

		/// <summary>
		/// Probar que valida la respuesta secreta erronea
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario desea recordar su clave el primer paso es validar
		/// pregunta y respuesta secreta del login del usuario. Este metodo valida que responda 
		/// error cuando la respuesta es erronea
		/// </remarks>
		[TestMethod]
		public void RecordarContrasenaRespErronea()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				LoginUsuario = @"seguridad\g221temp",
				PreguntaSecreta = "primer apellido?",
				RespuestaSecreta = "Pizaaaaaa"
			};

			contrasenaCambio = seguridad.ValidarPreguntaRespuestaSecreta(contrasenaCambio);
			Assert.IsTrue(contrasenaCambio.IdUsuario == 0);
		}

		/// <summary>
		/// Probar el cambio de clave de un usuario que no recuerda nada sea exitoso
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario es no recuerda nada debe cambiar la clave que genero automaticamente
		/// en el aplicativo al ser ingresado y crear de nuevo su pregunta y respuesta secreta
		/// </remarks>
		[TestMethod]
		public void UsuarioNoRecuerdaNada()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("Contra123!"),
				IdUsuario = 49,
				PreguntaSecreta = "PreguntaSecreta",
				RespuestaSecreta = "RespuestaSecreta"
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Ok
				&& respuesta.DetalleResultado.Contains("Se ha actualizado la contraseña."));
		}

		/// <summary>
		/// Probar el cambio de clave de un usuario que no recuerda nada sea erroneo
		/// por que incluye su login de usuario en cualquiera de las dos.
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario es no recuerda nada debe cambiar la clave que genero automaticamente
		/// en el aplicativo al ser ingresado y crear de nuevo su pregunta y respuesta secreta
		/// en este caso por incluir su login de usuario en la pregunta 
		/// </remarks>
		[TestMethod]
		public void UsuarioNoRecuerdaNadaIncluyeLogin()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("Contra123!"),
				IdUsuario = 49,
				PreguntaSecreta = "norecuerdanada",
				RespuestaSecreta = "RespuestaSecreta"
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Ok
				&& respuesta.DetalleResultado.Contains("Se ha actualizado la contraseña."));
		}

		/// <summary>
		/// Probar el cambio de clave de un usuario que no recuerda nada sea erroneo
		/// por que incluye su nombre en cualquiera de las dos.
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario es no recuerda nada debe cambiar la clave que genero automaticamente
		/// en el aplicativo al ser ingresado y crear de nuevo su pregunta y respuesta secreta
		/// en este caso por incluir su nombre completo en la pregunta tal como esta en la base
		/// el el campo "NombreCompleto" deberia no permitir que realice las asignaciones
		/// </remarks>
		[TestMethod]
		public void UsuarioNoRecuerdaNadaIncluyeNombre()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("Contra123!"),
				IdUsuario = 49,
				PreguntaSecreta = "Usuario NoRecuerda Pregunta NiRespuesta",
				RespuestaSecreta = "RespuestaSecreta"
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Error
				&& respuesta.DetalleResultado.Contains(""));
		}

		/// <summary>
		/// Probar el cambio de clave de un usuario nuevo sea exitoso
		/// </summary>
		/// <remarks>
		/// Autor: Wendy Murillo - g619temp
		/// FechaDeCreacion: 24/06/2011
		/// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
		/// FechaDeUltimaModificacion: (dd/MM/yyyy)
		/// EncargadoSoporte: Wendy Murillo - g619temp
		/// Descripción: Cuando el usuario es nuevo debe cambiar la clave que genero automaticamente
		/// el aplicativo al ser ingresado
		/// </remarks>
		[TestMethod]
		public void UsuarioNuevoClaveNuevaExitosa()
		{
			Seguridad seguridad = new Seguridad();
			ContrasenaCambio contrasenaCambio = new ContrasenaCambio()
			{
				ContrasenaAnterior = Encriptar("123"),
				ContrasenaNueva = Encriptar("Contra123!"),
				IdUsuario = 44,
				PreguntaSecreta = "PreguntaSecreta",
				RespuestaSecreta = "RespuestaSecreta"
			};
			Respuesta respuesta = seguridad.CambiarContrasena(contrasenaCambio);
			Assert.IsTrue(respuesta.CodigoResultado == CODIGO_RESULTADO.Ok
				&& respuesta.DetalleResultado.Contains("Se ha actualizado la contraseña."));
		}

		#endregion Methods 

		#endregion Public Members 

		#region Non-Public Members (2) 

		#region Fields (1) 

		private TestContext _testContextInstance;

		#endregion Fields 
		#region Methods (1) 

		private string Encriptar(string pTexto)
		{
			return Encripcion.Encriptar(pTexto, ConfigurationManager.AppSettings.Get("VectorDeInicializacion"), ConfigurationManager.AppSettings.Get("ClaveDeEncripcion"));
		}

		#endregion Methods 

		#endregion Non-Public Members 
	}
}