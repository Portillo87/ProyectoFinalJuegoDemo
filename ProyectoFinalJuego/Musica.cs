using System;
using System.Media;
using System.Threading;

namespace JuegoSudoku
{
    internal class Musica
    {
        private Thread musicaDeFondoThread;
        private string musicaDeFondo;
        private string sonidoVictoria;
        private CancellationTokenSource cancellationTokenSource;

        public Musica(string musicaDeFondo, string sonidoVictoria)
        {
            this.musicaDeFondo = musicaDeFondo;
            this.sonidoVictoria = sonidoVictoria;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void IniciarMusicaDeFondo()
        {
            musicaDeFondoThread = new Thread(() => ReproducirMusicaDeFondo(cancellationTokenSource.Token))
            {
                IsBackground = true
            };
            musicaDeFondoThread.Start();
        }

        public void DetenerMusicaDeFondo()
        {
            if (musicaDeFondoThread != null && musicaDeFondoThread.IsAlive)
            {
                cancellationTokenSource.Cancel();
                musicaDeFondoThread.Join();
            }
        }

        public void ReproducirSonidoVictoria()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(sonidoVictoria))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo reproducir el sonido de victoria: " + ex.Message);
            }
        }

        private void ReproducirMusicaDeFondo(CancellationToken token)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(musicaDeFondo))
                {
                    while (!token.IsCancellationRequested)
                    {
                        player.PlayLooping();
                        Thread.Sleep(1000);  // Delay to allow for cancellation check
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo reproducir la música de fondo: " + ex.Message);
            }
        }
    }
}