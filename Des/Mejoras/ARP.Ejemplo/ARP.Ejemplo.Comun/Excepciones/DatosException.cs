using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ARP.Ejemplo.Comun.Excepciones
{
    [Serializable]
    public class DatosException : System.Exception
    {
        #region Constructors (6)

        public DatosException(string pMessage, Exception pInnerException, string pCodigo)
            : base(pMessage, pInnerException)
        {
            this.Codigo = pCodigo;
        }

        public DatosException(string pMensaje, string pCodigo)
            : base(pMensaje)
        {
            this.Codigo = pCodigo;
        }

        protected DatosException(SerializationInfo pSerializationInfo, StreamingContext pStreamingContext)
            : base(pSerializationInfo, pStreamingContext)
        {
        }

        public DatosException(string pMensaje, Exception pInnerException)
            : base(pMensaje, pInnerException)
        {
        }

        public DatosException(string pMensaje)
            : base(pMensaje)
        {
        }

        public DatosException()
            : base()
        { }

        #endregion Constructors

        #region Properties (1)

        public string Codigo { get; private set; }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo pSerializationInfo, StreamingContext pStreamingContext)
        {
            base.GetObjectData(pSerializationInfo, pStreamingContext);
        }

        #endregion Methods
    }
}