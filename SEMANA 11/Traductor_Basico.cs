using System;

// Clase que contiene toda la lógica del traductor
class TraductorBasico
{
    // Arreglo de palabras en inglés (alineadas con las traducciones en español)
    static string[] palabrasIng = {
        "time",       "person",     "year",       "way",        "day",
        "thing",      "man",        "world",      "life",       "hand",
        "part",       "child",      "eye",        "woman",      "place",
        "work",       "week",       "case",       "point",      "government",
        "company"
    };

    // Arreglo de palabras en español (alineadas con las palabras en inglés)
    static string[] palabrasEsp = {
        "tiempo",     "persona",    "año",        "camino / forma", "día",
        "cosa",       "hombre",     "mundo",      "vida",       "mano",
        "parte",      "niño/a",     "ojo",        "mujer",      "lugar",
        "trabajo",    "semana",     "caso",       "punto / tema", "gobierno",
        "empresa / compañía"
    };

    // Contador que indica cuántas palabras hay actualmente en el diccionario
    static int totalPalabras = palabrasEsp.Length;

    // Traduce una frase ingresada por el usuario
    public static void TraducirFrase()
    {
        Console.Write("Ingrese una frase en español: ");
        string frase = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(frase))
        {
            Console.WriteLine("Frase vacía.");
            return;
        }

        string[] palabras = frase.Split(' ');
        string signos = ".,;:!?\"'()[]{}";
        string resultado = "";

        foreach (string palabraOriginal in palabras)
        {
            string palabraLimpia = palabraOriginal.Trim(signos.ToCharArray());
            string puntuacionFinal = palabraOriginal.Substring(palabraLimpia.Length);

            string traduccion = BuscarTraduccion(palabraLimpia.ToLower());

            if (traduccion != null)
            {
                if (palabraLimpia.Length > 0 && char.IsUpper(palabraLimpia[0]))
                {
                    traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);
                }
                resultado += traduccion + puntuacionFinal + " ";
            }
            else
            {
                resultado += palabraOriginal + " ";
            }
        }

        Console.WriteLine("\n🗣 Traducción parcial:");
        Console.WriteLine(resultado.Trim());
    }

    // Busca una palabra en español y devuelve su traducción en inglés
    static string BuscarTraduccion(string palabra)
    {
        for (int i = 0; i < totalPalabras; i++)
        {
            if (palabrasEsp[i].Contains(palabra))
            {
                return palabrasIng[i];
            }
        }
        return null;
    }

    // Permite agregar nuevas palabras al diccionario
    public static void AgregarPalabra()
    {
        Console.Write("Ingrese la palabra en español: ");
        string esp = Console.ReadLine()?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(esp))
        {
            Console.WriteLine("Español inválido.");
            return;
        }

        Console.Write("Ingrese la traducción en inglés: ");
        string ing = Console.ReadLine()?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(ing))
        {
            Console.WriteLine("Inglés inválido.");
            return;
        }

        // Verificar si ya existe
        for (int i = 0; i < totalPalabras; i++)
        {
            if (palabrasEsp[i] == esp)
            {
                Console.WriteLine("La palabra ya existe en el diccionario.");
                return;
            }
        }

        // Expandir los arreglos manualmente (sin estructuras genéricas)
        string[] nuevoEsp = new string[totalPalabras + 1];
        string[] nuevoIng = new string[totalPalabras + 1];

        for (int i = 0; i < totalPalabras; i++)
        {
            nuevoEsp[i] = palabrasEsp[i];
            nuevoIng[i] = palabrasIng[i];
        }

        nuevoEsp[totalPalabras] = esp;
        nuevoIng[totalPalabras] = ing;

        palabrasEsp = nuevoEsp;
        palabrasIng = nuevoIng;
        totalPalabras++;

        Console.WriteLine("Palabra agregada correctamente.");
    }
}

// Clase que contiene el menú principal del programa
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

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
