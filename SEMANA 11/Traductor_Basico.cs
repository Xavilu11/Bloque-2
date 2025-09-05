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

    public static void TraducirFrase()
    {
        Console.Write("Ingrese una frase en español: ");
        string? fraseInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fraseInput))
        {
            Console.WriteLine("Debe ingresar una frase.");
            return;
        }

        string[] palabras = fraseInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<string> resultado = new List<string>();

        foreach (string palabraOriginal in palabras)
        {
            // Limpiar signos de puntuación al final de la palabra
            string palabraLimpia = palabraOriginal.TrimEnd('.', ',', ';', ':', '!', '?').ToLower();
            string puntuacion = palabraOriginal.Length > palabraLimpia.Length
                ? palabraOriginal.Substring(palabraLimpia.Length)
                : "";

            if (diccionario.TryGetValue(palabraLimpia, out string traduccion))
            {
                // La palabra debe tener la misma forma que la ingresada, si tiene una mayúscula, también deberá tenerla la
                if (char.IsUpper(palabraOriginal[0]))
                    traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);

                resultado.Add(traduccion + puntuacion);
            }
            else
            {
                resultado.Add(palabraOriginal);
            }
        }

        Console.WriteLine("Traducción:");
        Console.WriteLine(string.Join(" ", resultado));
    }

    public static void AgregarPalabra()
    {
        Console.Write("Ingrese la palabra en español: ");
        string? espInput = Console.ReadLine();
        string esp = (espInput ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(esp))
        {
            Console.WriteLine("Debe ingresar una palabra válida.");
            return;
        }

        Console.Write("Ingrese la traducción en inglés: ");
        string? ingInput = Console.ReadLine();
        string ing = (ingInput ?? "").Trim().ToLower();

        if (string.IsNullOrWhiteSpace(ing))
        {
            Console.WriteLine("Debe ingresar una traducción válida.");
            return;
        }

        if (diccionario.ContainsKey(esp))
        {
            Console.WriteLine("La palabra ya existe en el diccionario. Se actualizará la traducción.");
        }
        diccionario[esp] = ing;
        Console.WriteLine("Palabra agregada/actualizada correctamente.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string? opcionInput = Console.ReadLine();
            string opcion = (opcionInput ?? "").Trim().ToLower();

            switch (opcion)
            {
                case "1":
                    TraductorBasico.TraducirFrase();
                    break;
                case "2":
                    TraductorBasico.AgregarPalabra();
                    break;
                case "0":
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