namespace ARP.Ejemplo.Negocio.Excepciones
{
    internal static class ExceptionManager
    {
        //internal static FaultException ManejarExcepcionDatos(DatosException pDatosException)
        //{
        //    ExceptionPolicy.HandleException(pDatosException, "PoliticaDatos");
        //    DatosFault datosEx = new DatosFault(pDatosException.GetType().ToString(), pDatosException.Codigo, pDatosException.Message);
        //    return new FaultException<DatosFault>(datosEx, new FaultReason(datosEx.Descripcion), FaultCode.CreateReceiverFaultCode(new FaultCode(datosEx.Codigo)));
        //}
        //internal static FaultException ManejarExcepcionNegocio(NegocioException pNegocioException)
        //{
        //    ExceptionPolicy.HandleException(pNegocioException, "PoliticaNegocio");
        //    NegocioFault negocioEx = new NegocioFault(pNegocioException.GetType().ToString(), pNegocioException.Codigo, pNegocioException.Message);
        //    return new FaultException<NegocioFault>(negocioEx, new FaultReason(negocioEx.Descripcion), FaultCode.CreateReceiverFaultCode(new FaultCode(negocioEx.Codigo)));
        //}
        //internal static FaultException ManejarExcepcionAplicacion(Exception pExcepcion)
        //{
        //    ExceptionPolicy.HandleException(pExcepcion, "PoliticaAplicacion");
        //    // Error de aplicación no controlado
        //    AplicacionFault aplicacionEx = new AplicacionFault(pExcepcion.GetType().ToString(), pExcepcion.Message);
        //    return new FaultException<AplicacionFault>(aplicacionEx, new FaultReason(aplicacionEx.Descripcion), FaultCode.CreateReceiverFaultCode(new FaultCode(aplicacionEx.Codigo)));
        //}
    }
}