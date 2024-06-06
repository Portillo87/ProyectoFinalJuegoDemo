using System;

namespace JuegoSudoku
{
    internal class Tablero
    {
        private int[,] tablero;
        public int Puntuacion { get; private set; }

        public const int Size = 9;

        public Tablero()
        {
            Reiniciar();
        }

        public void Reiniciar()
        {
            GenerarTableroAleatorio();
            Puntuacion = 0;
        }

        public bool RealizarMovimiento(string entrada)
        {
            if (!TryParseInput(entrada, out int fila, out int columna, out int numero) ||
                fila < 0 || fila >= Size || columna < 0 || columna >= Size || numero < 1 || numero > 9)
            {
                return false;
            }

            if (tablero[fila, columna] != 0 || !EsMovimientoValido(fila, columna, numero))
            {
                return false;
            }

            tablero[fila, columna] = numero;
            Puntuacion += 10;
            return true;
        }

        public bool EsCompleto()
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

        public int[,] ObtenerTablero()
        {
            return tablero;
        }
    }
}
