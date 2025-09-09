using System;
using System.Collections.Generic;

class CatalogoRevistas
{
    // Lista de revistas reconocidas
    static List<string> catalogo = new List<string>()
    {
        "National Geographic",
        "Time",
        "Forbes",
        "The Economist",
        "Scientific American",
        "Vogue",
        "Wired",
        "Nature",
        "Harvard Business Review",
        "Popular Science"
    };

    static void Main()
    {
        Console.WriteLine("¡Bienvenido al Catálogo de Revistas!");
        bool continuar = true;

        // Bucle principal para mostrar el menú para que el usuario decida qué hacer
        while (continuar)
        {
            Console.WriteLine("\n¿Qué te gustaría hacer?");
            Console.WriteLine("1. Buscar un título de revista");
            Console.WriteLine("2. Salir");
            Console.Write("Por favor, ingresa el número de la opción que prefieras: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    BuscarTitulo();
                    break;
                case "2":
                    continuar = false;
                    Console.WriteLine("¡Gracias por usar el catálogo!");
                    break;
                default:
                    Console.WriteLine("Ups, esa opción no es válida. Intenta de nuevo, por favor.");
                    break;
            }
        }
    }

    // Método que solicita al usuario el título a buscar y mostrar el resultado
    static void BuscarTitulo()
    {
        Console.Write("\nIngresa el título de la revista que deseas buscar: ");
        string tituloBuscado = Console.ReadLine();

        // Convertimos el título ingresado para hacer la búsqueda sin importar mayúsculas o minúsculas
        bool encontrado = BusquedaRecursiva(tituloBuscado.ToLower(), 0);

        if (encontrado)
        {
            Console.WriteLine($"¡Genial! La revista \"{tituloBuscado}\" fue encontrada en el catálogo.");
        }
        else
        {
            Console.WriteLine($"No pudimos encontrar la revista \"{tituloBuscado}\" en nuestro catálogo.");
        }
    }

    // Función recursiva que busca el título en la lista
    // Parámetros:
    // - titulo: el título a buscar 
    // - indice: posición actual en la lista 
    static bool BusquedaRecursiva(string titulo, int indice)
    {
        // Caso base: si llegamos al final de la lista sin encontrar el título, retornamos false
        if (indice >= catalogo.Count)
            return false;

        // Comparamos el título actual en la lista con el título buscado, ambos en minúsculas
        if (catalogo[indice].ToLower() == titulo)
            return true;

        // Si no es el título buscado, llamamos recursivamente para revisar el siguiente índice
        return BusquedaRecursiva(titulo, indice + 1);
    }
}
