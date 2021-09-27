using System;
using System.Runtime.Serialization;

namespace ARP.Ejemplo.Comun.Entidades
{
    /// <summary>
    /// Objeto de respuesta de las transacciones
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2011/02/Entidades/")]
    public class Respuesta
    {
		#region�Data�Members�(3)�

		//�Properties�(3)�

        /// <summary>
        /// Identificador de la respuesta, si aplica
        /// </summary>
        [DataMember(IsRequired = false)]
        public int CodigoObtenido { get; set; }

        /// <summary>
        /// C�digo del resultado de la invocaci�n al metodo
        /// </summary>
        [DataMember(IsRequired = true)]
        public CODIGO_RESULTADO CodigoResultado { get; set; }

        /// <summary>
        /// Descripci�n del resultado de la operaci�n
        /// </summary>
        [DataMember(IsRequired = false)]
        public string DetalleResultado { get; set; }

		#endregion�Data�Members�
    }
}