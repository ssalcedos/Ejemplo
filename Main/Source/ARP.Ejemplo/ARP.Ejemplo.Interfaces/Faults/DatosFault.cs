using System.Runtime.Serialization;

namespace ARP.Ejemplo.Interfaces.Faults
{
    [DataContract(Namespace = "http://schemas.ARP.com/AutenticacionSeguridad/2010/12/Faults")]
    public class DatosFault
    {
        #region Fields (1)

        private const string EstadoExcepcionNegocio = "ER-D";

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Constructor para excepciones de negocio (código especificado en throw)
        /// </summary>
        public DatosFault(string pTipo, string pCodigo, string pDescripcion)
        {
            this.Estado = EstadoExcepcionNegocio;
            this.Tipo = pTipo;
            this.Codigo = pCodigo;
            this.Descripcion = pDescripcion;
        }

        #endregion Constructors

        #region Properties (4)

        [DataMember(IsRequired = true)]
        public string Codigo { get; set; }

        [DataMember(IsRequired = true)]
        public string Descripcion { get; set; }

        [DataMember(IsRequired = true)]
        public string Estado { get; set; }

        [DataMember(IsRequired = true)]
        public string Tipo { get; set; }

        #endregion Properties
    }
}