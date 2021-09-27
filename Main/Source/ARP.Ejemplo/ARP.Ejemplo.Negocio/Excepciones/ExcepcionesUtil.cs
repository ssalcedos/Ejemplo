using System;
using System.ServiceModel;
using ARP.Ejemplo.Comun.Excepciones;
using ARP.Ejemplo.Interfaces.Faults;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ARP.Ejemplo.Negocio.Excepciones
{
    public static class ExcepcionesUtil
    {
        #region Methods (3)

        // Internal Methods (3) 

        internal static FaultException ManejarExcepcionAplicacion(Exception pExcepcion)
        {
            ExceptionPolicy.HandleException(pExcepcion, "PoliticaAplicacion");
            //// Error de aplicación no controlado
            AplicacionFault aplicacionEx = new AplicacionFault(pExcepcion.GetType().ToString(), pExcepcion.Message);
            return new FaultException<AplicacionFault>(aplicacionEx, new FaultReason(aplicacionEx.Descripcion), FaultCode.CreateReceiverFaultCode(new FaultCode(aplicacionEx.Codigo)));
        }

        internal static FaultException ManejarExcepcionDatos(DatosException pExcepcionDatos)
        {
            ExceptionPolicy.HandleException(pExcepcionDatos, "PoliticaDatos");
            DatosFault datosEx = new DatosFault(pExcepcionDatos.GetType().ToString(), pExcepcionDatos.Codigo, pExcepcionDatos.Message);
            return new FaultException<DatosFault>(datosEx, new FaultReason(datosEx.Descripcion), FaultCode.CreateReceiverFaultCode(new FaultCode(datosEx.Codigo)));
        }

        internal static FaultException ManejarExcepcionNegocio(NegocioException pExcepcionNegocio)
        {
            ExceptionPolicy.HandleException(pExcepcionNegocio, "PoliticaNegocio");
            NegocioFault negocioEx = new NegocioFault(pExcepcionNegocio.GetType().ToString(), pExcepcionNegocio.Codigo, pExcepcionNegocio.Message);
            return new FaultException<NegocioFault>(negocioEx, new FaultReason(negocioEx.Descripcion), FaultCode.CreateReceiverFaultCode(new FaultCode(negocioEx.Codigo)));
        }

        #endregion Methods
    }
}