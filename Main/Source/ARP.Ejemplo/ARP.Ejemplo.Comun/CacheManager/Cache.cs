using System;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace ARP.Ejemplo.Comun.CacheManager
{
    /// <summary>
    /// Tipos de cache para controlar el tiempo que se mantiene un objeto en memoria
    /// </summary>
    public enum CacheTipo
    {
        SinCache = 0,
        /// <summary>
        /// Este tipo de cache solo se usa para proposito de pruebas
        /// </summary>
        Cache1Minuto = 1,
        Cache10Minutos = 10,
        Cache1Hora = 60,
        Cache4Horas = 240,
        Cache1Dia = 1440,
        Cache1Semana = 10080
    }

    /// <summary>
    /// Clase encargada de manejar el cache del aplicativo
    /// </summary>
    public class Cache
    {
        #region Variables

        private ICacheManager _cache;
        private CacheTipo _tipoCache;

        #endregion Variables

        #region Constructores

        /// <summary>
        /// Crea una instancia de 1 hora por defecto
        /// </summary>
        public Cache()
        {
            _cache = CacheFactory.GetCacheManager("Cache1Hora");
            _tipoCache = CacheTipo.Cache1Hora;
            UsarTiempoAbsoluto = false;
        }

        /// <summary>
        /// Crear una instacia de la clase con el tiempo asignado
        /// </summary>
        /// <param name="pCacheTipo">Tipos: Cache10Minutos, Cache1Hora, Cache4Horas, Cache1Dia y Cache1Semana</param>
        public Cache(CacheTipo pCacheTipo)
        {
            _cache = CacheFactory.GetCacheManager(pCacheTipo.ToString());
            _tipoCache = pCacheTipo;
            UsarTiempoAbsoluto = false;
        }

        #endregion Constructores

        #region Propiedades

        public bool UsarTiempoAbsoluto { get; set; }

        #endregion Propiedades

        #region Metodos

        #region Metodos Publicos

        /// <summary>
        /// Borra la informacion existente relacionada con la llave dada y añade la nueva información
        /// para que no sea necesario refrescar el caché cada vez que se hace un reemplazo. Cuando
        /// se use este método también se debe refrescar el ítem en la base de dato.
        /// </summary>
        /// <typeparam name="T">Tipo de dato del objeto</typeparam>
        /// <param name="pLlave">Llave con la que se identifica el objeto en cache</param>
        /// <param name="pDato">objeto a actualizar</param>
        public void ActualizarObjeto<T>(T pDato, string pLlave)
        {
            _cache.Remove(pLlave);
            _cache.Add(pLlave, pDato, CacheItemPriority.Normal, new CacheRefreshAction(), GetCacheExpiration(_tipoCache));
        }

        /// <summary>
        /// Borra la informacion existente relacionada con el Key dado y añade la nueva información
        /// para que no sea necesario refrescar el caché cada vez que se hace un reemplazo. Cuando
        /// se use este método también se debe refrescar el ítem en la base de dato.
        /// </summary>
        /// <typeparam name="T">Tipo de dato del objeto</typeparam>
        /// <param name="pDato">objeto a almacenar</param>
        /// <param name="pNombreProcedimiento">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="pParametros">Parametros enviados al Procedimiento Almacenado</param>
        public void ActualizarObjeto<T>(T pDato, string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            ActualizarObjeto<T>(pDato, llave);
        }

        /// <summary>
        /// Almacena en cache la información del DbCommand con la llave Comando + Parametros
        /// </summary>
        /// <param name="Data">DbCommand a almacenar</param>
        /// <param name="Command">Nombre del Procedimiento Almacenado</param>
        /// d<param name="Parameters">Parametros enviandos al Procedimiento Alamacenado</param>
        public void AdicionarComando(DbCommand pDato, string pNombreBaseDatos, string pNombreComando, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreComando, pParametros) + "Comando";
            _cache.Add(llave, pDato, CacheItemPriority.Normal, new CacheRefreshAction(), GetCacheExpiration(_tipoCache));
        }

        /// <summary>
        /// Almacena en cache la información de un objeto
        /// </summary>
        /// <param name="pLlave">Llave con la cual se almacenará en el cache</param>
        /// <param name="pObjeto">Objeto del tipo T a almacenar </param>
        public void AdicionarObjeto<T>(T pDato, string pLlave)
        {
            _cache.Add(pLlave, pDato, CacheItemPriority.Normal, new CacheRefreshAction(), GetCacheExpiration(_tipoCache));
        }

        /// <summary>
        /// Almacena en cache la información del objeto con la llave Procedimiento + Parametros
        /// </summary>
        /// <typeparam name="T">Tipo de dato del objeto</typeparam>
        /// <param name="pDato">objeto a almacenar</param>
        /// <param name="pNombreProcedimiento">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="pParametros">Parametros enviados al Procedimiento Almacenado</param>
        public void AdicionarObjeto<T>(T pDato, string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            _cache.Add(llave, pDato, CacheItemPriority.Normal, new CacheRefreshAction(), GetCacheExpiration(_tipoCache));
        }

        /// <summary>
        /// Almacena en cache la información del DataTable con la llave Procedimiento + Parametros
        /// </summary>
        /// <param name="pDato">DataTable a almacenar</param>
        /// <param name="pNombreProcedimiento">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="pParametros">Parametros enviados al Procedimiento Almacenado</param>
        public void AdicionarTabla(DataTable pDato, string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            _cache.Add(llave, pDato, CacheItemPriority.Normal, new CacheRefreshAction(), GetCacheExpiration(_tipoCache));
        }

        /// <summary>
        /// Verifica si existe el objeto en el cache
        /// </summary>
        /// <param name="pLlave">Llave con la cual se almacenará en el cache</param>
        /// <returns>Verdadero si encuentra el objeto en el cache, de lo contrario falso</returns>
        public bool Existe(string pLlave)
        {
            object objeto = _cache.GetData(pLlave);
            return objeto != null;
        }

        /// <summary>
        /// Verifica si existe el objeto en el cache
        /// </summary>
        /// <param name="pNombreProcedimiento">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="pParametros">Parametros enviados al Procedimiento Almacenado</param>
        /// <returns>Verdadero si encuentra el objeto en el cache, de lo contrario falso</returns>
        public bool Existe(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            return Existe(llave);
        }

        /// <summary>
        /// Limpia todos los objetos almacenados en el cache
        /// </summary>
        public void Flush()
        {
            _cache.Flush();
        }

        /// <summary>
        /// Obtiene un DbCommand a partir de la llave Comando + Parametros
        /// </summary>
        /// <param name="pNombreComando">Nombre del Procedimiento Almacenado</param>
        /// <param name="pParametros">Parametros enviandos al Procedimiento Alamacenado</param>
        /// <returns>Comando de tipo DbCommand</returns>
        public DbCommand ObtenerComando(string pNombreBaseDatos, string pNombreComando, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreComando, pParametros) + "Comando";
            return (DbCommand)_cache.GetData(llave.ToString());
        }

        /// <summary>
        /// Obtiene un objeto con un tipo de dato generico que se ha almacenado previamente. En caso de no encontrar el onbeto devuelve default(T)
        /// </summary>
        /// <typeparam name="T">Tipo de dato del objeto</typeparam>
        /// <param name="pLlave">Llave con la cual se buscará en el cache</param>
        /// <returns>El objeto con tipo de dato definido por el usuario</returns>
        public T ObtenerObjeto<T>(string pLlave)
        {
            object objeto = _cache.GetData(pLlave);
            if (objeto == null)
            {
                return default(T);
            }
            return (T)objeto;
        }

        /// <summary>
        /// Obtiene un objeto con un tipo de dato generico consultando en la base de datos. En caso de no encontrar el onbeto devuelve default(T)
        /// </summary>
        /// <typeparam name="T">Tipo de dato del objeto</typeparam>
        /// <param name="pNombreProcedimiento">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="pParametros">Parametros enviados al Procedimiento Almacenado</param>
        /// <returns>El objeto con tipo de dato definido por el usuario</returns>
        public T ObtenerObjeto<T>(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            return ObtenerObjeto<T>(llave);
        }

        /// <summary>
        /// Obtiene un DataTable a partir de la llave Procedimiento + Parametros
        /// </summary>
        /// <param name="Procedimiento">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="Parametros">Parametros enviados al Procedimiento Almacenado</param>
        /// <returns>DataTable con la información almacenada en cache</returns>
        public DataTable ObtenerTabla(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            return (DataTable)_cache.GetData(llave);
        }

        /// <summary>
        /// Remueve de cache la información del DataTable con la llave Procedimiento + Parametros
        /// </summary>
        /// <param name="pLlave">Llave con la que se identifica el objeto en cache</param>
        public void Remover(string pLlave)
        {
            _cache.Remove(pLlave);
        }

        /// <summary>
        /// Remueve de cache la información del DataTable con la llave Procedimiento + Parametros
        /// </summary>
        /// <param name="ProcedureName">Nombre del Procedimiento Almacenado unido a la base de datos</param>
        /// <param name="Parameters">Parametros enviados al Procedimiento Almacenado</param>
        public void Remover(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            string llave = CrearLlave(pNombreBaseDatos, pNombreProcedimiento, pParametros);
            _cache.Remove(llave);
        }

        #endregion Metodos Publicos

        #region Metodos Privados

        /// <summary>
        /// Genera la llave unica para identificar el objeto almacenado en cache y luego recuperarlo
        /// </summary>
        /// <param name="Procedure">Nombre del procedimiento</param>
        /// <param name="Parameters">Listado de parametros</param>
        /// <returns>Retorna la llave en String</returns>
        private static string CrearLlave(string pNombreBaseDatos, string pNombreProcedimiento, params object[] pParametros)
        {
            StringBuilder llave = new StringBuilder();
            llave.Append(String.Format("{0}-{1}", pNombreBaseDatos, pNombreProcedimiento));
            ObtenerCadenaParametros(llave, pParametros);
            return llave.ToString();
        }

        private static AbsoluteTime GetAbsoluteTime(CacheTipo pCacheTipo)
        {
            return new AbsoluteTime(TimeSpan.FromMinutes((int)pCacheTipo));
        }

        private ICacheItemExpiration GetCacheExpiration(CacheTipo pTipoCache)
        {
            if (UsarTiempoAbsoluto)
            {
                return GetAbsoluteTime(pTipoCache);
            }
            return GetSlidingTime(pTipoCache);
        }

        /// <summary>
        /// Obtiene el slidingtime de acuerdo al tipo de cache definido en la enumeración. Default (1 hora)
        /// </summary>
        /// <param name="pCacheType">Tipo de cache</param>
        /// <returns> retorna un objeto SlidingTime</returns>
        private static SlidingTime GetSlidingTime(CacheTipo pCacheTipo)
        {
            return new SlidingTime(TimeSpan.FromMinutes((int)pCacheTipo));
        }

        private static void ObtenerCadenaParametros(StringBuilder pLlave, params object[] pParametros)
        {
            int columna = 0;
            foreach (object str in pParametros)
            {
                pLlave.Append(String.Format("col{0}={1}", columna, ValidarParametro(str)));
                columna++;
            }
        }

        /// <summary>
        /// Valida si una cadena es nula y retorna una cadena con un valor por defecto para evitar cadenas repetidas con parametros nulos, o el valor del parametro
        /// </summary>
        /// <param name="pObject">Parametro a evaluar</param>
        /// <returns>Devuelve una cadena con la información del objeto</returns>
        private static string ValidarParametro(object pObject)
        {
            return (pObject != null) ? pObject.ToString() : "#";
        }

        #endregion Metodos Privados

        #endregion Metodos
    }
}