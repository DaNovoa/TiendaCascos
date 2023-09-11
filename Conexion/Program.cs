using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MySqlX.XDevAPI.Relational;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
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
                        ActualizarCascoPorCedula(cedulaActualizar, connection);
                        break;
                    case 2:
                        Console.Write("Digite la cédula del comprador a eliminar: ");
                        string cedulaEliminar = Console.ReadLine();
                        EliminarCascoPorCedula(cedulaEliminar, connection);
                        break;
                    case 3:
                        ListarCascos(connection);
                        break;
                    case 4:
                        CalcularTotalVentas(connection);
                        break;
                    case 5:
                        IngresarNuevoCasco(connection, out DateTime nuevaFecha);
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

    static void ActualizarCascoPorCedula(string cedulaCliente, MySqlConnection connection)
    {
        // Consulta para seleccionar cascos con la misma cédula del cliente
        string selectQuery = $"SELECT idcasco, talla, marca, comprador, apellido, cedula, precio, unidades FROM cascos WHERE cedula = '{cedulaCliente}'";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);

        using (MySqlDataReader reader = selectCommand.ExecuteReader())
        {
            List<int> ids = new List<int>();
            List<string> tallas = new List<string>();
            List<string> marcas = new List<string>();
            List<string> compradores = new List<string>();
            List<string> apellidos = new List<string>();
            List<string> cedulas = new List<string>();
            List<double> precios = new List<double>();
            List<int> unidades = new List<int>();

            while (reader.Read())
            {
                int idcasco = reader.GetInt32("idcasco");
                string talla = reader.GetString("talla");
                string marca = reader.GetString("marca");
                string comprador = reader.GetString("comprador");
                string apellido = reader.GetString("apellido");
                string cedula = reader.GetString("cedula");
                double precio = reader.GetDouble("precio");
                int unidad = reader.GetInt32("unidades");

                ids.Add(idcasco);
                tallas.Add(talla);
                marcas.Add(marca);
                compradores.Add(comprador);
                apellidos.Add(apellido);
                cedulas.Add(cedula);
                precios.Add(precio);
                unidades.Add(unidad);
            }

            reader.Close();

            if (tallas.Count == 0)
            {
                Console.WriteLine("No se encontraron cascos para la cédula proporcionada.");
                return;
            }

            Console.WriteLine("Cascos disponibles para el cliente:");
            for (int i = 0; i < tallas.Count; i++)
            {
                Console.WriteLine($"{ids[i]}. Talla: {tallas[i]}, Marca: {marcas[i]}, Comprador: {compradores[i]} {apellidos[i]}, Cédula: {cedulas[i]}, Precio: {precios[i]}, Unidades: {unidades[i]}");

            }

            Console.Write("Seleccione un casco para actualizar (ingrese el número de ID): ");
            int opcionCasco = Int32.Parse(Console.ReadLine());

            int index = ids.IndexOf(opcionCasco);

            if (index >= 0 && index < tallas.Count)
            {
                int idSeleccionado = ids[index];
                string tallaSeleccionada = tallas[index];
                string marcaSeleccionada = marcas[index];

                Console.Write("Nuevo Marca: ");
                string nuevaMarca = Console.ReadLine();
                Console.Write("Nuevo Precio: ");
                double nuevoPrecio = Double.Parse(Console.ReadLine());
                Console.Write("Nuevas Unidades: ");
                int nuevasUnidades = Int32.Parse(Console.ReadLine());

                string updateQuery = $"UPDATE cascos SET marca = '{nuevaMarca}', precio = {nuevoPrecio}, unidades = {nuevasUnidades} WHERE idcasco = {idSeleccionado}";
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);

                Console.WriteLine("Consulta SQL para la actualización:");
                Console.WriteLine(updateQuery);

                int rowsUpdated = updateCommand.ExecuteNonQuery();

                if (rowsUpdated > 0)
                {
                    Console.WriteLine("Casco actualizado exitosamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró un casco con la cédula proporcionado.");
                }
            }
            else
            {
                Console.WriteLine("id no válida.");
            }
        }
    }

    static void EliminarCascoPorCedula(string cedulaCliente, MySqlConnection connection)
    {
        // Consulta para seleccionar cascos con la misma cédula del cliente
        string selectQuery = $"SELECT idcasco, talla, marca, comprador, apellido, cedula, precio, unidades FROM cascos WHERE cedula = '{cedulaCliente}'";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);

        using (MySqlDataReader reader = selectCommand.ExecuteReader())
        {
            List<int> ids = new List<int>();
            List<string> tallas = new List<string>();
            List<string> marcas = new List<string>();
            List<string> compradores = new List<string>();
            List<string> apellidos = new List<string>();
            List<string> cedulas = new List<string>();
            List<double> precios = new List<double>();
            List<int> unidades = new List<int>();

            while (reader.Read())
            {
                int idcasco = reader.GetInt32("idcasco");
                string talla = reader.GetString("talla");
                string marca = reader.GetString("marca");
                string comprador = reader.GetString("comprador");
                string apellido = reader.GetString("apellido");
                string cedula = reader.GetString("cedula");
                double precio = reader.GetDouble("precio");
                int unidad = reader.GetInt32("unidades");

                ids.Add(idcasco);
                tallas.Add(talla);
                marcas.Add(marca);
                compradores.Add(comprador);
                apellidos.Add(apellido);
                cedulas.Add(cedula);
                precios.Add(precio);
                unidades.Add(unidad);
            }

            reader.Close();

            if (tallas.Count == 0)
            {
                Console.WriteLine("No se encontraron cascos para la cédula proporcionada.");
                return;
            }

            Console.WriteLine("Cascos disponibles para el cliente:");
            for (int i = 0; i < tallas.Count; i++)
            {
                Console.WriteLine($"{ids[i]}. Talla: {tallas[i]}, Marca: {marcas[i]}, Comprador: {compradores[i]} {apellidos[i]}, Cédula: {cedulas[i]}, Precio: {precios[i]}, Unidades: {unidades[i]}");
            }

            Console.Write("Seleccione un casco para eliminar (ingrese el número de ID): ");
            int opcionCasco = Int32.Parse(Console.ReadLine());

            int index = ids.IndexOf(opcionCasco);

            if (index >= 0 && index < tallas.Count)
            {
                int idSeleccionado = ids[index];

                // Ajustar la consulta para eliminar solo el casco seleccionado
                string deleteQuery = $"DELETE FROM cascos WHERE idcasco = {idSeleccionado}";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);

                int rowsDeleted = deleteCommand.ExecuteNonQuery();

                if (rowsDeleted > 0)
                {
                    Console.WriteLine("Casco eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró un casco con el id proporcionada.");
                }
            }
            else
            {
                Console.WriteLine("id no válida.");
            }
        }
    }

    static void CalcularTotalVentas(MySqlConnection connection)
    {
        string selectQuery = "SELECT precio, unidades FROM cascos";
        MySqlCommand command = new MySqlCommand(selectQuery, connection);

        using (MySqlDataReader reader = command.ExecuteReader())
        {
            double totalVentas = 0;
            while (reader.Read())
            {
                double precio = reader.GetDouble("precio");
                int unidades = reader.GetInt32("unidades");
                totalVentas += precio * unidades;
            }

            Console.WriteLine("El total de ventas es: " + totalVentas);
        }
    }

    static void ListarCascos(MySqlConnection connection)
    {
        string selectQuery = "SELECT idcasco, talla, marca, comprador, apellido, cedula, precio, unidades FROM cascos ORDER BY marca";
        MySqlCommand command = new MySqlCommand(selectQuery, connection);

        using (MySqlDataReader reader = command.ExecuteReader())
        {
            Console.WriteLine("Lista de cascos ordenados por marca:");
            while (reader.Read())
            {
                int idcasco = reader.GetInt32("idcasco");
                string talla = reader.GetString("talla");
                string marca = reader.GetString("marca");
                string comprador = reader.GetString("comprador");
                string apellido = reader.GetString("apellido");
                string cedula = reader.GetString("cedula");
                double precio = reader.GetDouble("precio");
                int unidad = reader.GetInt32("unidades");
                Console.WriteLine($"ID: {idcasco}, Talla: {talla}, Marca: {marca}, Comprador: {comprador} {apellido}, Cédula: {cedula}, Precio: {precio}, Unidades: {unidad}");
            }
        }
    }

    static void IngresarNuevoCasco(MySqlConnection connection, out DateTime nuevaFecha)
    {
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

        string fechaStr;

        nuevaFecha = DateTime.MinValue; // Inicializar nuevaFecha

        bool formatoFechaCorrecto = false;

        while (!formatoFechaCorrecto)
        {
            Console.Write("Ingrese la fecha del casco (formato yyyy-MM-dd): ");
            fechaStr = Console.ReadLine();
            Console.WriteLine("Valor ingresado: " + fechaStr);

            if (DateTime.TryParseExact(fechaStr, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out nuevaFecha))
            {
                formatoFechaCorrecto = true;
            }
            else
            {
                Console.WriteLine("Formato de fecha incorrecto. El formato debe ser 'yyyy-MM-dd'.");
            }

        }


        InsertarCasco(nuevaTalla, nuevaMarca, nuevoPrecio, nuevoComprador, nuevoApellido, nuevaCedula, nuevasUnidades, nuevaFecha, connection);
    }

    static void InsertarCasco(string talla, string marca, double precio, string comprador, string apellido, string cedula, int unidades, DateTime fecha, MySqlConnection connection)
    {
        string fechaFormateada = fecha.ToString("yyyy-MM-dd HH:mm:ss");
        string insertQuery = $"INSERT INTO cascos (talla, marca, precio, comprador, apellido, cedula, unidades, fecha) VALUES ('{talla}', '{marca}', {precio}, '{comprador}', '{apellido}', '{cedula}', {unidades}, '{fechaFormateada}')";

        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

        int rowsInserted = insertCommand.ExecuteNonQuery();

        if (rowsInserted > 0)
        {
            Console.WriteLine("Casco ingresado exitosamente.");
        }
        else
        {
            Console.WriteLine("Error al ingresar el casco.");
        }
    }
}
