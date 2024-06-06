using System;
using Spectre.Console;

namespace JuegoSudoku
{
    class Program
    {
        static void Main()
        {
            try
            {
                Juego juego = new Juego();
                juego.Iniciar();
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
            finally
            {
                AnsiConsole.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
