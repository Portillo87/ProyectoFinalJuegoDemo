using Spectre.Console;
using System;
using System.Media;
using System.Threading;

namespace JuegoSudoku
{
    internal class Juego
    {
        private Tablero tablero;
        private InterfazUsuario interfazUsuario;
        private Musica musica;

        public Juego()
        {
            tablero = new Tablero();
            interfazUsuario = new InterfazUsuario();
            musica = new Musica();
        }

        public void Iniciar()
        {
            musica.IniciarMusicaDeFondo();

            while (true)
            {
                AnsiConsole.Clear();
                var eleccion = interfazUsuario.MostrarMenuPrincipal();

                switch (eleccion)
                {
                    case "Jugar":
                        Jugar();
                        break;
                    case "Ayuda":
                        interfazUsuario.MostrarAyuda();
                        break;
                    case "[green]Reiniciar Tablero[/]":
                        tablero.Reiniciar();
                        break;
                    case "[red]Salir (borrar system 32)[/]":
                        musica.DetenerMusicaDeFondo();
                        return;
                }
            }
        }

        private void Jugar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                interfazUsuario.ImprimirTablero(tablero);
                Console.WriteLine($"Puntuación: {tablero.Puntuacion}");
                string entrada = interfazUsuario.PedirMovimiento();

                if (entrada.ToLower() == "salir")
                {
                    return;
                }

                if (!tablero.RealizarMovimiento(entrada))
                {
                    interfazUsuario.MostrarMensaje("Entrada inválida o movimiento no permitido. Presiona cualquier tecla para intentarlo de nuevo...");
                    Console.ReadKey();
                    continue;
                }

                if (tablero.EsCompleto())
                {
                    AnsiConsole.Clear();
                    interfazUsuario.ImprimirTablero(tablero);
                    interfazUsuario.ReproducirSonidoVictoria();
                    interfazUsuario.MostrarMensaje($"¡Felicidades! Completaste el rompecabezas de Sudoku con una puntuación de {tablero.Puntuacion}.");
                    Console.ReadKey();
                    break;
                }
            }

            if (interfazUsuario.PreguntarSiJugarDeNuevo())
            {
                tablero.Reiniciar();
                Jugar();
            }
        }
    }
}
