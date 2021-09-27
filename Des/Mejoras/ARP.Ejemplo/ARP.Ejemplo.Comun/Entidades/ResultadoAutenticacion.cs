using System;
using System.Runtime.Serialization;

namespace ARP.Ejemplo.Comun.Entidades
{
    /// <summary>
    /// Objeto de respuesta de las transacciones
    /// </summary>
    [Serializable]
    [DataContract(Namespace = "http://schemas.ARP.Ejemplo.com/2011/02/Entidades/")]
    public class ResultadoAutenticacion
    {
        #region Properties (5)

        /// <summary>
        /// 1 si el usuario esta bloqueado, 0 si no
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool UsuarioBloqueado { get; set; }

        /// <summary>
        /// 1 si el usuario esta activo, 0 si no
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool UsuarioActivo { get; set; }

        /// <summary>
        /// 1 si la contraseña del usuario caduca, 0 si no
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool UsuarioContrasenaCaduca { get; set; }

        /// <summary>
        /// 1 si el usuario debe cambiar la contraseña, 0 si no
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool UsuarioObligarCambioContrasena { get; set; }

        /// <summary>
        /// 1 si el usuario confirmo su registro, 0 si no
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool UsuarioConfirmoRegistro { get; set; }

        /// <summary>
        /// 1 si el usuario existe, 0 si no
        /// </summary>
        [DataMember(IsRequired = false)]
        public bool UsuarioAutenticado { get; set; }

        /// <summary>
        /// Solo viene llena cuando la propiedad UsuarioObligarCambioContrasena esta en true
        /// </summary>
        [DataMember(IsRequired = false)]
        public string UrlCambioContrasena { get; set; }

        /// <summary>
        /// Respuesta de la operación
        /// </summary>
        [DataMember(IsRequired = false)]
        public Respuesta RespuestaOperacion { get; set; }

        /// <summary>
        /// Usuario externo autenticado
        /// </summary>
        [DataMember(IsRequired = false)]
        public UsuarioExterno Usuario { get; set; }

        #endregion Properties
    }
}