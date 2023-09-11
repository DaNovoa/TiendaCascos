using System;
using System.Collections.Generic;

public class ViewCascos
{
    // Método para mostrar los detalles de un casco en la consola
    public void MostrarCasco(Cascos casco)
    {
        Console.WriteLine("Detalles del Casco:\n" + casco.ToString());
    }

    // Método para mostrar una lista de cascos en la consola
    public void MostrarCascos(List<Cascos> cascos)
    {
        if (cascos.Count == 0)
        {
            Console.WriteLine("No hay cascos para mostrar.");
            return;
        }

        Console.WriteLine("Lista de Cascos:");
        foreach (Cascos casco in cascos)
        {
            Console.WriteLine("------------");
            Console.WriteLine(casco.ToString());
        }
    }
}
