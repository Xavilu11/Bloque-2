using System; 

// Representamos al jugador de fútbol
class Jugador
{
    public string Nombre;
    public string Posicion;
    public string Numero;

    public Jugador(string nombre, string posicion, string numero)
    {
        Nombre = nombre;
        Posicion = posicion;
        Numero = numero;
    }
}

// Clase para mostrar un equipo con su lista de jugadores
class Equipo
{
    public string Nombre;
    public Jugador[] Jugadores = new Jugador[30]; // Máximo 30 jugadores por equipo
    public int TotalJugadores = 0;

    public Equipo(string nombre)
    {
        Nombre = nombre;
    }

    // Verifica si un jugador está registrado.
    public bool ExisteJugador(string nombre)
    {
        for (int i = 0; i < TotalJugadores; i++)
        {
            if (Jugadores[i].Nombre == nombre)
                return true;
        }
        return false;
    }

    // Agrega un jugador al equipo
    public void AgregarJugador(Jugador j)
    {
        if (TotalJugadores < 100)
        {
            Jugadores[TotalJugadores] = j;
            TotalJugadores++;
        }
    }
}

// Clase principal del programa
class Program
{
    // Arreglo para guardar los equipos registrados
    static Equipo[] equipos = new Equipo[50]; // Máximo 50 equipos
    static int totalEquipos = 0;

    // Busca un equipo por nombre.
    static int BuscarEquipo(string nombre)
    {
        for (int i = 0; i < totalEquipos; i++)
        {
            if (equipos[i].Nombre == nombre)
                return i;
        }
        return -1; // No encontrado
    }

    // Registra un nuevo equipo si no existe
    static void RegistrarEquipo()
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string nombre = Console.ReadLine().Trim();

        if (BuscarEquipo(nombre) != -1)
        {
            Console.WriteLine($"El equipo '{nombre}' ya está registrado.");
        }
        else
        {
            equipos[totalEquipos] = new Equipo(nombre);
            totalEquipos++;
            Console.WriteLine($"Equipo '{nombre}' registrado correctamente.");
        }
    }

    // Registra jugadores en un equipo.
    static void RegistrarJugador()
    {
        if (totalEquipos == 0)
        {
            Console.WriteLine("No hay equipos registrados. Primero registre un equipo.");
            return;
        }

        Console.WriteLine("Equipos registrados:");
        for (int i = 0; i < totalEquipos; i++)
        {
            Console.WriteLine($"- {equipos[i].Nombre}");
        }

        Console.Write("Ingrese el nombre del equipo para registrar jugadores: ");
        string nombreEquipo = Console.ReadLine().Trim();
        int idx = BuscarEquipo(nombreEquipo);

        if (idx == -1)
        {
            Console.WriteLine($"El equipo '{nombreEquipo}' no está registrado.");
            return;
        }

        Equipo eq = equipos[idx];

        while (true)
        {
            Console.Write("Ingrese el nombre del jugador (o 'salir' para terminar): ");
            string nombreJugador = Console.ReadLine().Trim();
            if (nombreJugador.ToLower() == "salir")
                break;

            if (eq.ExisteJugador(nombreJugador))
            {
                Console.WriteLine($"El jugador '{nombreJugador}' ya está registrado en el equipo '{nombreEquipo}'.");
                continue;
            }

            Console.Write("Ingrese la posición del jugador: ");
            string posicion = Console.ReadLine().Trim();

            Console.Write("Ingrese el número del jugador: ");
            string numero = Console.ReadLine().Trim();

            eq.AgregarJugador(new Jugador(nombreJugador, posicion, numero));
            Console.WriteLine($"Jugador '{nombreJugador}' registrado en el equipo '{nombreEquipo}'.");
        }
    }

    // Muestra el reporte completo de equipos y jugadores
    static void MostrarReporte()
    {
        Console.WriteLine("\n--- Reporte de Equipos y Jugadores ---");

        for (int i = 0; i < totalEquipos; i++)
        {
            Equipo eq = equipos[i];
            Console.WriteLine($"\nEquipo: {eq.Nombre}");

            if (eq.TotalJugadores == 0)
            {
                Console.WriteLine("  No hay jugadores registrados.");
            }
            else
            {
                for (int j = 0; j < eq.TotalJugadores; j++)
                {
                    Jugador jugador = eq.Jugadores[j];
                    Console.WriteLine($"  Jugador: {jugador.Nombre}, Posición: {jugador.Posicion}, Número: {jugador.Numero}");
                }
            }
        }
    }

    // Menú principal del programa
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