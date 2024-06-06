using System;
using System.Media;
using System.Threading;

namespace JuegoSudoku
{
    internal class Musica
    {
        private Thread musicaDeFondoThread;

        public void IniciarMusicaDeFondo()
        {
            musicaDeFondoThread = new Thread(ReproducirMusicaDeFondo);
            musicaDeFondoThread.IsBackground = true;
            musicaDeFondoThread.Start();
        }

        private void ReproducirMusicaDeFondo()
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer("waluigi.wav"))
                {
                    while (true)
                    {
                        player.PlaySync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo reproducir la música de fondo: " + ex.Message);
            }
        }

        public void DetenerMusicaDeFondo()
        {
            if (musicaDeFondoThread != null && musicaDeFondoThread.IsAlive)
            {
                musicaDeFondoThread.Abort();
            }
        }
    }
}
