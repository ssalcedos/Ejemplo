using System;
using System.Runtime.Serialization;

namespace ARP.Ejemplo.Comun.Entidades
{
    /// <summary>
    /// Manejo de los datos de autenticacion
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2011/02/Entidades/")]
    public class Autenticacion
    {
        #region Properties (2)

        /// <summary>
        /// Contrase�a del usuario
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Contrasena { get; set; }

        /// <summary>
        /// Login del usuario (Dominio\LoginUsuario)
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Login { get; set; }

        /// <summary>
        /// Indica si el usuario ingresa autenticado por el dominio (true)
        /// ó por la pantalla de login (false)
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool EsDominio { get; set; }

        #endregion Properties 
    }
}