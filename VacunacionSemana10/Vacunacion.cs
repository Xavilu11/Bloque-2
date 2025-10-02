using System;

class Program
{
    // Clase que representa a un ciudadano con su ID y nombre
    public class Ciudadano
    {
        public int Id;
        public string Nombre;
    }

    static void Main()
    {
        // Creamos un arreglo para guardar los 500 ciudadanos
        Ciudadano[] todosLosCiudadanos = new Ciudadano[500];

        // Llenamos el arreglo con ciudadanos del 1 al 500
        for (int i = 0; i < 500; i++)
        {
            Ciudadano c = new Ciudadano();
            c.Id = i + 1;
            c.Nombre = "Ciudadano " + (i + 1);
            todosLosCiudadanos[i] = c;
        }

        // Arreglos para guardar los IDs vacunados con cada dosis
        int[] vacunadosPfizer = GenerarVacunadosAleatorios(75);
        int[] vacunadosAstraZeneca = GenerarVacunadosAleatorios(75);

        // Creamos arreglos para representar los conjuntos
        int[] todosLosIds = GenerarTodosLosIds(500);
        int[] vacunadosTotales = Union(vacunadosPfizer, vacunadosAstraZeneca);
        int[] noVacunados = Diferencia(todosLosIds, vacunadosTotales);
        int[] ambasDosis = Interseccion(vacunadosPfizer, vacunadosAstraZeneca);
        int[] soloPfizer = Diferencia(vacunadosPfizer, vacunadosAstraZeneca);
        int[] soloAstraZeneca = Diferencia(vacunadosAstraZeneca, vacunadosPfizer);

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

    // Genera un arreglo con IDs aleatorios únicos entre 1 y 500
    static int[] GenerarVacunadosAleatorios(int cantidad)
    {
        int[] resultado = new int[cantidad];
        Random rnd = new Random();
        int contador = 0;

        while (contador < cantidad)
        {
            int id = rnd.Next(1, 501);
            bool repetido = false;

            // Verificamos que no se repita
            for (int i = 0; i < contador; i++)
            {
                if (resultado[i] == id)
                {
                    repetido = true;
                    break;
                }
            }

            if (!repetido)
            {
                resultado[contador] = id;
                contador++;
            }
        }

        return resultado;
    }

    // Genera un arreglo con todos los IDs del 1 al n
    static int[] GenerarTodosLosIds(int n)
    {
        int[] ids = new int[n];
        for (int i = 0; i < n; i++)
        {
            ids[i] = i + 1;
        }
        return ids;
    }

    // Devuelve la unión de dos arreglos sin repetir elementos
    static int[] Union(int[] a, int[] b)
    {
        int[] temp = new int[a.Length + b.Length];
        int contador = 0;

        // Agregamos todos los de a
        for (int i = 0; i < a.Length; i++)
        {
            temp[contador++] = a[i];
        }

        // Agregamos los de b que no estén en a
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
            {
                temp[contador++] = b[i];
            }
        }

        // Creamos arreglo final con tamaño exacto
        int[] resultado = new int[contador];
        for (int i = 0; i < contador; i++)
        {
            resultado[i] = temp[i];
        }

        return resultado;
    }

    // Devuelve los elementos que están en a pero no en b
    static int[] Diferencia(int[] a, int[] b)
    {
        int[] temp = new int[a.Length];
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
            {
                temp[contador++] = a[i];
            }
        }

        int[] resultado = new int[contador];
        for (int i = 0; i < contador; i++)
        {
            resultado[i] = temp[i];
        }

        return resultado;
    }

    // Devuelve los elementos que están en ambos arreglos
    static int[] Interseccion(int[] a, int[] b)
    {
        int[] temp = new int[Math.Min(a.Length, b.Length)];
        int contador = 0;

        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                if (a[i] == b[j])
                {
                    temp[contador++] = a[i];
                    break;
                }
            }
        }

        int[] resultado = new int[contador];
        for (int i = 0; i < contador; i++)
        {
            resultado[i] = temp[i];
        }

        return resultado;
    }

    // Muestra los ciudadanos cuyo ID está en el arreglo filtrado
    static void MostrarListado(string titulo, Ciudadano[] ciudadanos, int[] idsFiltrados)
    {
        Console.WriteLine("\n--- " + titulo + " (" + idsFiltrados.Length + ") ---");

        for (int i = 0; i < ciudadanos.Length; i++)
        {
            for (int j = 0; j < idsFiltrados.Length; j++)
            {
                if (ciudadanos[i].Id == idsFiltrados[j])
                {
                    Console.WriteLine(ciudadanos[i].Nombre);
                    break;
                }
            }
        }
    }
}