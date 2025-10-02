// Importamos la librería básica para entrada/salida en consola
using System;

// Esta clase tiene toda la lógica del traductor
class TraductorBasico
{
    // Creamos el diccionario como un arreglo bidimensional de 25 filas y 2 columnas
    // Cada fila representa una palabra en español y su traducción en inglés
    static string[,] diccionario = new string[25, 2]
    {
        // Palabras base sugeridas por el ejercicio
        {"tiempo", "time"}, {"persona", "person"}, {"año", "year"},
        {"camino", "way"}, {"forma", "way"}, {"día", "day"},
        {"cosa", "thing"}, {"hombre", "man"}, {"mundo", "world"},
        {"vida", "life"}, {"mano", "hand"}, {"parte", "part"},
        {"niño", "child"}, {"niña", "child"}, {"ojo", "eye"},
        {"mujer", "woman"}, {"lugar", "place"}, {"trabajo", "work"},
        {"semana", "week"}, {"caso", "case"}, {"punto", "point"},
        {"tema", "point"}, {"gobierno", "government"},
        {"empresa", "company"}, {"compañía", "company"}
    };

    // Variable para llevar el conteo de cuántas palabras hay en el diccionario
    static int totalPalabras = 25;

    // Aquí traduce una frase ingresada por el usuario
    public static void TraducirFrase()
    {
        // Pedimos al usuario que ingrese una frase
        Console.Write("Ingrese una frase en español: ");
        string frase = Console.ReadLine();

        // Validamos que la frase no esté vacía
        if (string.IsNullOrWhiteSpace(frase))
        {
            Console.WriteLine("⚠️ Frase vacía.");
            return;
        }

        // Separamos la frase en palabras usando el espacio como separador
        string[] palabras = frase.Split(' ');
        string resultado = "";

        // Cadena con signos de puntuación que queremos limpiar
        string signos = ".,;:!?\"'()[]{}";

        // Recorremos cada palabra de la frase
        for (int i = 0; i < palabras.Length; i++)
        {
            string palabraOriginal = palabras[i];

            // Quitamos signos al inicio y al final usando Trim
            string palabraLimpia = palabraOriginal.Trim(signos.ToCharArray());

            // Si la palabra tenía signos al final, los guardamos aparte
            string puntuacionFinal = "";
            if (palabraOriginal.Length > palabraLimpia.Length)
            {
                puntuacionFinal = palabraOriginal.Substring(palabraLimpia.Length);
            }

            // Buscamos la traducción en el diccionario
            string traduccion = BuscarTraduccion(palabraLimpia.ToLower());

            if (traduccion != null)
            {
                // Si la palabra original empieza con mayúscula, la conservamos
                if (palabraLimpia.Length > 0 && char.IsUpper(palabraLimpia[0]))
                {
                    traduccion = char.ToUpper(traduccion[0]) + traduccion.Substring(1);
                }

                // Agregamos la traducción con la puntuación final
                resultado += traduccion + puntuacionFinal + " ";
            }
            else
            {
                // Si no está en el diccionario, dejamos la palabra original
                resultado += palabraOriginal + " ";
            }
        }

        // Mostramos la frase traducida parcialmente
        Console.WriteLine("\n🗣 Traducción parcial:");
        Console.WriteLine(resultado.Trim());
    }

    // Este método busca una palabra en español dentro del diccionario
    static string BuscarTraduccion(string palabra)
    {
        // Recorremos todas las filas del diccionario
        for (int i = 0; i < totalPalabras; i++)
        {
            // Comparamos la palabra en español (columna 0)
            if (diccionario[i, 0].ToLower() == palabra)
            {
                // Si la encontramos, devolvemos la traducción en inglés (columna 1)
                return diccionario[i, 1];
            }
        }

        // Si no está, devolvemos null
        return null;
    }

    // Este método permite al usuario agregar una nueva palabra al diccionario
    public static void AgregarPalabra()
    {
        // Verificamos que no se haya llenado el diccionario
        if (totalPalabras >= diccionario.GetLength(0))
        {
            Console.WriteLine("⚠️ Diccionario lleno. No se pueden agregar más palabras.");
            return;
        }

        // Pedimos la palabra en español
        Console.Write("Ingrese la palabra en español: ");
        string esp = Console.ReadLine();

        // Validamos que no esté vacía
        if (string.IsNullOrWhiteSpace(esp))
        {
            Console.WriteLine("⚠️ Español inválido.");
            return;
        }

        // Pedimos la traducción en inglés
        Console.Write("Ingrese la traducción en inglés: ");
        string ing = Console.ReadLine();

        // Validamos que no esté vacía
        if (string.IsNullOrWhiteSpace(ing))
        {
            Console.WriteLine("⚠️ Inglés inválido.");
            return;
        }

        // Guardamos ambas palabras en minúsculas en el diccionario
        diccionario[totalPalabras, 0] = esp.ToLower();
        diccionario[totalPalabras, 1] = ing.ToLower();

        // Aumentamos el contador de palabras
        totalPalabras++;

        Console.WriteLine("✅ Palabra agregada correctamente.");
    }
}

// Esta clase contiene el menú principal del programa
class Program
{
    static void Main()
    {
        // Bucle infinito hasta que el usuario decida salir
        while (true)
        {
            Console.Clear(); // Limpiamos la pantalla
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            // Evaluamos la opción ingresada
            switch (opcion)
            {
                case "1":
                    TraductorBasico.TraducirFrase();
                    break;
                case "2":
                    TraductorBasico.AgregarPalabra();
                    break;
                case "0":
                    Console.WriteLine("👋 ¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("❌ Opción no válida.");
                    break;
            }

            // Pausa antes de volver al menú
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}