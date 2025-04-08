using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
       
        Graph grafo = new Graph();

        
        grafo.AgregarCiudad("A");
        grafo.AgregarCiudad("B");
        grafo.AgregarCiudad("C");
        grafo.AgregarCiudad("D");
        grafo.AgregarCiudad("E");

      
        grafo.AgregarCarretera("A", "B", 5);
        grafo.AgregarCarretera("A", "C", 2);
        grafo.AgregarCarretera("B", "C", 1);
        grafo.AgregarCarretera("B", "D", 3);
        grafo.AgregarCarretera("C", "D", 4);
        grafo.AgregarCarretera("D", "E", 2);

        
        Console.WriteLine("Ejecutando el algoritmo de Dijkstra desde la ciudad A hasta la ciudad E:");
        grafo.Dijkstra("A", "E");

        Console.ReadLine();
    }
}

class Graph
{
    private Dictionary<string, Dictionary<string, int>> grafo;

    public Graph()
    {
        grafo = new Dictionary<string, Dictionary<string, int>>();
    }

   
    public void AgregarCiudad(string ciudad)
    {
        if (!grafo.ContainsKey(ciudad))
        {
            grafo[ciudad] = new Dictionary<string, int>();
        }
    }

   
    public void AgregarCarretera(string origen, string destino, int distancia)
    {
        
        grafo[origen][destino] = distancia;
        grafo[destino][origen] = distancia;
    }

    
    public void Dijkstra(string inicio, string fin)
    {
        
        if (!grafo.ContainsKey(inicio) || !grafo.ContainsKey(fin))
        {
            Console.WriteLine("Una o ambas ciudades no existen en el grafo.");
            return;
        }

        
        Dictionary<string, int> distancias = new Dictionary<string, int>();

        
        Dictionary<string, string> previos = new Dictionary<string, string>();

       
        HashSet<string> noVisitados = new HashSet<string>();

        
        foreach (var ciudad in grafo.Keys)
        {
            distancias[ciudad] = int.MaxValue;
            previos[ciudad] = null;
            noVisitados.Add(ciudad);
        }

        
        distancias[inicio] = 0;

        while (noVisitados.Count > 0)
        {
            
            string actual = null;
            int distanciaMinima = int.MaxValue;

            foreach (var ciudad in noVisitados)
            {
                if (distancias[ciudad] < distanciaMinima)
                {
                    distanciaMinima = distancias[ciudad];
                    actual = ciudad;
                }
            }

           
            if (actual == null || actual == fin)
                break;

            
            noVisitados.Remove(actual);

           
            foreach (var vecino in grafo[actual])
            {
                string ciudad = vecino.Key;
                int distancia = vecino.Value;

                int nuevaDistancia = distancias[actual] + distancia;

                
                if (nuevaDistancia < distancias[ciudad])
                {
                    distancias[ciudad] = nuevaDistancia;
                    previos[ciudad] = actual;
                }
            }
        }

        
        if (distancias[fin] == int.MaxValue)
        {
            Console.WriteLine($"No existe una ruta desde {inicio} hasta {fin}");
            return;
        }

        List<string> ruta = new List<string>();
        string nodoActual = fin;

        while (nodoActual != null)
        {
            ruta.Add(nodoActual);
            nodoActual = previos[nodoActual];
        }

        
        ruta.Reverse();

       
        Console.WriteLine($"La distancia más corta desde {inicio} hasta {fin} es: {distancias[fin]} km");
        Console.WriteLine($"La ruta es: {string.Join(" -> ", ruta)}");

        
        Console.WriteLine("\nDetalles de la ruta:");
        for (int i = 0; i < ruta.Count - 1; i++)
        {
            string ciudadActual = ruta[i];
            string siguienteCiudad = ruta[i + 1];
            int distanciaSegmento = grafo[ciudadActual][siguienteCiudad];

            Console.WriteLine($"{ciudadActual} -> {siguienteCiudad}: {distanciaSegmento} km");
        }
    }
}