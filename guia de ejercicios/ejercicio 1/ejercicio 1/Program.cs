using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
       
        string[] codigos = { "MZ123", "QK456", "PL789", "MN321", "QK654", "PL987", "MZ321" };

       
        TablaHash tabla = new TablaHash(7);

        
        Console.WriteLine("Valores hash de cada código:");
        foreach (var codigo in codigos)
        {
            int hashValue = tabla.CalcularHash(codigo);
            Console.WriteLine($"Hash('{codigo}') = {hashValue}");
        }

        
        Console.WriteLine("\nInsertando códigos en la tabla hash:");
        foreach (var codigo in codigos)
        {
            tabla.Insertar(codigo);
        }

        
        Console.WriteLine("\nTabla hash resultante (con resolución de colisiones por encadenamiento):");
        tabla.MostrarTabla();

        
        Console.WriteLine("\nDemostrando búsqueda de códigos:");
        foreach (var codigo in codigos)
        {
            bool encontrado = tabla.Buscar(codigo);
            Console.WriteLine($"Búsqueda de '{codigo}': {(encontrado ? "Encontrado" : "No encontrado")}");
        }

       
        string codigoInexistente = "XX999";
        bool encontradoInexistente = tabla.Buscar(codigoInexistente);
        Console.WriteLine($"Búsqueda de '{codigoInexistente}': {(encontradoInexistente ? "Encontrado" : "No encontrado")}");
    }
}

class TablaHash
{
    private List<string>[] tabla;
    private int tamaño;

    public TablaHash(int tamaño)
    {
        this.tamaño = tamaño;
        tabla = new List<string>[tamaño];

       
        for (int i = 0; i < tamaño; i++)
        {
            tabla[i] = new List<string>();
        }
    }

    
    public int CalcularHash(string codigo)
    {
        int suma = 0;
        foreach (char c in codigo)
        {
            suma += (int)c; 
        }
        return suma % tamaño; 
    }

    
    public void Insertar(string codigo)
    {
        int indice = CalcularHash(codigo);
        tabla[indice].Add(codigo);
        Console.WriteLine($"Insertado '{codigo}' en posición {indice}");
    }

   
    public bool Buscar(string codigo)
    {
        int indice = CalcularHash(codigo);
        return tabla[indice].Contains(codigo);
    }

    
    public void MostrarTabla()
    {
        for (int i = 0; i < tamaño; i++)
        {
            Console.Write($"Posición {i}: ");

            if (tabla[i].Count == 0)
            {
                Console.WriteLine("(vacía)");
            }
            else
            {
                
                for (int j = 0; j < tabla[i].Count; j++)
                {
                    Console.Write(tabla[i][j]);
                    if (j < tabla[i].Count - 1)
                    {
                        Console.Write(" -> ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}