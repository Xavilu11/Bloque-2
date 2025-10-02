using System;

// Esta clase contiene toda la lógica del traductor
class TraductorBasico
{
    // Arreglo de palabras en inglés (columna izquierda)
    static string[] palabrasIng = new string[50]
    {
        "time",       "person",     "year",       "way",        "day",
        "thing",      "man",        "world",      "life",       "hand",
        "part",       "child",      "eye",        "woman",      "place",
        "work",       "week",       "case",       "point",      "government",
        "company"
    };

    // Arreglo de palabras en español (columna derecha)
    static string[] palabrasEsp = new string[50]
    {
        "tiempo",     "persona",    "año",        "camino / forma", "día",
        "cosa",       "hombre",     "mundo",      "vida",       "mano",
        "parte",      "niño/a",     "ojo",        "mujer",      "lugar",
        "trabajo",    "semana",     "caso",       "punto / tema", "gobierno",
        "empresa / compañía"
    };

    // Contador que indica cuántas palabras hay actualmente en el diccionario
    static int totalPalabras = 21;

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

        // Separar la frase en palabras usando espacio como delimitador
        string[] palabras = frase.Split(' ');
        string resultado = "";

        // Signos de puntuación que queremos limpiar
        string signos = ".,;:!?\"'()[]{}";

        // Recorrer cada palabra de la frase
        for (int i = 0; i < palabras.Length; i++)
        {
            string palabraOriginal = palabras[i];
            string palabraLimpia = palabraOriginal.Trim(signos.ToCharArray());

            // Detectar si tenía puntuación al final
            string puntuacionFinal = "";
            if (palabraOriginal.Length > palabraLimpia.Length)
            {
                puntuacionFinal = palabraOriginal.Substring(palabraLimpia.Length);
            }

            // Buscar traducción en el diccionario
            string traduccion = BuscarTraduccion(palabraLimpia.ToLower());

            if (traduccion != null)
            {
                // Si la palabra original empieza con mayúscula, conservar estilo
                if (palabraLimpia.Length > 0 && char.IsUpper(palabraLimpia[0]))
                {
                    traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);
                }

                resultado += traduccion + puntuacionFinal + " ";
            }
            else
            {
                // Si no está en el diccionario, dejar la palabra original
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
            // Si la palabra buscada está contenida dentro de la entrada en español
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
        if (totalPalabras >= palabrasEsp.Length)
        {
            Console.WriteLine("Diccionario lleno. No se pueden agregar más palabras.");
            return;
        }

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

        // Agregar la nueva palabra en la siguiente posición libre
        palabrasEsp[totalPalabras] = esp;
        palabrasIng[totalPalabras] = ing;
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
