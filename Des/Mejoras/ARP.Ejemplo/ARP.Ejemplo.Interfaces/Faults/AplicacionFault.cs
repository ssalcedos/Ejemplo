using System.Runtime.Serialization;

namespace ARP.Ejemplo.Interfaces.Faults
{
    [DataContract(Namespace = "http://schemas.ARP.com/AutenticacionSeguridad/2010/12/Faults")]
    public class AplicacionFault
    {
        #region Fields (2)

        private const string CodigoExcepcionAplicacion = "999";
        private const string EstadoExcepcionNegocio = "ER-A";

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Constructor para excepciones de aplicación (código especificado en throw)
        /// </summary>
        public AplicacionFault(string pTipo, string pDescripcion)
        {
            this.Estado = EstadoExcepcionNegocio;
            this.Tipo = pTipo;
            this.Codigo = CodigoExcepcionAplicacion;
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