using System;
using System.Runtime.Serialization;

namespace ARP.Ejemplo.Comun.Entidades
{
    /// <summary>
    /// Objeto de respuesta de las transacciones
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2011/02/Entidades/")]
    public class ContrasenaCambio
    {
		#region Data Members (8) 

		// Properties (8) 

        /// <summary>
        /// Contraseña anterios
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ContrasenaAnterior { get; set; }

        /// <summary>
        /// Contraseña nueva
        /// </summary>
        [DataMember(IsRequired = true)]
        public string ContrasenaNueva { get; set; }

        /// <summary>
        /// Id de usuario al que se la va a cambiar la contraseña
        /// </summary>
        [DataMember(IsRequired = true)]
        public int IdUsuario { get; set; }

        /// <summary>
        /// Login de usuario para el caso de recordar contraseña
        /// </summary>
        [DataMember(IsRequired = true)]
        public string LoginUsuario { get; set; }

             /// <summary>
        /// Pregunta Secreta
        /// </summary>
        [DataMember(IsRequired = true)]
        public string PreguntaSecreta { get; set; }

             /// <summary>
        /// Respuesta Secreta
        /// </summary>
        [DataMember(IsRequired = true)]
        public string RespuestaSecreta { get; set; }

        /// <summary>
        /// Tipo de usuario si es interno o externo
        /// </summary>
        [DataMember(IsRequired = true)]
        public TIPO_USUARIO TipoUsuario { get; set; }

        /// <summary>
        /// Url para redireccionar el cambio de contraseña
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Sólo se requiere la url no una uri"), DataMember(IsRequired = true)]
        public string UrlObtenida { get; set; }

		#endregion Data Members 
    }
}