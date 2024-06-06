using Spectre.Console;
using System;

namespace JuegoSudoku
{
    public class InterfazUsuario
    {
        public static void ShowWelcomeMessage()
        {
            var panel = new Panel("[bold]Bienvenido al Juego de sudoku[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(Style.Parse("blue"))
                .Header("SUDOKU", Justify.Center);
            AnsiConsole.Write(panel);
        }

        public static void MostrarPuntuacion(int puntuacion)
        {
            AnsiConsole.MarkupLine($"[bold]Puntuación: {puntuacion}[/]");
        }

        public static void MostrarAyuda()
        {
            AnsiConsole.MarkupLine("[bold]Instrucciones de Sudoku:[/]");
            AnsiConsole.MarkupLine("1. El objetivo es llenar una cuadrícula de 9×9 con dígitos.");
            AnsiConsole.MarkupLine("2. Cada columna, cada fila y cada una de las nueve subcuadrículas de 3×3 deben contener todos los dígitos del 1 al 9.");
            AnsiConsole.MarkupLine("3. Usa el formato 'fila columna número' para colocar un número en el tablero.");
            AnsiConsole.MarkupLine("4. Recuerda que en el sudoku no puedes eliminar números así que si te equivocas tienes que reiniciar el tablero.");
            AnsiConsole.MarkupLine("Presiona cualquier tecla para regresar al menú principal...");
            Console.ReadKey();
        }

        public static void ImprimirTablero(int[,] tablero)
        {
            Console.WriteLine("   1 2 3   4 5 6   7 8 9");
            Console.WriteLine(" ┌───────┬───────┬───────┐");
            for (int i = 0; i < Tablero.Size; i++)
            {
                Console.Write($"{i + 1}│ ");
                for (int j = 0; j < Tablero.Size; j++)
                {
                    Console.Write(tablero[i, j] == 0 ? "  " : $"{tablero[i, j]} ");
                    if ((j + 1) % 3 == 0)
                        Console.Write("│ ");
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0 && i != Tablero.Size - 1)
                    Console.WriteLine(" ├───────┼───────┼───────┤");
            }
            Console.WriteLine(" └───────┴───────┴───────┘");
        }

        public static bool EsMovimientoValido(int[,] tablero, int fila, int columna, int numero)
        {
            for (int i = 0; i < Tablero.Size; i++)
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

        public static bool TryParseInput(string input, out int fila, out int columna, out int numero)
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

        public static void MostrarMensajeError(string mensaje)
        {
            AnsiConsole.MarkupLine($"[bold red]{mensaje}[/]");
            Console.WriteLine("Presiona cualquier tecla para intentarlo de nuevo...");
            Console.ReadKey();
        }

        public static void MostrarMensajeVictoria(int puntuacion)
        {
            AnsiConsole.MarkupLine($"[bold green]¡Felicidades! Completaste el rompecabezas de Sudoku con una puntuación de {puntuacion}.[/]");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public static bool PreguntarReiniciar()
        {
            Console.WriteLine("¿Quieres jugar de nuevo? (s/n)");
            return Console.ReadLine().ToLower() == "s";
        }
    }
}
