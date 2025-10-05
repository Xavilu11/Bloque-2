using System;

class Program
{
    static void Main(string[] args)
    {
        //Definición de nodos urbanos
        string[] nombres = { "Quitumbe", "La Marín", "San Rafael", "Conocoto" };
        GraphMatrix g = new GraphMatrix(nombres);

        //Conexiones entre nodos
        g.AddEdge("Quitumbe", "La Marín");
        g.AddEdge("Quitumbe", "San Rafael");
        g.AddEdge("La Marín", "Conocoto");
        g.AddEdge("San Rafael", "Conocoto");

        //Reportería: matriz de adyacencia
        Console.WriteLine("=== Matriz de adyacencia ===");
        g.PrintMatrix();

        //Reportería: conexiones por nodo
        Console.WriteLine("\n=== Conexiones por nodo ===");
        g.PrintConnections();
    }
}

class GraphMatrix
{
    private int[,] adjMatrix;
    private string[] nombres;
    private int size;

    public GraphMatrix(string[] nombres)
    {
        this.nombres = nombres;
        this.size = nombres.Length;
        adjMatrix = new int[size, size];
    }

    public void AddEdge(string origen, string destino)
    {
        int i = IndexOf(origen);
        int j = IndexOf(destino);
        if (i != -1 && j != -1)
        {
            adjMatrix[i, j] = 1;
        }
    }

    public void PrintMatrix()
    {
        Console.Write("       ");
        for (int i = 0; i < size; i++)
            Console.Write($"[{nombres[i],10}] ");
        Console.WriteLine();

        for (int i = 0; i < size; i++)
        {
            Console.Write($"[{nombres[i],10}] ");
            for (int j = 0; j < size; j++)
            {
                Console.Write($"     {adjMatrix[i, j]}     ");
            }
            Console.WriteLine();
        }
    }

    public void PrintConnections()
    {
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Nodo [{nombres[i]}] conecta con: ");
            bool tieneConexiones = false;
            for (int j = 0; j < size; j++)
            {
                if (adjMatrix[i, j] == 1)
                {
                    Console.Write($"[{nombres[j]}] ");
                    tieneConexiones = true;
                }
            }
            if (!tieneConexiones)
                Console.Write("ninguno");
            Console.WriteLine();
        }
    }

    private int IndexOf(string nombre)
    {
        for (int i = 0; i < size; i++)
            if (nombres[i] == nombre)
                return i;
        return -1;
    }
}