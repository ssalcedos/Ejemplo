using ARP.Ejemplo.Comun.Entidades;
using ARP.Ejemplo.Negocio;
using BCSC.Comun.Encripcion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace ARP.Ejemplo.Pruebas
{
    [TestClass]
    public class PruebaAutenticacion
    {
		#region Data Members (2) 

        private TestContext _testContextInstance;
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

		#endregion Data Members 

		#region Initialization (1) 

        public PruebaAutenticacion() { }

		#endregion Initialization 

		#region Tests (11) 

        /// <summary>
        /// Autenticación exitosa
        /// </summary>
        [TestMethod]
        public void AutenticacionExitosa()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\g221temp"),
                Contrasena = Encriptar("Carlos123")
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Ok
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.AutenticacionExitosa
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario != null
                && resultadoAutenticacion.Usuario.Id != 0);
        }

        /// <summary>
        /// Clave errada
        /// </summary>
        [TestMethod]
        public void ClaveErrada()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\muchosintentos"),
                Contrasena = Encriptar("abc")
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.ClaveErrada
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario == null);
        }

        /// <summary>
        /// El usuario debe cambiar la contrasena en el siguiente inicio de sesion
        /// </summary>
        [TestMethod]
        public void ContrasenaCaduco()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\contraseñacaduco"),
                Contrasena = Encriptar("123")
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.ContrasenaCaduco
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == true
                && resultadoAutenticacion.UsuarioBloqueado == false);
        }

        /// <summary>
        /// Envío de datos nulos
        /// </summary>
        [TestMethod]
        public void DatosEnBlanco()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = string.Empty,
                Contrasena = string.Empty
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.DatosEnBlanco
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario == null);
        }

        /// <summary>
        /// Envío de datos nulos
        /// </summary>
        [TestMethod]
        public void DatosEnNull()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = null,
                Contrasena = null
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.DatosEnNull
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario == null);
        }

        /// <summary>
        /// Envío de datos sin encriptar
        /// </summary>
        [TestMethod]
        public void DatosSinEncriptar()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = @"seguridad\g221temp",
                Contrasena = "123"
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.DatosSinEncriptar //Mensaje de la primera validacion que realiza el servicio
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario == null);
        }

        /// <summary>
        /// El usuario excedio el numero de intentos perimitidos.
        /// </summary>
        [TestMethod]
        public void ExcedeNumeroIntentos()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\muchosintentos"),
                Contrasena = Encriptar("123")
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.ExcedeNumeroIntentos
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == true);
        }

        /// <summary>
        /// El servicio no esta disponible
        /// </summary>
        [TestMethod]
        public void ServicioNoDisponible()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\g221temp"),
                Contrasena = Encriptar("123")
            };
            try
            {
                ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(Mensaje.ServicioNoDisponible));
            }

        }

        /// <summary>
        /// El usuario está bloqueado
        /// </summary>
        [TestMethod]
        public void UsuarioBloqueado()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\usuariobloqueado"),
                Contrasena = Encriptar("123")
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.UsuarioBloqueado
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == true);
        }

        /// <summary>
        /// Usuario no existe
        /// </summary>
        [TestMethod]
        public void UsuarioNoExiste()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = Encriptar(@"seguridad\Login"),
                Contrasena = Encriptar("abc123")
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.UsuarioNoExiste
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario == null);
        }

        /// <summary>
        /// Envío de nombre de usuario sin el dominio
        /// </summary>
        [TestMethod]
        public void UsuarioSinDominio()
        {
            Seguridad seguridad = new Seguridad();
            Autenticacion autenticacion = new Autenticacion()
            {
                Login = "g221temp",
                Contrasena = "123"
            };
            ResultadoAutenticacion resultadoAutenticacion = seguridad.AutenticarUsuario(autenticacion);
            Assert.IsTrue(resultadoAutenticacion.RespuestaOperacion.CodigoResultado == CODIGO_RESULTADO.Error
                && resultadoAutenticacion.RespuestaOperacion.DetalleResultado == Mensaje.UsuarioSinDominio
                && resultadoAutenticacion.UsuarioObligarCambioContrasena == false
                && resultadoAutenticacion.UsuarioBloqueado == false
                && resultadoAutenticacion.Usuario == null);
        }

		#endregion Tests 

		#region Helper Methods (1) 

        private string Encriptar(string pTexto)
        {
            return Encripcion.Encriptar(pTexto, ConfigurationManager.AppSettings.Get("VectorDeInicializacion"), ConfigurationManager.AppSettings.Get("ClaveDeEncripcion"));
        }

		#endregion Helper Methods 
    }
}