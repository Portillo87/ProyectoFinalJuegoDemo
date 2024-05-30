using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalJuego
{
    public class Game
    {
        private const int Size = 9;
        private int[,] tablero;
        private int puntuacion;
        private SoundPlayer reproductorSonido;

        public Game()
        {
            tablero = new int[,]
            {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            };
            puntuacion = 0;
            reproductorSonido = new SoundPlayer(@"C:\Users\MINED\Desktop\background.wav");
        }

        public void Iniciar()
        {
            reproductorSonido.PlayLooping();
            while (true)
            {
                AnsiConsole.Clear();
                var eleccion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Bienvenido al Juego de Sudoku")
                        .PageSize(10)
                        .AddChoices(new[] { "Jugar", "Ayuda", "Salir" }));

                switch (eleccion)
                {
                    case "Jugar":
                        Jugar();
                        break;
                    case "Ayuda":
                        MostrarAyuda();
                        break;
                    case "Salir":
                        reproductorSonido.Stop();
                        return;
                }
            }
        }

        private void Jugar()
        {
            while (true)
            {
                Console.Clear();
                ImprimirTablero();
                Console.WriteLine($"Puntuación: {puntuacion}");
                Console.WriteLine("Ingresa tu movimiento en el formato 'fila columna número' (ej., '1 2 3' para colocar 3 en la fila 1, columna 2):");
                var entrada = Console.ReadLine();
                var partes = entrada.Split(' ');

                if (partes.Length != 3 ||
                    !int.TryParse(partes[0], out int fila) ||
                    !int.TryParse(partes[1], out int columna) ||
                    !int.TryParse(partes[2], out int numero) ||
                    fila < 1 || fila > 9 || columna < 1 || columna > 9 || numero < 1 || numero > 9)
                {
                    Console.WriteLine("Entrada inválida. Presiona cualquier tecla para intentarlo de nuevo...");
                    Console.ReadKey();
                    continue;
                }

                fila--;
                columna--;

                if (tablero[fila, columna] != 0)
                {
                    Console.WriteLine("La celda ya está llena. Presiona cualquier tecla para intentarlo de nuevo...");
                    Console.ReadKey();
                    continue;
                }

                tablero[fila, columna] = numero;
                puntuacion += 10;
                ReproducirSonido(@"C:\Users\briya\Desktop\Will\JuegroSudoko1Demo\move.wav");

                if (EsTableroCompleto())
                {
                    Console.Clear();
                    ImprimirTablero();
                    Console.WriteLine($"¡Felicidades! Completaste el rompecabezas de Sudoku con una puntuación de {puntuacion}.");
                    ReproducirSonido(@"C:\Users\briya\Desktop\Will\JuegroSudoko1Demo\win.wav");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
                }
            }

            Console.WriteLine("¿Quieres jugar de nuevo? (s/n)");
            if (Console.ReadLine().ToLower() == "s")
            {
                ReiniciarTablero();
                Jugar();
            }
        }

        private void MostrarAyuda()
        {
            AnsiConsole.MarkupLine("[bold]Instrucciones de Sudoku:[/]");
            AnsiConsole.MarkupLine("1. El objetivo es llenar una cuadrícula de 9×9 con dígitos.");
            AnsiConsole.MarkupLine("2. Cada columna, cada fila y cada una de las nueve subcuadrículas de 3×3 deben contener todos los dígitos del 1 al 9.");
            AnsiConsole.MarkupLine("3. Usa el formato 'fila columna número' para colocar un número en el tablero.");
            AnsiConsole.MarkupLine("Presiona cualquier tecla para regresar al menú principal...");
            Console.ReadKey();
        }

        private void ImprimirTablero()
        {
            Console.WriteLine("┌───────┬───────┬───────┐");
            for (int i = 0; i < Size; i++)
            {
                Console.Write("│ ");
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(tablero[i, j] == 0 ? "  " : $"{tablero[i, j]} ");
                    if ((j + 1) % 3 == 0)
                        Console.Write("│ ");
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0 && i != Size - 1)
                    Console.WriteLine("├───────┼───────┼───────┤");
            }
            Console.WriteLine("└───────┴───────┴───────┘");
        }

        private bool EsTableroCompleto()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (tablero[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ReiniciarTablero()
        {
            tablero = new int[,]
            {
                { 5, 3, 0, 0, 7, 0, 0, 0, 0 },
                { 6, 0, 0, 1, 9, 5, 0, 0, 0 },
                { 0, 9, 8, 0, 0, 0, 0, 6, 0 },
                { 8, 0, 0, 0, 6, 0, 0, 0, 3 },
                { 4, 0, 0, 8, 0, 3, 0, 0, 1 },
                { 7, 0, 0, 0, 2, 0, 0, 0, 6 },
                { 0, 6, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 4, 1, 9, 0, 0, 5 },
                { 0, 0, 0, 0, 8, 0, 0, 7, 9 }
            };
            puntuacion = 0;
        }

        private void ReproducirSonido(string filePath)
        {
            SoundPlayer musicPlayer = new SoundPlayer(filePath);
            musicPlayer.Play();
        }

    }
}
