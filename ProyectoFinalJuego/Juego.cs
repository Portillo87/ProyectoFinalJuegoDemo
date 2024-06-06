using Spectre.Console;
using System;

namespace JuegoSudoku
{
    public class Juego
    {
        private Tablero tablero;
        private int puntuacion;
        private Musica musica;

        public Juego()
        {
            tablero = new Tablero();
            puntuacion = 0;
            musica = new Musica("waluigi.wav", "victory.wav");
            musica.IniciarMusicaDeFondo();
        }

        public void Iniciar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                InterfazUsuario.ShowWelcomeMessage();
                var eleccion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(new[] { "Jugar", "Resolver Automáticamente [red](SOLO USAR DESPUES DE HABERLE DADO A JUGAR)[/]", "Ayuda", "[green]Reiniciar Tablero[/]", "[red]Salir (borrar system 32)[/]" }));

                switch (eleccion)
                {
                    case "Jugar":
                        Jugar();
                        break;
                    case "Resolver Automáticamente [red](SOLO USAR DESPUES DE HABERLE DADO A JUGAR)[/]":
                        ResolverAutomaticamente();
                        break;
                    case "Ayuda":
                        InterfazUsuario.MostrarAyuda();
                        break;
                    case "[green]Reiniciar Tablero[/]":
                        tablero.Reiniciar();
                        break;
                    case "[red]Salir (borrar system 32)[/]":
                        musica.DetenerMusicaDeFondo();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        return;
                }
            }
        }

        private void Jugar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                tablero.Imprimir();
                InterfazUsuario.MostrarPuntuacion(puntuacion);
                Console.WriteLine("Ingresa tu movimiento en el formato 'fila columna número' (ej., '1 2 3' para colocar 3 en la fila 1, columna 2) o 'salir' para volver al menú principal:");
                string entrada = Console.ReadLine();

                if (entrada.ToLower() == "salir")
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    return;
                }

                if (!InterfazUsuario.TryParseInput(entrada, out int fila, out int columna, out int numero) ||
                    !tablero.EsMovimientoValido(fila, columna, numero))
                {
                    InterfazUsuario.MostrarMensajeError("Entrada inválida o movimiento no válido.");
                    continue;
                }

                tablero.RealizarMovimiento(fila, columna, numero);
                puntuacion += 10;

                if (tablero.EsTableroCompleto())
                {
                    AnsiConsole.Clear();
                    tablero.Imprimir();
                    musica.ReproducirSonidoVictoria();
                    InterfazUsuario.MostrarMensajeVictoria(puntuacion);
                    break;
                }
            }

            if (InterfazUsuario.PreguntarReiniciar())
            {
                tablero.Reiniciar();
                Jugar();
            }
        }

        private void ResolverAutomaticamente()
        {
            if (tablero.ResolverSudoku())
            {
                AnsiConsole.Clear();
                tablero.Imprimir();
                Console.WriteLine("El Sudoku ha sido resuelto automáticamente.");
            }
            else
            {
                Console.WriteLine("No se pudo resolver el Sudoku automáticamente.");
            }

            Console.WriteLine("Presiona cualquier tecla para volver al menú principal...");
            Console.ReadKey();
        }
    }
}
