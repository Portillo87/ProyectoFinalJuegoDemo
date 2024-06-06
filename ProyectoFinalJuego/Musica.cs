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

        public Musica(string musicaDeFondo, string sonidoVictoria)
        {
            this.musicaDeFondo = musicaDeFondo;
            this.sonidoVictoria = sonidoVictoria;
        }

        public void IniciarMusicaDeFondo()
        {
            musicaDeFondoThread = new Thread(ReproducirMusicaDeFondo);
            musicaDeFondoThread.IsBackground = true;
            musicaDeFondoThread.Start();
        }

        public void DetenerMusicaDeFondo()
        {
            if (musicaDeFondoThread != null && musicaDeFondoThread.IsAlive)
            {
                musicaDeFondoThread.Abort();
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

        private void ReproducirMusicaDeFondo()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(musicaDeFondo))
                {
                    player.PlayLooping();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo reproducir la música de fondo: " + ex.Message);
            }
        }
    }
}
