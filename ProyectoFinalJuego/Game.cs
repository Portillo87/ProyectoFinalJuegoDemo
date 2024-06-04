using Spectre.Console;
using System;

namespace JuegoSudoku
{
    internal class Juego
    {
        private int[,] tablero;
        private int puntuacion;

        public const int Size = 9;

        public Juego()
        {
            ReiniciarTablero();
            puntuacion = 0;
        }

        public void Iniciar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var eleccion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Bienvenido al Juego de Sudoku")
                        .PageSize(10)
                        .AddChoices(new[] { "Jugar", "Ayuda", "Salir (borrar system 32)" }));

                switch (eleccion)
                {
                    case "Jugar":
                        Jugar();
                        break;
                    case "Ayuda":
                        MostrarAyuda();
                        break;
                    
                    case "Salir (borrar system 32)":
                        return;
                }
            }
        }

        private void Jugar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                ImprimirTablero();
                Console.WriteLine($"Puntuación: {puntuacion}");
                Console.WriteLine("Ingresa tu movimiento en el formato 'fila columna número' (ej., '1 2 3' para colocar 3 en la fila 1, columna 2) o 'salir' para volver al menú principal:");
                string entrada = Console.ReadLine();

                if (entrada.ToLower() == "salir")
                {
                    return;
                }

                if (!TryParseInput(entrada, out int fila, out int columna, out int numero) ||
                    fila < 0 || fila >= Size || columna < 0 || columna >= Size || numero < 1 || numero > 9)
                {
                    Console.WriteLine("Entrada inválida. Presiona cualquier tecla para intentarlo de nuevo...");
                    Console.ReadKey();
                    continue;
                }

                if (tablero[fila, columna] != 0)
                {
                    Console.WriteLine("La celda ya está llena. Presiona cualquier tecla para intentarlo de nuevo...");
                    Console.ReadKey();
                    continue;
                }

                tablero[fila, columna] = numero;
                puntuacion += 10;

                if (EsTableroCompleto())
                {
                    AnsiConsole.Clear();
                    ImprimirTablero();
                    Console.WriteLine($"¡Felicidades! Completaste el rompecabezas de Sudoku con una puntuación de {puntuacion}.");
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
            GenerarTableroAleatorio();
            puntuacion = 0;
        }

        private void GenerarTableroAleatorio()
        {
            tablero = new int[Size, Size];
            Random random = new Random();
            int count = 0;

            while (count < 20) // 20 números aleatorios para empezar
            {
                int fila = random.Next(0, Size);
                int columna = random.Next(0, Size);
                int numero = random.Next(1, 10);

                if (tablero[fila, columna] == 0 && EsMovimientoValido(fila, columna, numero))
                {
                    tablero[fila, columna] = numero;
                    count++;
                }
            }
        }

        private bool EsMovimientoValido(int fila, int columna, int numero)
        {
            for (int i = 0; i < Size; i++)
            {
                if (tablero[fila, i] == numero || tablero[i, columna] == numero)
                    return false;
            }

            int startRow = fila / 3 * 3;
            int startCol = columna / 3 * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tablero[startRow + i, startCol + j] == numero)
                        return false;
                }
            }

            return true;
        }

        private static bool TryParseInput(string input, out int fila, out int columna, out int numero)
        {
            string[] partes = input.Split(' ');
            if (partes.Length == 3 &&
                int.TryParse(partes[0], out fila) &&
                int.TryParse(partes[1], out columna) &&
                int.TryParse(partes[2], out numero))
            {
                fila--;
                columna--;
                return true;
            }
            fila = columna = numero = -1;
            return false;
        }
    }
}
