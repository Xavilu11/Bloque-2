using System;

class CatalogoRevistas
{
    // Creamos un arreglo con 10 títulos de revistas
    // Este arreglo es fijo, no cambia durante la ejecución
    static string[] catalogo = new string[]
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

        // Esta variable nos ayuda a mantener el menú activo
        bool continuar = true;

        // Bucle principal que muestra el menú y espera la opción del usuario
        while (continuar)
        {
            Console.WriteLine("\n¿Qué te gustaría hacer?");
            Console.WriteLine("1. Buscar un título de revista");
            Console.WriteLine("2. Salir");
            Console.Write("Ingresa el número de la opción: ");

            string opcion = Console.ReadLine();

            // Usamos switch para decidir qué hacer según la opción
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
                    Console.WriteLine("Opción no válida. Intenta de nuevo.");
                    break;
            }
        }
    }

    // Este método pide al usuario el título que quiere buscar
    static void BuscarTitulo()
    {
        Console.Write("\nIngresa el título de la revista que deseas buscar: ");
        string tituloBuscado = Console.ReadLine();

        // Convertimos el texto a minúsculas para comparar sin importar mayúsculas
        string tituloNormalizado = tituloBuscado.ToLower();

        // Llamamos a la función recursiva para buscar el título
        bool encontrado = BuscarRecursivo(tituloNormalizado, 0);

        // Mostramos el resultado según lo que devuelva la función
        if (encontrado)
        {
            Console.WriteLine("Encontrado");
        }
        else
        {
            Console.WriteLine("No encontrado");
        }
    }

    // Función recursiva que busca el título en el arreglo
    // Recibe el título en minúsculas y el índice actual
    static bool BuscarRecursivo(string titulo, int indice)
    {
        // Si ya revisamos todo el arreglo y no lo encontramos, devolvemos false
        if (indice >= catalogo.Length)
            return false;

        // Comparamos el título actual con el buscado, ambos en minúsculas
        if (catalogo[indice].ToLower() == titulo)
            return true;

        // Si no es el que buscamos, seguimos buscando en el siguiente índice
        return BuscarRecursivo(titulo, indice + 1);
    }
}