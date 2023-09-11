public interface ICascosDao
{
    void ActualizarCascoPorCedula(string cedulaCliente);
    void EliminarCascoPorCedula(string cedulaCliente);
    void CalcularTotalVentas();
    void ListarCascos();
    void IngresarNuevoCasco(string talla, string marca, double precio, string comprador, string apellido, string cedula, int unidades);
}
