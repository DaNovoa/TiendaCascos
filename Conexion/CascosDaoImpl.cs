using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class CascosDaoImpl : ICascosDao
{
    private MySqlConnection connection;

    public CascosDaoImpl(MySqlConnection connection)
    {
        this.connection = connection;
    }

    public void ActualizarCascoPorCedula(string cedulaCliente)
    {
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
                    Console.WriteLine("No se encontró un casco con la cédula proporcionada.");
                }
            }
            else
            {
                Console.WriteLine("ID no válida.");
            }
        }
    }

    public void EliminarCascoPorCedula(string cedulaCliente)
    {
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

                string deleteQuery = $"DELETE FROM cascos WHERE idcasco = {idSeleccionado}";
                MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);

                int rowsDeleted = deleteCommand.ExecuteNonQuery();

                if (rowsDeleted > 0)
                {
                    Console.WriteLine("Casco eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró un casco con el ID proporcionado.");
                }
            }
            else
            {
                Console.WriteLine("ID no válida.");
            }
        }
    }

    public void CalcularTotalVentas()
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

    public void ListarCascos()
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

    public void IngresarNuevoCasco(string nuevaTalla, string nuevaMarca, double nuevoPrecio, string nuevoComprador, string nuevoApellido, string nuevaCedula, int nuevasUnidades)
    {
        string insertQuery = $"INSERT INTO cascos (talla, marca, precio, comprador, apellido, cedula, unidades) VALUES ('{nuevaTalla}', '{nuevaMarca}', {nuevoPrecio}, '{nuevoComprador}', '{nuevoApellido}', '{nuevaCedula}', {nuevasUnidades})";

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
