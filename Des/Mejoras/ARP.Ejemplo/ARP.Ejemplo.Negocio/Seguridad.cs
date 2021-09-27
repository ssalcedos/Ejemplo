using System;
using ARP.Ejemplo.Comun.Entidades;
using ARP.Ejemplo.Comun.Excepciones;
using ARP.Ejemplo.Interfaces.Interfaces;
using ARP.Ejemplo.Negocio.Excepciones;

namespace ARP.Ejemplo.Negocio
{
    public class Seguridad : ISeguridad
    {


        #region Methods 

        // Public Methods 

        /// <summary>
        /// Metodo para Autenticar un usuario
        /// </summary>
        /// <param name="pAutenticacion">Objeto que contiene los datos necesario para la autenticación</param>
        /// <returns></returns>
        /// <remarks>
        /// Autor: Wendy Murillo - g619temp
        /// FechaDeCreacion: (20/06/2011)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: Wendy Murillo - g619temp
        /// Descripci n: Este metodo reliza un llamado al servicio de autenticacion expuesto
        /// por la aplicación AdminSeguridad y obtiene la respuesta a cerca de si la autenticacion
        /// es correcta para el usuario correspondiente.
        /// </remarks>
        public ResultadoAutenticacion AutenticarUsuario(Autenticacion pAutenticacion)
        {
            try
            {
                return Integracion.SeguridadAutenticacion.AutenticarUsuario(pAutenticacion);
            }
            catch (DatosException excepcionDatos)
            {
                throw ExcepcionesUtil.ManejarExcepcionDatos(excepcionDatos);
            }
            catch (NegocioException excepcionNegocio)
            {
                throw ExcepcionesUtil.ManejarExcepcionNegocio(excepcionNegocio);
            }
            catch (Exception exception)
            {
                throw ExcepcionesUtil.ManejarExcepcionAplicacion(exception);
            }
        }

        /// <summary>
        /// Metodo para obtener la url de cambio de contraseña
        /// </summary>
        /// <param name="pCambioContrase a">obejto con los datos necesarios para obtener la url
        /// de cambio de contraseña</param>
        /// <returns></returns>
        /// <remarks>
        /// Autor: Wendy Murillo - g619temp
        /// FechaDeCreacion: (20/06/2011)
        /// UltimaModificacionPor: (Nombre del Autor de la modificación - Usuario del dominio)
        /// FechaDeUltimaModificacion: (dd/MM/yyyy)
        /// EncargadoSoporte: Wendy Murillo - g619temp
        /// Descripci n: Este metodo se encarga de enviar la informacion naecesaria al servicios de seguridadusuarios
        /// expuesto por la aplicación de AdminSeguridad y que retorna la Url a la que el aplicativo debe redireccionar
        /// para que el usuario pueda realizar el cambio de contraseñas.
        /// </remarks>
        public ContrasenaCambio ObtenerUrl(ContrasenaCambio pCambioContrasena)
        {
            try
            {
                return Integracion.SeguridadUsuarios.ObtenerUrl(pCambioContrasena);
            }
            catch (DatosException excepcionDatos)
            {
                throw ExcepcionesUtil.ManejarExcepcionDatos(excepcionDatos);
            }
            catch (NegocioException excepcionNegocio)
            {
                throw ExcepcionesUtil.ManejarExcepcionNegocio(excepcionNegocio);
            }
            catch (Exception exception)
            {
                throw ExcepcionesUtil.ManejarExcepcionAplicacion(exception);
            }
        }

        /// <summary>
        /// Metodo para realizar el cambio de contraseña
        /// </summary>
        /// <param name="pCambioContraseña">Objeto con la información de cambio de contraseña</param>
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
        public Respuesta CambiarContrasena(ContrasenaCambio pCambioContrasena)
        {
            try
            {
                return Integracion.SeguridadUsuarios.CambiarContrasena(pCambioContrasena);
            }
            catch (DatosException excepcionDatos)
            {
                throw ExcepcionesUtil.ManejarExcepcionDatos(excepcionDatos);
            }
            catch (NegocioException excepcionNegocio)
            {
                throw ExcepcionesUtil.ManejarExcepcionNegocio(excepcionNegocio);
            }
            catch (Exception exception)
            {
                throw ExcepcionesUtil.ManejarExcepcionAplicacion(exception);
            }
        }

        /// <summary>
        /// Metodo que permite realizar la validacion de pregunta y 
        /// respuesta secreta para que el usuario pueda cambiar su contraseña en caso de olvido
        /// </summary>
        /// <Autor>Wendy Murillo - G619TEMP</Autor>
        /// <FechaDeCreacion>2011-Jun-13</FechaDeCreacion>
        /// <UltimaModificacionPor></UltimaModificacionPor>
        /// <FechaDeUltimaModificacion></FechaDeUltimaModificacion>
        /// <EncargadoSoporte>Wendy Murillo - G619TEMP</EncargadoSoporte>
        /// <param name="pCambioContrasena">Datos para la validacion</param>
        /// <exception cref="ARP.Aplicaciones.Servicios.Comun.Excepciones.ExcepcionDatos">Se lanza cuando hay algún error en la DAL</exception>        
        /// <exception cref="ARP.Aplicaciones.Servicios.Comun.Excepciones.ExcepcionNegocio">Se lanza cuando hay algún error en la lógica de negocio</exception>
        /// <exception cref="System.Exception">Excepción de aplicación no específica</exception>
        /// <remarks>Invoca el método de autenticacion que hace las validaciones respectivas y retorna un objeto 
        /// "Resultado" en el cual se especifica si la transaccion fue exitosa o no</remarks>
        /// <returns>Resultado de la autenticacion</returns>
        public ContrasenaCambio ValidarPreguntaRespuestaSecreta(ContrasenaCambio pCambioContrasena)
        {
            try
            {
                return Integracion.SeguridadUsuarios.ValidarPreguntaRespuestaSecreta(pCambioContrasena);
            }
            catch (DatosException excepcionDatos)
            {
                throw ExcepcionesUtil.ManejarExcepcionDatos(excepcionDatos);
            }
            catch (NegocioException excepcionNegocio)
            {
                throw ExcepcionesUtil.ManejarExcepcionNegocio(excepcionNegocio);
            }
            catch (Exception exception)
            {
                throw ExcepcionesUtil.ManejarExcepcionAplicacion(exception);
            }
        }

        #endregion Methods
    }
}