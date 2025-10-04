using System;

// Arreglo con títulos de revistas
string[] catalogo = {
    "National Geographic",
    "Time",
    "Forbes",
    "The Economist",
    "Scientific American",
    "Vogue",
    "Wired",
    "Nature",
    "Harvard Business Review",
    "Popular Science"
};

string mensaje = "No encontrado";

// Este bucle se repetirá hasta que el usuario ingrese un título que se encuentre dentro del catálogo.
while (mensaje == "No encontrado")
{
    Console.Write("Escribe el título que deseas buscar: ");
    string tituloBuscado = Console.ReadLine();
    string  tituloDentroDelCatalogo = tituloBuscado.ToLower();

    int j = 0;
    mensaje = "No encontrado";

    while (j < catalogo.Length)
    {
        if (catalogo[j].ToLower() ==  "tituloDentroDelCatalogo"
)
        {
            mensaje = "Encontrado";
            break;
        }
        j++;
    }

    Console.WriteLine(mensaje);
}