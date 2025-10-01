using System;
using System.Collections.Generic;

class Program
{
    // Clase que representa a un ciudadano con ID y nombre
    public class Ciudadano
    {
        public int Id;
        public string Nombre;
    }

    static void Main()
    {
        // Lista principal con los 500 ciudadanos
        List<Ciudadano> todosLosCiudadanos = new List<Ciudadano>();

        // Generamos los ciudadanos: "Ciudadano 1" hasta "Ciudadano 500"
        for (int i = 1; i <= 500; i++)
        {
            Ciudadano c = new Ciudadano();
            c.Id = i;
            c.Nombre = "Ciudadano " + i;
            todosLosCiudadanos.Add(c);
        }

        // Conjuntos para representar vacunados con Pfizer y AstraZeneca
        HashSet<int> vacunadosPfizer = new HashSet<int>();
        HashSet<int> vacunadosAstraZeneca = new HashSet<int>();
        Random rnd = new Random();

        // Seleccionamos 75 ciudadanos aleatorios para Pfizer
        while (vacunadosPfizer.Count < 75)
        {
            int idAleatorio = rnd.Next(1, 501);
            vacunadosPfizer.Add(idAleatorio);
        }

        // Seleccionamos 75 ciudadanos aleatorios para AstraZeneca
        while (vacunadosAstraZeneca.Count < 75)
        {
            int idAleatorio = rnd.Next(1, 501);
            vacunadosAstraZeneca.Add(idAleatorio);
        }

        // Conjunto con todos los ciudadanos creados
        HashSet<int> todosLosIds = new HashSet<int>();
        for (int i = 1; i <= 500; i++)
        {
            todosLosIds.Add(i);
        }

        // Operaciones de teoría de conjuntos
        HashSet<int> vacunadosTotales = new HashSet<int>(vacunadosPfizer);
        vacunadosTotales.UnionWith(vacunadosAstraZeneca);

        HashSet<int> noVacunados = new HashSet<int>(todosLosIds);
        noVacunados.ExceptWith(vacunadosTotales);

        HashSet<int> ambasDosis = new HashSet<int>(vacunadosPfizer);
        ambasDosis.IntersectWith(vacunadosAstraZeneca);

        HashSet<int> soloPfizer = new HashSet<int>(vacunadosPfizer);
        soloPfizer.ExceptWith(vacunadosAstraZeneca);

        HashSet<int> soloAstraZeneca = new HashSet<int>(vacunadosAstraZeneca);
        soloAstraZeneca.ExceptWith(vacunadosPfizer);

        // Menú interactivo para el usuario
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

    // Método auxiliar para mostrar un listado en consola
    static void MostrarListado(string titulo, List<Ciudadano> ciudadanos, HashSet<int> idsFiltrados)
    {
        Console.WriteLine("\n--- " + titulo + " (" + idsFiltrados.Count + ") ---");

        for (int i = 0; i < ciudadanos.Count; i++)
        {
            if (idsFiltrados.Contains(ciudadanos[i].Id))
            {
                Console.WriteLine(ciudadanos[i].Nombre);
            }
        }
    }
}
}
