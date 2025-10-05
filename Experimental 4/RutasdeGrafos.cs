using System;

class Programa
{
    static void Main()
    {
        //Inicio del programa.
        int maxNodos = 10;
        int maxConexiones = 5;
        Nodo[] grafo = new Nodo[maxNodos];
        int totalNodos = 0;

        //Se crean los nodos Quitumbe, La Marin, San Rafael y Conocoto
        int quitumbe = totalNodos;
        grafo[totalNodos++] = new Nodo("Quitumbe", maxConexiones);

        int laMarin = totalNodos;
        grafo[totalNodos++] = new Nodo("La Marín", maxConexiones);

        int sanRafael = totalNodos;
        grafo[totalNodos++] = new Nodo("San Rafael", maxConexiones);

        int conocoto = totalNodos;
        grafo[totalNodos++] = new Nodo("Conocoto", maxConexiones);

        //Conexiones directas
        grafo[quitumbe].Conexiones[0] = laMarin;
        grafo[quitumbe].Pesos[0] = 15;

        grafo[quitumbe].Conexiones[1] = sanRafael;
        grafo[quitumbe].Pesos[1] = 25;

        grafo[laMarin].Conexiones[0] = conocoto;
        grafo[laMarin].Pesos[0] = 20;

        grafo[sanRafael].Conexiones[0] = conocoto;
        grafo[sanRafael].Pesos[0] = 10;

        //Reportería
        Console.WriteLine("=== Consulta de estructura del grafo ===");
        for (int i = 0; i < totalNodos; i++)
        {
            Console.WriteLine("Nodo [" + i + "]: " + grafo[i].Nombre);
            for (int j = 0; j < maxConexiones; j++)
            {
                int conectado = grafo[i].Conexiones[j];
                int peso = grafo[i].Pesos[j];
                if (conectado != -1)
                {
                    Console.WriteLine("  ↳ Conecta con [" + conectado + "]: " + grafo[conectado].Nombre + " | Peso: " + peso + " minutos");
                }
            }
        }
        Console.WriteLine("========================================\n");

        // Sección Dijkstra
        grafo[quitumbe].Distancia = 0;

        for (int i = 0; i < totalNodos; i++)
        {
            int actual = -1;
            int menor = int.MaxValue;

            for (int j = 0; j < totalNodos; j++)
            {
                if (!grafo[j].Visitado && grafo[j].Distancia < menor)
                {
                    menor = grafo[j].Distancia;
                    actual = j;
                }
            }

            if (actual == -1) break;

            grafo[actual].Visitado = true;

            for (int k = 0; k < maxConexiones; k++)
            {
                int vecino = grafo[actual].Conexiones[k];
                int peso = grafo[actual].Pesos[k];

                if (vecino != -1 && !grafo[vecino].Visitado)
                {
                    int nuevaDistancia = grafo[actual].Distancia + peso;
                    if (nuevaDistancia < grafo[vecino].Distancia)
                    {
                        grafo[vecino].Distancia = nuevaDistancia;
                        grafo[vecino].Previo = actual;
                    }
                }
            }
        }

        // Mostrar la ruta más corta
        Console.WriteLine("=== Ruta más corta desde Quitumbe a Conocoto ===");
        int[] ruta = new int[maxNodos];
        int pasos = 0;
        int actualDestino = conocoto;

        while (actualDestino != -1)
        {
            ruta[pasos++] = actualDestino;
            actualDestino = grafo[actualDestino].Previo;
        }

        for (int i = pasos - 1; i >= 0; i--)
        {
            Console.Write("[" + grafo[ruta[i]].Nombre + "] ");
            if (i > 0) Console.Write("→ ");
        }

        Console.WriteLine("\nTiempo total estimado: " + grafo[conocoto].Distancia + " minutos");
        Console.WriteLine("===============================================");
    }
}

//Clase Nodo al final del archivo
class Nodo
{
    public string Nombre;
    public int[] Conexiones;
    public int[] Pesos;
    public int Distancia;
    public bool Visitado;
    public int Previo;

    public Nodo(string nombre, int maxConexiones)
    {
        Nombre = nombre;
        Conexiones = new int[maxConexiones];
        Pesos = new int[maxConexiones];
        Distancia = int.MaxValue;
        Visitado = false;
        Previo = -1;

        for (int i = 0; i < maxConexiones; i++)
        {
            Conexiones[i] = -1;
            Pesos[i] = -1;
        }
    }
}