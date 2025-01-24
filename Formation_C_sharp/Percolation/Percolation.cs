using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Percolation
{
    public class Percolation
    {
        private readonly bool[,] _open;
        private readonly bool[,] _full;
        private readonly int _size;
        private bool _percolate;

        public Percolation(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "Taille de la grille négative ou nulle.");
            }

            _open = new bool[size, size];
            _full = new bool[size, size];
            _size = size;
        }

        public bool IsOpen(int i, int j)
        {
            return _open[i,j];
        }

        public bool IsFull(int i, int j)
        {
            return _full[i,j];
        }

        public bool Percolate()
        {
            for (int i=0;i<_size;i++)
            {
                bool allCheck = false;
                for (int j=0;j<_size;j++)
                {
                    if (IsFull(i,j))
                    {
                        allCheck = true;
                        break;
                    }
                }
                if (allCheck==false)
                {
                    _percolate = false;
                    return false;
                }
            }
            _percolate = true;
            return true;
        }

        public List<KeyValuePair<int, int>> CloseNeighbors(int i, int j)
        {
            List<KeyValuePair<int, int>> nei = new List<KeyValuePair<int, int>>();

            if (i>0)
            {
                nei.Add(new KeyValuePair<int, int>(i - 1, j));
            }
            if (j>0)
            {
                nei.Add(new KeyValuePair<int, int>(i, j-1));
            }
            if (i<_size-1)
            {
                nei.Add(new KeyValuePair<int, int>(i+1, j));
            }
            if (j < _size - 1)
            {
                nei.Add(new KeyValuePair<int, int>(i, j+1));
            }
            return nei;
        }

        public void Open(int i, int j)
        {
            if (i==0)
            {
                _full[i, j] = true;
            }
            _open[i, j] = true;

            var neis = CloseNeighbors(i, j);
            for (int k=0; k<neis.Count; k++)
            {
                if (IsFull(neis[k].Key, neis[k].Value))
                {
                    _full[i, j] = true;
                }

            }

            if (IsFull(i,j)) //update voisons
            {
                for (int p = 0; p < neis.Count; p++)
                {
                    if (IsOpen(neis[p].Key, neis[p].Value))
                    {
                        _full[neis[p].Key, neis[p].Value] = true;
                    }

                }
            }
        }

    }
}
