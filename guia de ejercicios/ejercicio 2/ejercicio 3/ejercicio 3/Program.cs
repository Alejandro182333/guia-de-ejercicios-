using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        PriorityQueue<Paciente> colaPrioridad = new PriorityQueue<Paciente>();

       
        colaPrioridad.Encolar(new Paciente("Ana", 3));
        colaPrioridad.Encolar(new Paciente("Luis", 1));
        colaPrioridad.Encolar(new Paciente("Sofía", 2));
        colaPrioridad.Encolar(new Paciente("Marta", 1));
        colaPrioridad.Encolar(new Paciente("Pedro", 2));

        Console.WriteLine("Lista de pacientes (orden de llegada):");
        Console.WriteLine("1. Ana (Prioridad 3)");
        Console.WriteLine("2. Luis (Prioridad 1)");
        Console.WriteLine("3. Sofía (Prioridad 2)");
        Console.WriteLine("4. Marta (Prioridad 1)");
        Console.WriteLine("5. Pedro (Prioridad 2)");

        Console.WriteLine("\nOrden de atención según cola de prioridad mínima:");

        int orden = 1;
        
        while (!colaPrioridad.EstaVacia())
        {
            Paciente paciente = colaPrioridad.Desencolar();
            Console.WriteLine($"{orden}. {paciente.Nombre} (Prioridad {paciente.Prioridad})");
            orden++;
        }

        Console.ReadLine();
    }
}

class Paciente : IComparable<Paciente>
{
    public string Nombre { get; private set; }
    public int Prioridad { get; private set; }

    public Paciente(string nombre, int prioridad)
    {
        Nombre = nombre;
        Prioridad = prioridad;
    }

   
    public int CompareTo(Paciente otro)
    {
        
        return this.Prioridad.CompareTo(otro.Prioridad);
    }
}


class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap;

    public PriorityQueue()
    {
        heap = new List<T>();
    }

    public bool EstaVacia()
    {
        return heap.Count == 0;
    }

    public int Tamaño()
    {
        return heap.Count;
    }

    public void Encolar(T item)
    {
        
        heap.Add(item);

       
        HeapifyUp(heap.Count - 1);
    }

    public T Desencolar()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La cola de prioridad está vacía");
        }

       
        T resultado = heap[0];

        
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        
        if (heap.Count > 0)
        {
            HeapifyDown(0);
        }

        return resultado;
    }

   
    private void HeapifyUp(int index)
    {
      
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;

            
            if (heap[index].CompareTo(heap[parentIndex]) >= 0)
            {
                break;
            }

            
            Swap(index, parentIndex);

            
            index = parentIndex;
        }
    }

   
    private void HeapifyDown(int index)
    {
        int size = heap.Count;

        while (true)
        {
           
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int smallest = index;

            
            if (leftChild < size && heap[leftChild].CompareTo(heap[smallest]) < 0)
            {
                smallest = leftChild;
            }

          
            if (rightChild < size && heap[rightChild].CompareTo(heap[smallest]) < 0)
            {
                smallest = rightChild;
            }

            
            if (smallest == index)
            {
                break;
            }

            
            Swap(index, smallest);

          
            index = smallest;
        }
    }

    
    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
