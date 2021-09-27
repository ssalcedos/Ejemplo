using System.Runtime.Serialization;

namespace ARP.Ejemplo.Comun.Entidades
{
    /// <summary>
    /// Enumeración para el tipo de respuesta de la ejecución
    /// </summary>
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2010/10/Enumeraciones/")]
    public enum CODIGO_RESULTADO
    {
        NoEspecificado = 0,
        [EnumMember()]
        Ok,
        [EnumMember()]
        Error
    }

    /// <summary>
    /// Enumeración para el tipo de Internet
    /// </summary>
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2010/10/Enumeraciones/")]
    public enum TIPO_USUARIO
    {
        NoEspecificado = 0,
        [EnumMember()]
        Interno,
        [EnumMember()]
        Externo
    }
}