using System.Collections.Generic;
using System.Runtime.Serialization;

namespace dot_shop
{
    /// <summary>
    /// Klasa do obsługi kategorii przedmiotów
    /// </summary>
    [DataContract(Name = "ItemsCategories", Namespace = "Penk", IsReference = true)]
    public class ItemCategory
    {
        /// <summary>
        /// <c>Lista</c> kategorii.
        /// </summary>
        public static List<ItemCategory> itemCategories = new List<ItemCategory>();

        /// <summary>
        /// <c>Lista</c> przedmiotów w danej kategorii.
        /// </summary>
        [DataMember]
        List<Item> items = new List<Item>();

        /// <summary>
        /// Nazwa kategorii (krótka).
        /// </summary>
        [DataMember]
        public string categoryName;

        /// <summary>
        /// Wyświetlana nazwa kategorii (długa).
        /// </summary>
        [DataMember]
        public string categoryViewedName;

        /// <summary>
        /// Liczba przedmiotów w kategorii.
        /// </summary>
        [DataMember]
        private int itemsCount=0;

        /// <summary>
        /// Konstruktor kategorii.
        /// <para>Przypisuje krótszą nazwę do dłuższej</para>
        /// </summary>
        public ItemCategory(string categoryName)
        {
            this.categoryName = categoryName;
            this.categoryViewedName = categoryName;
            ItemCategory.itemCategories.Add(this);
        }
        /// <summary>
        /// Konstruktor kategorii.
        /// <para>Ustawia osobno któtszą i dłuższa nazwę kategorii</para>
        /// </summary>
        public ItemCategory(string categoryName, string categoryViewedName)
        {
            this.categoryViewedName = categoryViewedName;
            this.categoryName = categoryName;
            ItemCategory.itemCategories.Add(this);
        }

        /// <summary>
        /// Sprawdza czy podana kategoria już istnieje.
        /// <para>Jeśli tak, przypisuje obiekt do istniejącej kategorii.</para>
        /// <para>Jeśli nie, tworzy nową kategorię i pzrypisuje do niej obiekt <paramref name="item"/>.</para>
        /// </summary>
        /// <param name="item">Przedmiot przypisywany do kategorii.</param>
        /// <param name="category">Nazwa kategorii do przypisania.</param>
        /// <returns>Obiekt klasy <c>ItemCategory</c> - kategorię przypisaną do obiektu.</returns>
        static public ItemCategory addItem(string category, Item item) 
        {
            ItemCategory obj = ItemCategory.itemCategories.Find(s => { return s.categoryName.Equals(category); });
            if ( obj == null)
            {
                obj = new ItemCategory(category);
            }
            obj.items.Add(item);
            obj.itemsCount++;
            return obj;

        }

        /// <summary>
        /// Pobranie ilości przedmiotów w kategorii.
        /// </summary>
        public int getCountItemsInCategories() {
            return itemsCount;
        }

        /// <summary>
        /// Metoda zwracająca przedmioty dostępne w kategorii.
        /// </summary>
        /// <returns>Obiekt klasy <c>List<Item></c> - listę wszyskich przedmiotów w danej kategorii.</returns>
        public List<Item> GetItems()
        {
            return this.items;
        }
    }
}
