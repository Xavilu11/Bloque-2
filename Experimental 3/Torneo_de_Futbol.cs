using System;
using System.Collections.Generic;

namespace RegistroTorneoFutbol
{
    class Jugador
    {
        public string Nombre { get; set; }
        public string Posicion { get; set; }
        public string Numero { get; set; }

        public Jugador(string nombre, string posicion, string numero)
        {
            Nombre = nombre;
            Posicion = posicion;
            Numero = numero;
        }
    }

    class Program
    {
        static HashSet<string> equipos = new HashSet<string>();
        static Dictionary<string, Dictionary<string, Jugador>> jugadoresPorEquipo = new Dictionary<string, Dictionary<string, Jugador>>();

        static void RegistrarEquipo()
        {
            Console.Write("Ingrese el nombre del equipo: ");
            string equipo = Console.ReadLine().Trim();

            if (equipos.Contains(equipo))
            {
                Console.WriteLine($"El equipo '{equipo}' ya está registrado.");
            }
            else
            {
                equipos.Add(equipo);
                Console.WriteLine($"Equipo '{equipo}' registrado correctamente.");
            }
        }

        static void RegistrarJugador()
        {
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados. Primero registre un equipo.");
                return;
            }

            Console.WriteLine("Equipos registrados:");
            foreach (var e in equipos)
            {
                Console.WriteLine($"- {e}");
            }

            Console.Write("Ingrese el nombre del equipo para registrar jugadores: ");
            string equipo = Console.ReadLine().Trim();

            if (!equipos.Contains(equipo))
            {
                Console.WriteLine($"El equipo '{equipo}' no está registrado.");
                return;
            }

            if (!jugadoresPorEquipo.ContainsKey(equipo))
            {
                jugadoresPorEquipo[equipo] = new Dictionary<string, Jugador>();
            }

            while (true)
            {
                Console.Write("Ingrese el nombre del jugador (o 'salir' para terminar): ");
                string nombre = Console.ReadLine().Trim();
                if (nombre.ToLower() == "salir")
                    break;

                if (jugadoresPorEquipo[equipo].ContainsKey(nombre))
                {
                    Console.WriteLine($"El jugador '{nombre}' ya está registrado en el equipo '{equipo}'.");
                    continue;
                }

                Console.Write("Ingrese la posición del jugador: ");
                string posicion = Console.ReadLine().Trim();

                Console.Write("Ingrese el número del jugador: ");
                string numero = Console.ReadLine().Trim();

                jugadoresPorEquipo[equipo][nombre] = new Jugador(nombre, posicion, numero);
                Console.WriteLine($"Jugador '{nombre}' registrado en el equipo '{equipo}'.");
            }
        }

        static void MostrarReporte()
        {
            Console.WriteLine("\n--- Reporte de Equipos y Jugadores ---");
            foreach (var equipo in equipos)
            {
                Console.WriteLine($"\nEquipo: {equipo}");
                if (jugadoresPorEquipo.ContainsKey(equipo) && jugadoresPorEquipo[equipo].Count > 0)
                {
                    foreach (var jugador in jugadoresPorEquipo[equipo].Values)
                    {
                        Console.WriteLine($"  Jugador: {jugador.Nombre}, Posición: {jugador.Posicion}, Número: {jugador.Numero}");
                    }
                }
                else
                {
                    Console.WriteLine("  No hay jugadores registrados.");
                }
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nOpciones:");
                Console.WriteLine("1. Registrar equipo");
                Console.WriteLine("2. Registrar jugador de equipo");
                Console.WriteLine("3. Mostrar reporte");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine().Trim();

                switch (opcion)
                {
                    case "1":
                        RegistrarEquipo();
                        break;
                    case "2":
                        RegistrarJugador();
                        break;
                    case "3":
                        MostrarReporte();
                        break;
                    case "4":
                        Console.WriteLine("Saliendo del programa.");
                        return;
                    default:
                        Console.WriteLine("Opción no válida, intente de nuevo.");
                        break;
                }
            }
        }
    }
}
