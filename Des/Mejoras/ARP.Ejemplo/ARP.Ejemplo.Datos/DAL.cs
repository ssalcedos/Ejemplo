using System;
using System.Data;
using System.Data.Common;
using System.Text;
using ARP.Ejemplo.Comun.CacheManager;
using ARP.Ejemplo.Comun.Excepciones;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ARP.Ejemplo.Datos
{
    /// <summary>
    /// Clase encargada de controlar el acceso a datos para el aplicativo
    /// comentario
    /// </summary>
    public static class DAL
    {
        #region Methods (4)

        // Public Methods (4) 

        /// <summary>
        /// Ejecuta un procedimiento almacenado en la base de datos
        /// </summary>
        /// <param name="pNombreProcedimiento">Lista compuesta por el nombre de la conexion a la Base de datos y Nombre del procedimiento almacenado. Separado por ;</param>
        /// <param name="pParametros">Parametros que se deben enviar al Procedimiento Almacenado</param>
        /// <returns>Devuelve verdadero si se ejecuto el procedimiento y si se ingresaron datos, o falso si no se afecto la base de datos</returns>
        public static bool EjecutarQuery(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            try
            {
                Database dataBase;
                DbCommand dbCommand;
                CargarConexion(pNombreBaseDatos, pNombreProcedimiento, out dataBase, out dbCommand, pParametros);
                //Ejecutar el procedimiento almacenado
                return dataBase.ExecuteNonQuery(dbCommand) > 0;
            }
            catch (Exception exc)
            {
                //Escalar el throw
                string mensajeExcepcion = ObtenerMensajeExcepcion(pNombreBaseDatos, pNombreProcedimiento, exc, pParametros);
                throw new DatosException(mensajeExcepcion, Config.CodigosError.Default.DAL_EjecutarQuery);
            }
        }

        /// <summary>
        /// Retorna un dato tipo object
        /// </summary>
        /// <param name="pNombreProcedimiento">Lista compuesta por el nombre de la conexion a la Base de datos y Nombre del procedimiento almacenado. Separado por ;</param>
        /// <param name="pParametros">Parametros que se deben enviar al Procedimiento Almacenado</param>
        /// <returns>Devuelve un valor scalar de la consulta, el tipo de dato es object</returns>
        public static object ObtenerObjeto(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            try
            {
                Database dataBase;
                DbCommand dbCommand;
                CargarConexion(pNombreBaseDatos, pNombreProcedimiento, out dataBase, out dbCommand, pParametros);
                //Ejecutar el procedimiento almacenado
                return dataBase.ExecuteScalar(dbCommand);
            }
            catch (Exception exc)
            {
                //Escalar el throw
                string mensajeExcepcion = ObtenerMensajeExcepcion(pNombreBaseDatos, pNombreProcedimiento, exc, pParametros);
                throw new DatosException(mensajeExcepcion, Config.CodigosError.Default.DAL_ObtenerObjeto);
            }
        }

        /// <summary>
        /// Retorna el objeto de tipo T. En caso de no obtener nada de la base de datos retorna el default(T)
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="pNombreProcedimiento">Lista compuesta por el nombre de la conexion a la Base de datos y Nombre del procedimiento almacenado. Separado por ;</param>
        /// <param name="pParametros">Parametros que se deben enviar al Procedimiento Almacenado</param>
        /// <returns>Devuelve el valor del objeto tipado</returns>
        public static T ObtenerObjeto<T>(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            object resultado = ObtenerObjeto(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            try
            {
                return (resultado == null) ? default(T) : (T)resultado;
            }
            catch (Exception exc)
            {
                //Escalar el throw
                string mensajeExcepcion = String.Format("Error convirtiendo el objeto a un tipo de dato {0}. Procedimiento: {1} EXC: {2}", typeof(T), pNombreProcedimiento, exc.ToString());
                throw new DatosException(mensajeExcepcion, Config.CodigosError.Default.DAL_ObtenerObjetoT);
            }
        }

        /// <summary>
        /// Obtiene un DataTable de la Base de Datos que se defina.
        /// Los parametros deben enviarse en el mismo orden en que estan definidos en el Procedimiento Almacenado (Verificar tipos de datos)
        /// </summary>
        /// <param name="pNombreProcedimiento">Lista compuesta por el nombre de la conexion a la Base de datos y Nombre del procedimiento almacenado. Separado por ;</param>
        /// <param name="pParametros">Parametros que se deben enviar al Procedimiento Almacenado</param>
        /// <returns>DataTable con la información de la Base de Datos</returns>
        public static DataTable ObtenerTabla(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            try
            {
                Database dataBase;
                DbCommand dbCommand;
                CargarConexion(pNombreBaseDatos, pNombreProcedimiento, out dataBase, out dbCommand, pParametros);
                //Ejecutar el procedimiento almacenado
                return dataBase.ExecuteDataSet(dbCommand).Tables[0];
            }
            catch (Exception exc)
            {
                //Escalar el throw
                string mensajeExcepcion = ObtenerMensajeExcepcion(pNombreBaseDatos, pNombreProcedimiento, exc, pParametros);
                throw new DatosException(mensajeExcepcion, Config.CodigosError.Default.DAL_ObtenerTabla);
            }
        }

        #endregion Methods

        #region Metodos privados 

        private static void CargarConexion(string pNombreBaseDatos, string pNombreProcedimiento, out Database pDatabase, out DbCommand pDbCommand, params object[] pParametros)
        {
            pDatabase = DatabaseFactory.CreateDatabase(pNombreBaseDatos);
            pDbCommand = ObtenerComandoVerificarCache(pNombreBaseDatos, pNombreProcedimiento, pDatabase, pNombreProcedimiento, pParametros);
            pDbCommand.CommandTimeout = Config.Datos.Default.DefaultCommandTimeout;
        }

        private static DbCommand ObtenerComandoVerificarCache(string pNombreBaseDatos, string pNombreProcedimiento, Database pDatabase, string pNombreProcedimientoAlmacenado, params object[] pParametros)
        {
            Cache cache = new Cache(CacheTipo.Cache1Dia);
            DbCommand pDbCommand = cache.ObtenerComando(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            if (pDbCommand == null)
            {
                //Enviar los parametros al procedimiento almacenado (Discover Parameters)
                pDbCommand = pDatabase.GetStoredProcCommand(pNombreProcedimientoAlmacenado, pParametros);
                //Almacenar el Command en el cache para evitar que la proxima consulta realice el Discover Parameters
                cache.AdicionarComando(pDbCommand, pNombreBaseDatos, pNombreProcedimiento, pParametros);
            }
            return pDbCommand;
        }

        private static string ObtenerMensajeExcepcion(string pNombreBaseDatos, string pNombreProcedimiento, Exception pException, params object[] pParametros)
        {
            return String.Format("Error consultando la base de datos. CONEXION: {0}\nMENSAJE\n{1}\nPROCEDIMIENTO\n{2}\nEXCEPCION\n{3}", pNombreBaseDatos, pException.Message, ObtenerDatosProcedimiento(pNombreProcedimiento, pParametros), pException.ToString());
        }

        private static string ObtenerDatosProcedimiento(string pNombreProcedimiento, params object[] pParametros)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(String.Format("Nombre: {0}\n", pNombreProcedimiento));
            int posicion = 0;
            foreach (object parametro in pParametros)
            {
                string valor = "null";
                if (parametro != null)
                {
                    valor = parametro.ToString();
                }

                stringBuilder.Append(String.Format("Parametro[{0}]: {1}\n", posicion, valor));
                posicion++;
            }

            return stringBuilder.ToString();
        }

        #endregion
    }
}