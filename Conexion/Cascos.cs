public class Cascos
{
    public int IdCasco { get; set; }
    public string Talla { get; set; }
    public string Marca { get; set; }
    public string Comprador { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public double Precio { get; set; }
    public int Unidades { get; set; }

    public Cascos(int idCasco, string talla, string marca, string comprador, string apellido, string cedula, double precio, int unidades)
    {
        IdCasco = idCasco;
        Talla = talla;
        Marca = marca;
        Comprador = comprador;
        Apellido = apellido;
        Cedula = cedula;
        Precio = precio;
        Unidades = unidades;
    }

    public override string ToString()
    {
        return $"ID: {IdCasco}, Talla: {Talla}, Marca: {Marca}, Comprador: {Comprador} {Apellido}, Cédula: {Cedula}, Precio: {Precio}, Unidades: {Unidades}";
    }
}
