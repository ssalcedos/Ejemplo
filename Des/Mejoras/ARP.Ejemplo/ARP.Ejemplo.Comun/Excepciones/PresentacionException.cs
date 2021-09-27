using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ARP.Ejemplo.Comun.Excepciones
{
    [Serializable]
    public class PresentacionException : System.Exception
    {
        #region Constructors (6)

        public PresentacionException(string pMessage, Exception pInnerException, string pCodigo)
            : base(pMessage, pInnerException)
        {
            this.Codigo = pCodigo;
        }

        public PresentacionException(string pMensaje, string pCodigo)
            : base(pMensaje)
        {
            this.Codigo = pCodigo;
        }

        protected PresentacionException(SerializationInfo pSerializationInfo, StreamingContext pStreamingContext)
            : base(pSerializationInfo, pStreamingContext)
        {
        }

        public PresentacionException(string pMensaje, Exception pInnerException)
            : base(pMensaje, pInnerException)
        {
        }

        public PresentacionException(string pMensaje)
            : base(pMensaje)
        {
        }

        public PresentacionException()
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