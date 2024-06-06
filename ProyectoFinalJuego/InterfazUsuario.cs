using Spectre.Console;
using System;

namespace JuegoSudoku
{
    internal class InterfazUsuario
    {
        public string MostrarMenuPrincipal()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Bienvenido al Juego de Sudoku")
                    .PageSize(10)
                    .AddChoices(new[] { "Jugar", "Ayuda", "[green]Reiniciar Tablero[/]", "[red]Salir (borrar system 32)[/]" }));
        }

        public void MostrarAyuda()
        {
            AnsiConsole.MarkupLine("[bold]Instrucciones de Sudoku:[/]");
            AnsiConsole.MarkupLine("1. El objetivo es llenar una cuadrícula de 9×9 con dígitos.");
            AnsiConsole.MarkupLine("2. Cada columna, cada fila y cada una de las nueve subcuadrículas de 3×3 deben contener todos los dígitos del 1 al 9.");
            AnsiConsole.MarkupLine("3. Usa el formato 'fila columna número' para colocar un número en el tablero.");
            AnsiConsole.MarkupLine("Presiona cualquier tecla para regresar al menú principal...");
            Console.ReadKey();
        }

        public void ImprimirTablero(Tablero tablero)
        {
            int[,] tableroActual = tablero.ObtenerTablero();

            Console.WriteLine("   1 2 3   4 5 6   7 8 9");
            Console.WriteLine(" ┌───────┬───────┬───────┐");
            for (int i = 0; i < Tablero.Size; i++)
            {
                Console.Write($"{i + 1}│ ");
                for (int j = 0; j < Tablero.Size; j++)
                {
                    Console.Write(tableroActual[i, j] == 0 ? "  " : $"{tableroActual[i, j]} ");
                    if ((j + 1) % 3 == 0)
                        Console.Write("│ ");
                }
                Console.WriteLine();
                if ((i + 1) % 3 == 0 && i != Tablero.Size - 1)
                    Console.WriteLine(" ├───────┼───────┼───────┤");
            }
            Console.WriteLine(" └───────┴───────┴───────┘");
        }

        public string PedirMovimiento()
        {
            Console.WriteLine("Ingresa tu movimiento en el formato 'fila columna número' (ej., '1 2 3' para colocar 3 en la fila 1, columna 2) o 'salir' para volver al menú principal:");
            return Console.ReadLine();
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        public bool PreguntarSiJugarDeNuevo()
        {
            Console.WriteLine("¿Quieres jugar de nuevo? (s/n)");
            return Console.ReadLine().ToLower() == "s";
        }

        public void ReproducirSonidoVictoria()
        {
            try
            {
                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer("victory.wav"))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo reproducir el sonido de victoria: " + ex.Message);
            }
        }
    }
}
