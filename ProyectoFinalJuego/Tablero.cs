using System;

namespace JuegoSudoku
{
    public class Tablero
    {
        public const int Size = 9;
        private int[,] tablero;

        public Tablero()
        {
            Reiniciar();
        }

        public void Reiniciar()
        {
            GenerarTableroAleatorio();
        }

        public void Imprimir()
        {
            InterfazUsuario.ImprimirTablero(tablero);
        }

        public bool EsMovimientoValido(int fila, int columna, int numero)
        {
            return InterfazUsuario.EsMovimientoValido(tablero, fila, columna, numero);
        }

        public void RealizarMovimiento(int fila, int columna, int numero)
        {
            tablero[fila, columna] = numero;
        }

        public bool EsTableroCompleto()
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

        public bool ResolverSudoku()
        {
            for (int fila = 0; fila < Size; fila++)
            {
                for (int columna = 0; columna < Size; columna++)
                {
                    if (tablero[fila, columna] == 0)
                    {
                        for (int numero = 1; numero <= 9; numero++)
                        {
                            if (EsMovimientoValido(fila, columna, numero))
                            {
                                tablero[fila, columna] = numero;

                                if (ResolverSudoku())
                                {
                                    return true;
                                }

                                tablero[fila, columna] = 0;
                            }
                        }
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

            while (count < 20)
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
    }
}