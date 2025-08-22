using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Ciudadano
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Ciudad { get; set; }
    public bool VacunadoConPfizer { get; set; }
    public bool VacunadoConAstraZeneca { get; set; }
    public bool TienePrimeraDosis { get; set; }
    public bool TieneSegundaDosis { get; set; }

    public Ciudadano(int id, string nombre, int edad, string ciudad, 
                    bool pfizer, bool astrazeneca, bool primeraDosis, bool segundaDosis)
    {
        Id = id;
        Nombre = nombre;
        Edad = edad;
        Ciudad = ciudad;
        VacunadoConPfizer = pfizer;
        VacunadoConAstraZeneca = astrazeneca;
        TienePrimeraDosis = primeraDosis;
        TieneSegundaDosis = segundaDosis;
    }

    public override string ToString()
    {
        return $"{Nombre}, {Edad} años, {Ciudad}";
    }
}

public class ProgramaVacunacion
{
    private static readonly string[] CiudadesEcuatorianas = {
        "Quito", "Guayaquil", "Cuenca", "Santo Domingo", "Machala",
        "Manta", "Portoviejo", "Ambato", "Riobamba", "Ibarra",
        "Loja", "Quevedo", "Milagro", "Esmeraldas", "Latacunga",
        "Tulcán", "Babahoyo", "Salinas", "Daule", "Sangolquí"
    };

    public static void Main()
    {
        Console.WriteLine("=== SISTEMA DE REPORTE DE VACUNACIÓN COVID-19 ===");
        Console.WriteLine("Ministerio de Salud - Gobierno del Ecuador");
        Console.WriteLine("Generando datos...\n");

        // Generar 500 ciudadanos
        List<Ciudadano> ciudadanos = GenerarCiudadanos();

        // Aplicar operaciones de teoría de conjuntos
        var noVacunados = ciudadanos.Where(c => !c.TienePrimeraDosis && !c.TieneSegundaDosis).ToList();
        var ambasDosis = ciudadanos.Where(c => c.TienePrimeraDosis && c.TieneSegundaDosis).ToList();
        var soloPfizer = ciudadanos.Where(c => c.VacunadoConPfizer && !c.VacunadoConAstraZeneca).ToList();
        var soloAstraZeneca = ciudadanos.Where(c => !c.VacunadoConPfizer && c.VacunadoConAstraZeneca).ToList();

        // Mostrar estadísticas
        MostrarEstadisticas(noVacunados, ambasDosis, soloPfizer, soloAstraZeneca);

        // Generar reporte en PDF
        GenerarReportePDF(noVacunados, ambasDosis, soloPfizer, soloAstraZeneca);

        Console.WriteLine("Reporte generado exitosamente en 'ReporteVacunacionEcuador.pdf'");
        Console.WriteLine("Estadísticas completas disponibles en el archivo PDF");
        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }

    private static List<Ciudadano> GenerarCiudadanos()
    {
        List<Ciudadano> ciudadanos = new List<Ciudadano>();
        Random random = new Random();

        for (int i = 1; i <= 500; i++)
        {
            string nombre = $"Ciudadano {i}";
            int edad = random.Next(18, 80);
            string ciudad = CiudadesEcuatorianas[random.Next(CiudadesEcuatorianas.Length)];

            bool pfizer = false;
            bool astrazeneca = false;
            bool primeraDosis = false;
            bool segundaDosis = false;

            // Asignar vacunas según los requisitos del Ministerio de Salud
            if (i <= 75) // Primeros 75 con Pfizer
            {
                pfizer = true;
                primeraDosis = true;
                segundaDosis = random.NextDouble() > 0.3; // 70% con segunda dosis
            }
            else if (i > 75 && i <= 150) // Siguientes 75 con AstraZeneca
            {
                astrazeneca = true;
                primeraDosis = true;
                segundaDosis = random.NextDouble() > 0.4; // 60% con segunda dosis
            }
            else if (i > 150 && i <= 225) // Algunos ciudadanos con ambas vacunas (teoría de conjuntos)
            {
                if (random.NextDouble() > 0.5)
                {
                    pfizer = true;
                    astrazeneca = random.NextDouble() > 0.7; // 30% con ambas
                }
                else
                {
                    astrazeneca = true;
                    pfizer = random.NextDouble() > 0.7; // 30% con ambas
                }
                primeraDosis = true;
                segundaDosis = random.NextDouble() > 0.2; // 80% con segunda dosis
            }
            else // Resto de ciudadanos
            {
                if (random.NextDouble() > 0.65) // 35% vacunados
                {
                    primeraDosis = true;
                    if (random.NextDouble() > 0.5)
                    {
                        pfizer = true;
                        segundaDosis = random.NextDouble() > 0.6; // 40% con segunda dosis
                    }
                    else
                    {
                        astrazeneca = true;
                        segundaDosis = random.NextDouble() > 0.7; // 30% con segunda dosis
                    }
                }
            }

            ciudadanos.Add(new Ciudadano(i, nombre, edad, ciudad, 
                                       pfizer, astrazeneca, primeraDosis, segundaDosis));
        }

        return ciudadanos;
    }

    private static void MostrarEstadisticas(List<Ciudadano> noVacunados, List<Ciudadano> ambasDosis, 
                                          List<Ciudadano> soloPfizer, List<Ciudadano> soloAstraZeneca)
    {
        Console.WriteLine("=== ESTADÍSTICAS DE VACUNACIÓN EN ECUADOR ===");
        Console.WriteLine($"Total de ciudadanos encuestados: 500");
        Console.WriteLine($"Ciudadanos no vacunados: {noVacunados.Count} ({noVacunados.Count * 100 / 500}%)");
        Console.WriteLine($"Ciudadanos con ambas dosis: {ambasDosis.Count} ({ambasDosis.Count * 100 / 500}%)");
        Console.WriteLine($"Solo vacuna Pfizer: {soloPfizer.Count} ({soloPfizer.Count * 100 / 500}%)");
        Console.WriteLine($"Solo vacuna AstraZeneca: {soloAstraZeneca.Count} ({soloAstraZeneca.Count * 100 / 500}%)");
        Console.WriteLine();

        // Distribución por ciudades
        Console.WriteLine("=== DISTRIBUCIÓN POR CIUDADES ===");
        var porCiudad = noVacunados.GroupBy(c => c.Ciudad)
                                  .OrderByDescending(g => g.Count())
                                  .Take(5);
        
        foreach (var grupo in porCiudad)
        {
            Console.WriteLine($"{grupo.Key}: {grupo.Count()} no vacunados");
        }
    }

    private static void GenerarReportePDF(List<Ciudadano> noVacunados, List<Ciudadano> ambasDosis, 
                                        List<Ciudadano> soloPfizer, List<Ciudadano> soloAstraZeneca)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("ReporteVacunacionEcuador.pdf"))
            {
                // Encabezado PDF
                writer.WriteLine("%PDF-1.4");
                writer.WriteLine("1 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/Type /Catalog");
                writer.WriteLine("/Pages 2 0 R");
                writer.WriteLine(">>");
                writer.WriteLine("endobj");

                writer.WriteLine("2 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/Type /Pages");
                writer.WriteLine("/Kids [3 0 R]");
                writer.WriteLine("/Count 1");
                writer.WriteLine(">>");
                writer.WriteLine("endobj");

                writer.WriteLine("3 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/Type /Page");
                writer.WriteLine("/Parent 2 0 R");
                writer.WriteLine("/MediaBox [0 0 612 792]");
                writer.WriteLine("/Contents 4 0 R");
                writer.WriteLine(">>");
                writer.WriteLine("endobj");

                // Contenido del reporte
                StringBuilder content = new StringBuilder();
                content.AppendLine("BT");
                content.AppendLine("/F1 14 Tf");
                content.AppendLine("50 750 Td");
                content.AppendLine("(REPORTE OFICIAL - MINISTERIO DE SALUD ECUADOR) Tj");
                content.AppendLine("/F1 12 Tf");
                content.AppendLine("0 -25 Td");
                content.AppendLine($"(Campaña de Vacunación COVID-19 - {DateTime.Now:dd/MM/yyyy}) Tj");
                content.AppendLine("0 -30 Td");
                content.AppendLine("(RESULTADOS ESTADÍSTICOS) Tj");
                content.AppendLine("0 -20 Td");
                content.AppendLine($"(Total de ciudadanos: 500) Tj");
                content.AppendLine("0 -15 Td");
                content.AppendLine($"(No vacunados: {noVacunados.Count}) Tj");
                content.AppendLine("0 -15 Td");
                content.AppendLine($"(Con ambas dosis: {ambasDosis.Count}) Tj");
                content.AppendLine("0 -15 Td");
                content.AppendLine($"(Solo Pfizer: {soloPfizer.Count}) Tj");
                content.AppendLine("0 -15 Td");
                content.AppendLine($"(Solo AstraZeneca: {soloAstraZeneca.Count}) Tj");

                // Listado de no vacunados
                content.AppendLine("0 -30 Td");
                content.AppendLine("(CIUDADANOS NO VACUNADOS:) Tj");
                content.AppendLine("/F1 10 Tf");
                foreach (var ciudadano in noVacunados.Take(20))
                {
                    content.AppendLine("0 -12 Td");
                    content.AppendLine($"({ciudadano.Nombre} - {ciudadano.Ciudad} - {ciudadano.Edad} años) Tj");
                }

                // Listado con ambas dosis
                content.AppendLine("0 -30 Td");
                content.AppendLine("/F1 12 Tf");
                content.AppendLine("(CIUDADANOS CON AMBAS DOSIS:) Tj");
                content.AppendLine("/F1 10 Tf");
                foreach (var ciudadano in ambasDosis.Take(15))
                {
                    content.AppendLine("0 -12 Td");
                    content.AppendLine($"({ciudadano.Nombre} - {ciudadano.Ciudad} - {(ciudadano.VacunadoConPfizer ? "Pfizer" : "AstraZeneca")}) Tj");
                }

                // Listado solo Pfizer
                content.AppendLine("0 -30 Td");
                content.AppendLine("/F1 12 Tf");
                content.AppendLine("(CIUDADANOS SOLO CON PFIZER:) Tj");
                content.AppendLine("/F1 10 Tf");
                foreach (var ciudadano in soloPfizer.Take(15))
                {
                    content.AppendLine("0 -12 Td");
                    content.AppendLine($"({ciudadano.Nombre} - {ciudadano.Ciudad} - {ciudadano.Edad} años) Tj");
                }

                // Listado solo AstraZeneca
                content.AppendLine("0 -30 Td");
                content.AppendLine("/F1 12 Tf");
                content.AppendLine("(CIUDADANOS SOLO CON ASTRAZENECA:) Tj");
                content.AppendLine("/F1 10 Tf");
                foreach (var ciudadano in soloAstraZeneca.Take(15))
                {
                    content.AppendLine("0 -12 Td");
                    content.AppendLine($"({ciudadano.Nombre} - {ciudadano.Ciudad} - {ciudadano.Edad} años) Tj");
                }

                content.AppendLine("ET");

                writer.WriteLine("4 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/Length " + content.Length);
                writer.WriteLine(">>");
                writer.WriteLine("stream");
                writer.Write(content.ToString());
                writer.WriteLine("endstream");
                writer.WriteLine("endobj");

                // Final del PDF
                writer.WriteLine("xref");
                writer.WriteLine("0 5");
                writer.WriteLine("0000000000 65535 f ");
                writer.WriteLine("0000000010 00000 n ");
                writer.WriteLine("0000000078 00000 n ");
                writer.WriteLine("0000000176 00000 n ");
                writer.WriteLine("0000000302 00000 n ");
                writer.WriteLine("trailer");
                writer.WriteLine("<<");
                writer.WriteLine("/Size 5");
                writer.WriteLine("/Root 1 0 R");
                writer.WriteLine(">>");
                writer.WriteLine("startxref");
                writer.WriteLine("%%EOF");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar PDF: {ex.Message}");
        }
    }
}

