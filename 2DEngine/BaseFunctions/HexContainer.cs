using System;
using System.Collections;
using System.Collections.Generic;

namespace BaseFunctions
{
    //TODO >
    // Implement ICollection<T>?
    public class HexContainer<T> : IEnumerable<T>
    {
        T[][] items;

        public T this[int i, int j]
        {
            get
            {
                return items[j][i];
            }
            set
            {
                items[j][i] = value;
            }
        }

        public HexContainer(int dimension)
        {
            items = new T[2 * dimension - 1][];
            int i;
            for (i = 0; i < dimension; i++)
            {
                items[i] = new T[dimension + i];
            }
            int count = 0;
            for (i = 2 * dimension - 2; i >= dimension; i--)
            {
                items[i] = new T[dimension + count];
                count++;
            }
        }

        public int RowCount()
        {
            return items.Length;
        }

        public int RowLength(int row)
        {
            return items[row].Length;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T[] jaggedT in items)
                foreach (T t in jaggedT)
                    yield return t;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
