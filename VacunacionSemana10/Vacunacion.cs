using System;

class Program
{
    // Clase que representa a un ciudadano con código y nombre
    public class Ciudadano
    {
        public string Codigo;
        public string Nombre;
    }

    static void Main()
    {
        // Creamos el conjunto de 500 ciudadanos
        Ciudadano[] todosLosCiudadanos = new Ciudadano[500];

        for (int i = 0; i < 500; i++)
        {
            Ciudadano c = new Ciudadano();
            c.Codigo = "C" + (i + 1).ToString("D3"); // C001, C002, ..., C500
            c.Nombre = "Ciudadano " + (i + 1);
            todosLosCiudadanos[i] = c;
        }

        // Creamos los conjuntos de vacunados ficticios
        string[] vacunadosPfizer = GenerarCodigosAleatorios(75);
        string[] vacunadosAstraZeneca = GenerarCodigosAleatorios(75);

        // Aplicamos operaciones de teoría de conjuntos
        string[] todosLosCodigos = GenerarTodosLosCodigos(500);
        string[] vacunadosTotales = Union(vacunadosPfizer, vacunadosAstraZeneca);
        string[] noVacunados = Diferencia(todosLosCodigos, vacunadosTotales);
        string[] ambasDosis = Interseccion(vacunadosPfizer, vacunadosAstraZeneca);
        string[] soloPfizer = Diferencia(vacunadosPfizer, vacunadosAstraZeneca);
        string[] soloAstraZeneca = Diferencia(vacunadosAstraZeneca, vacunadosPfizer);

        // Menú interactivo
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("\n=== MENÚ DE CONSULTA DE VACUNACIÓN ===");
            Console.WriteLine("1. Ver ciudadanos NO vacunados");
            Console.WriteLine("2. Ver ciudadanos con ambas dosis");
            Console.WriteLine("3. Ver ciudadanos con solo Pfizer");
            Console.WriteLine("4. Ver ciudadanos con solo AstraZeneca");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción (1-5): ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MostrarListado("Ciudadanos NO vacunados", todosLosCiudadanos, noVacunados);
                    break;
                case "2":
                    MostrarListado("Ciudadanos con ambas dosis", todosLosCiudadanos, ambasDosis);
                    break;
                case "3":
                    MostrarListado("Ciudadanos con solo Pfizer", todosLosCiudadanos, soloPfizer);
                    break;
                case "4":
                    MostrarListado("Ciudadanos con solo AstraZeneca", todosLosCiudadanos, soloAstraZeneca);
                    break;
                case "5":
                    continuar = false;
                    Console.WriteLine("Gracias por usar el sistema. ¡Hasta pronto!");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    break;
            }
        }
    }

    // Genera un arreglo con códigos aleatorios únicos tipo "C001" a "C500"
    static string[] GenerarCodigosAleatorios(int cantidad)
    {
        string[] resultado = new string[cantidad];
        Random rnd = new Random();
        int contador = 0;

        while (contador < cantidad)
        {
            int numero = rnd.Next(1, 501);
            string codigo = "C" + numero.ToString("D3");

            bool repetido = false;
            for (int i = 0; i < contador; i++)
            {
                if (resultado[i] == codigo)
                {
                    repetido = true;
                    break;
                }
            }

            if (!repetido)
            {
                resultado[contador] = codigo;
                contador++;
            }
        }

        return resultado;
    }

    // Genera todos los códigos del conjunto total
    static string[] GenerarTodosLosCodigos(int n)
    {
        string[] codigos = new string[n];
        for (int i = 0; i < n; i++)
        {
            codigos[i] = "C" + (i + 1).ToString("D3");
        }
        return codigos;
    }

    // Unión de dos conjuntos sin repetir elementos
    static string[] Union(string[] a, string[] b)
    {
        string[] temp = new string[a.Length + b.Length];
        int contador = 0;

        for (int i = 0; i < a.Length; i++)
            temp[contador++] = a[i];

        for (int i = 0; i < b.Length; i++)
        {
            bool existe = false;
            for (int j = 0; j < a.Length; j++)
            {
                if (b[i] == a[j])
                {
                    existe = true;
                    break;
                }
            }
            if (!existe)
                temp[contador++] = b[i];
        }

        string[] resultado = new string[contador];
        for (int i = 0; i < contador; i++)
            resultado[i] = temp[i];

        return resultado;
    }

    // Diferencia: elementos en 'a' que no están en 'b'
    static string[] Diferencia(string[] a, string[] b)
    {
        string[] temp = new string[a.Length];
        int contador = 0;

        for (int i = 0; i < a.Length; i++)
        {
            bool existe = false;
            for (int j = 0; j < b.Length; j++)
            {
                if (a[i] == b[j])
                {
                    existe = true;
                    break;
                }
            }
            if (!existe)
                temp[contador++] = a[i];
        }

        string[] resultado = new string[contador];
        for (int i = 0; i < contador; i++)
            resultado[i] = temp[i];

        return resultado;
    }

    // Intersección: elementos que están en ambos conjuntos
    static string[] Interseccion(string[] a, string[] b)
    {
        string[] temp = new string[Math.Min(a.Length, b.Length)];
        int contador = 0;

        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                if (a[i] == b[j])
                {
                    bool yaIncluido = false;
                    for (int k = 0; k < contador; k++)
                    {
                        if (temp[k] == a[i])
                        {
                            yaIncluido = true;
                            break;
                        }
                    }
                    if (!yaIncluido)
                    {
                        temp[contador++] = a[i];
                    }
                    break;
                }
            }
        }

        string[] resultado = new string[contador];
        for (int i = 0; i < contador; i++)
            resultado[i] = temp[i];

        return resultado;
    }

    // Muestra los ciudadanos cuyo código está en el conjunto filtrado
    static void MostrarListado(string titulo, Ciudadano[] ciudadanos, string[] codigosFiltrados)
    {
        Console.WriteLine("\n--- " + titulo + " (" + codigosFiltrados.Length + ") ---");

        for (int i = 0; i < ciudadanos.Length; i++)
        {
            for (int j = 0; j < codigosFiltrados.Length; j++)
            {
                if (ciudadanos[i].Codigo == codigosFiltrados[j])
                {
                    Console.WriteLine(ciudadanos[i].Nombre + " (" + ciudadanos[i].Codigo + ")");
                    break;
                }
            }
        }
    }
}
