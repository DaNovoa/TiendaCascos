using System;
using MySql.Data.MySqlClient;

public class ControllerCascos
{
    private ICascosDao cascosDao;

    public ControllerCascos(ICascosDao cascosDao)
    {
        this.cascosDao = cascosDao;
    }

    public void Ejecutar()
    {
        string connectionString = "server=localhost;user=root;password=;database=cascosdb;";
        MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();

            while (true)
            {
                Console.WriteLine("\nMENU DE OPCIONES:");
                Console.WriteLine("1. Actualizar casco por cédula del comprador");
                Console.WriteLine("2. Eliminar casco por cédula del comprador");
                Console.WriteLine("3. Listar cascos");
                Console.WriteLine("4. Calcular total de ventas");
                Console.WriteLine("5. Ingresar nuevo casco");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");
                int opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Digite la cédula del comprador a actualizar: ");
                        string cedulaActualizar = Console.ReadLine();
                        cascosDao.ActualizarCascoPorCedula(cedulaActualizar);
                        break;
                    case 2:
                        Console.Write("Digite la cédula del comprador a eliminar: ");
                        string cedulaEliminar = Console.ReadLine();
                        cascosDao.EliminarCascoPorCedula(cedulaEliminar);
                        break;
                    case 3:
                        cascosDao.ListarCascos();
                        break;
                    case 4:
                        cascosDao.CalcularTotalVentas();
                        break;
                    case 5:
                        Console.WriteLine("Ingresar nuevo casco:");
                        Console.Write("Digite el nombre del comprador: ");
                        string nuevoComprador = Console.ReadLine();
                        Console.Write("Digite el apellido del comprador: ");
                        string nuevoApellido = Console.ReadLine();
                        Console.Write("Digite la cédula del comprador: ");
                        string nuevaCedula = Console.ReadLine();
                        Console.Write("Digite la cantidad de unidades: ");
                        int nuevasUnidades = Int32.Parse(Console.ReadLine());
                        Console.Write("Digite la talla del casco: ");
                        string nuevaTalla = Console.ReadLine();
                        Console.Write("Digite la marca del casco: ");
                        string nuevaMarca = Console.ReadLine();
                        Console.Write("Digite el precio del casco: ");
                        double nuevoPrecio = Double.Parse(Console.ReadLine());

                        cascosDao.IngresarNuevoCasco(nuevaTalla, nuevaMarca, nuevoPrecio, nuevoComprador, nuevoApellido, nuevaCedula, nuevasUnidades);
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del programa.");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Seleccione una opción válida del menú.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
    }
}
