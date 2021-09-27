using System;
using System.Runtime.Serialization;

namespace ARP.Ejemplo.Comun.Entidades
{
    /// <summary>
    /// Objeto que contiene los datos del usuario externo autenticado en el sistema de seguridad
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2011/02/Entidades/")]
    public class UsuarioExterno
    {
		#region Data Members (5) 

		// Properties (5) 

        /// <summary>
        /// Identificador del usuario en el aplicativo de seguridad
        /// </summary>
        [DataMember(IsRequired = false)]
        public int Id { get; set; }

        /// <summary>
        /// Primer apellido del usuario
        /// </summary>
        [DataMember(IsRequired = true)]
        public string PrimerApellido { get; set; }

        /// <summary>
        /// Primer Nombre del usuario
        /// </summary>
        [DataMember(IsRequired = true)]
        public string PrimerNombre { get; set; }

        /// <summary>
        /// Segundo apellido del usuario
        /// </summary>
        [DataMember(IsRequired = false)]
        public string SegundoApellido { get; set; }

        /// <summary>
        /// Segundo nombre del usuario
        /// </summary>
        [DataMember(IsRequired = false)]
        public string SegundoNombre { get; set; }

        /// <summary>
        /// Nombre Completo del usuario
        /// </summary>
        [DataMember(IsRequired = false)]
        public string NombreCompleto { get; set; }

        /// <summary>
        /// Cargo del usuario 
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Cargo { get; set; }

		#endregion Data Members 
    }
}