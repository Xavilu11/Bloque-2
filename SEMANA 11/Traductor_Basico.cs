using System;
using System.Collections.Generic;

class TraductorBasico
{
    public static Dictionary<string, string> diccionario = new Dictionary<string, string>()
    {
        {"tiempo", "time"},
        {"persona", "person"},
        {"año", "year"},
        {"camino", "way"},
        {"forma", "way"},
        {"día", "day"},
        {"cosa", "thing"},
        {"hombre", "man"},
        {"mundo", "world"},
        {"vida", "life"},
        {"mano", "hand"},
        {"parte", "part"},
        {"niño", "child"},
        {"niña", "child"},
        {"ojo", "eye"},
        {"mujer", "woman"},
        {"lugar", "place"},
        {"trabajo", "work"},
        {"semana", "week"},
        {"caso", "case"},
        {"punto", "point"},
        {"tema", "point"},
        {"gobierno", "government"},
        {"empresa", "company"},
        {"compañía", "company"}
    };


    public static void TraducirPalabra()
    {
        Console.Write("Ingrese una palabra en español: ");
        string palabra = (Console.ReadLine() ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(palabra))
        {
            Console.WriteLine("Debe ingresar una palabra.");
            return;
        }

        if (diccionario.TryGetValue(palabra, out string traduccion))
        {
            Console.WriteLine($"Traducción: {traduccion}");
        }
        else
        {
            Console.WriteLine("Palabra no encontrada en el diccionario.");
        }
    }

   public static void MostrarDiccionario()
    {
        Console.WriteLine("\nPalabras disponibles en el diccionario:");
        foreach (var par in diccionario)
        {
            Console.WriteLine($"{par.Key} -> {par.Value}");
        }
    }
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Traductor Básico Español-Inglés ===");
            Console.WriteLine("1. Traducir palabra");
            Console.WriteLine("2. Mostrar todas las palabras");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = (Console.ReadLine() ?? "");

            switch (opcion)
            {
                case "1":
                    TraductorBasico.TraducirPalabra();
                    break;
                case "2":
                    TraductorBasico.MostrarDiccionario();
                    break;
                case "3":
                    Console.WriteLine("¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
