using System;
using System.Globalization;

namespace ARP.Ejemplo.Comun.Utils
{
    public static class Conversiones
    {
        #region Methods (12)

        // Public Methods (12) 

        /// <summary>
        /// Convertir booleano en int
        /// </summary>
        /// <param name="pBooleano"></param>
        /// <returns>Regresa 0 si es false, 1 si es true</returns>
        public static int ConvertirBooleanoEnInt(bool pBooleano)
        {
            int respuesta = 0;
            respuesta = Convert.ToInt32(pBooleano);
            return respuesta;
        }

        public static string ConvertirBooleanoEnString(bool pBooleano)
        {
            return pBooleano ? "S" : "N";
        }

        /// <summary>
        /// Convertir Cadena En Booleano
        /// </summary>
        /// <param name="pCadena"></param>
        /// <returns>El valor de la cadena convertido o null si no se puede convertir</returns>
        public static bool? ConvertirCadenaEnBooleano(string pCadena)
        {
            bool cadenaConvertida = false;
            if (bool.TryParse(pCadena, out cadenaConvertida))
            {
                return cadenaConvertida;
            }
            return null;
        }

        /// <summary>
        /// Convertir Cadena En Decimal
        /// </summary>
        /// <param name="pCadena"></param>m
        /// <returns>El valor de la cadena convertido o cero si no se puede convertir</returns>
        public static Decimal ConvertirCadenaEnDecimal(string pCadena)
        {
            Decimal respuesta = 0;
            return Decimal.TryParse(pCadena, out respuesta) ? respuesta : default(Decimal);
        }

        public static double ConvertirCadenaEnDouble(string pDouble)
        {
            double respuesta = 0;
            return double.TryParse(pDouble, out respuesta) ? respuesta : default(double);
        }

        /// <summary>
        /// Convertir Cadena En Entero
        /// </summary>
        /// <param name="pCadena"></param>
        /// <returns>El valor de la cadena convertido o cero si no se puede convertir</returns>
        public static int ConvertirCadenaEnEntero(string pCadena)
        {
            int respuesta = 0;
            return int.TryParse(pCadena, out respuesta) ? respuesta : default(int);
        }

        /// <summary>
        /// Convertir Cadena En Fecha
        /// </summary>
        /// <param name="pCadena"></param>
        /// <returns>La fecha de la cadena convertido o el minvalue si no se puede convertir</returns>
        public static DateTime ConvertirCadenaEnFecha(string pCadena)
        {
            DateTime respuesta = new DateTime();
            return DateTime.TryParse(pCadena, out respuesta) ? respuesta : default(DateTime);
        }

        /// <summary>
        /// Convertir Cadena En Float
        /// </summary>
        /// <param name="pCadena"></param>m
        /// <returns>El valor de la cadena convertido o cero si no se puede convertir</returns>
        public static float ConvertirCadenaEnFloat(string pCadena)
        {
            float respuesta = 0;
            return float.TryParse(pCadena, out respuesta) ? respuesta : default(float);
        }

        /// <summary>
        /// Convertir Cadena En Long
        /// </summary>
        /// <param name="pCadena"></param>
        /// <returns>El valor de la cadena convertido o cero si no se puede convertir</returns>
        public static long ConvertirCadenaEnLong(string pCadena)
        {
            long respuesta = 0;
            return long.TryParse(pCadena, out respuesta) ? respuesta : default(long);
        }

        /// <summary>
        /// Convierte un entero en fecha
        /// </summary>
        /// <param name="pEntero">Valor con la fecha en formato aaaammdd</param>
        /// <returns>La fecha de la cadena convertido o el minvalue si no se puede convertir</returns>
        public static DateTime ConvertirCadenaNumericaEnFecha(string pCadena)
        {
            DateTime respuesta = new DateTime();
            try
            {
                int year = int.Parse(pCadena.Substring(0, 4), CultureInfo.CurrentCulture);
                int month = int.Parse(pCadena.Substring(4, 2), CultureInfo.CurrentCulture);
                int day = int.Parse(pCadena.Substring(6, 2), CultureInfo.CurrentCulture);

                respuesta = new DateTime(year, month, day);
            }
            catch
            {
                respuesta = DateTime.MinValue;
            }
            return respuesta;
        }

        /// <summary>
        /// Convertir Objeto En Entero
        /// </summary>
        /// <param name="pObjeto"> </param>
        /// <returns>El valor de la cadena convertido o cero si no se puede convertir</returns>
        public static int ConvertirObjetoEnEntero(object pObjeto)
        {
            if (pObjeto == null)
            {
                return 0;
            }
            return ConvertirCadenaEnEntero(pObjeto.ToString());
        }

        public static object ConvertirObjetoEnString(object pObject)
        {
            return pObject == null ? String.Empty : pObject.ToString();
        }

        #endregion Methods
    }
}