using System;
using System.Collections.Generic;
using System.Text;

namespace dot_shop
{
    /// <summary>
    /// Klasa porównania przedmiotu od góry.
    /// </summary>
    class ItemCompareNameUP : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.CompareTo(y);

        }
    }

    /// <summary>
    /// Klasa porównania przedmiotu w dół.
    /// </summary>
    class ItemCompareNameDown : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return -x.CompareTo(y);

        }
    }
}
