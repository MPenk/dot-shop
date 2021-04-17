using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace dot_shop
{
    /// <summary>
    /// Odpowiada za przechowywanie oraz operacje na obiektach typu <c>Item</c>.
    /// </summary>
    [DataContract(Name = "Items", IsReference = true)]
    [KnownType(typeof(ItemCPU))]
    [KnownType(typeof(ItemOther))]
    [KnownType(typeof(ItemRAM))]
    [KnownType(typeof(ItemGPU))]
    [KnownType(typeof(ItemPowerSupply))]
    [KnownType(typeof(ItemMotherboard))]
    [KnownType(typeof(ItemDrive))]

    public abstract class Item : BrowseInfo, IComparable
    {

        /// <summary>
        /// <c>Lista</c> przedmiotów.
        /// </summary>
        public static List<Item> items = new List<Item>();

        /// <summary>
        /// Model przedmiotu.
        /// </summary>
        [DataMember]
        protected string model { get; set; }

        /// <summary>
        /// Kategoria przedmiotu.
        /// </summary>
        [DataMember]
        protected ItemCategory ItemCategory;

        /// <summary>
        /// Producent przedmiotu.
        /// </summary>
        [DataMember]
        protected string manufacturer { get; set; }

        /// <summary>
        /// <c>Lista</c> dodatkowych właściwości przedmiotu.
        /// </summary>
        [DataMember]
        List<ItemProperty> itemProperties = new List<ItemProperty>();

        public Item() { }
        /// <summary>
        /// Konstruktor klasy <c>Item</c>. Musi zawierać: <paramref name="name"/>, <paramref name="model"/>, <paramref name="category"/>, <paramref name="manufacturer"/> oraz <paramref name="imgUrl"/>.
        /// </summary>
        /// <param name="name">Nazwa przedmiotu</param>
        /// <param name="model">Model przedmiotu</param>
        /// <param name="category">Skrócona nazwa kategorii</param>
        /// <param name="manufacturer">Producent przedmiotu</param>
        /// <param name="imgUrl">URL zdjęcia przedmiotu</param>
        public Item(string name, string model, string category, string manufacturer, string imgUrl) : base(name, imgUrl)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            Item.items.Add(this);
            this.ItemCategory = ItemCategory.addItem(category, this);
        }

        /// <summary>
        /// Konstruktor klasy <c>Item</c>. Musi zawierać: <paramref name="name"/>, <paramref name="model"/>, <paramref name="category"/>, <paramref name="manufacturer"/>, <paramref name="imgUrl"/> oraz <paramref name="categoryViewedName"/>.
        /// <para> Umożliwia dodanie wyświetlanej nazyw kategorii</para>
        /// </summary>
        ///
        /// <param name="name">Nazwa przedmiotu</param>
        /// <param name="model">Model przedmiotu</param>
        /// <param name="category">Skrócona nazwa kategorii</param>
        /// <param name="manufacturer">Producent przedmiotu</param>
        /// <param name="imgUrl">URL zdjęcia przedmiotu</param>
        /// <param name="categoryViewedName">Wyświetlana nazwa kategorii</param>
        public Item(string name, string model, string category, string manufacturer, string imgUrl, string categoryViewedName) : base(name, imgUrl)
        {
            this.manufacturer = manufacturer;
            this.model = model;
            Item.items.Add(this);
            this.ItemCategory = ItemCategory.addItem(category, this);
            this.setCategoryViewedName(categoryViewedName);
        }

        /// <summary>
        /// Tworzy i dodaje właściwość do przedmiotu.
        /// </summary>
        /// <param name="propertyName">Nazwa nowej właściwości przedmiotu</param>
        /// <param name="napropertyContent">Zawartość nowej właściwości przedmiotu</param>
        public void CreateAndAddItemProperty(string propertyName, string propertyContent)
        {
            this.itemProperties.Add(new ItemProperty(propertyName, propertyContent));
        }

        /// <summary>
        /// Wstawianie podstawowych wartości.
        /// </summary>
        public void setDefaultInformation(string name, string model, string category, string manufacturer, string imgUrl) 
        {
            this.manufacturer = manufacturer;
            this.model = model;
            this.name = name;
            this.imgUrl = imgUrl;
            Guid photoID = System.Guid.NewGuid();
            this.fullFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\dot-shop\\img\\" + photoID.ToString() + ".jpg";
            Item.items.Add(this);
            this.ItemCategory = ItemCategory.addItem(category, this);
        }

        /// <summary>
        /// Znajduje i zwraca kategorię danego przedmiotu.
        /// </summary>
        /// <returns>Obiekt klasy <c>ItemCategory</c></returns>
        public ItemCategory GetCategory()
        {
            return this.ItemCategory;
        }

        /// <summary>
        /// Pobieranie nazwy przedmiotu.
        /// </summary>
        public string GetItemName() { return this.name; }

        /// <summary>
        /// Ustawia widzialną nazwę kategorii do niej przypisanej.
        /// </summary>
        /// <param name="name">Widzialna nazwa kategorii</param>
        public void setCategoryViewedName(string name)
        {
            this.ItemCategory.categoryViewedName = name;
        }

        /// <summary>
        /// Dodawanie zmiennych do widoku
        /// </summary>
        protected virtual void addPropertiesToView(ItemView itemView)
        {
            foreach (var property in this.itemProperties)
            {
                itemView.addNewLbl(property.GetPropertyName() + ": " + property.GetPropertyContent());
            }
            
        }

        /// <summary>
        /// Konfiguracja i zwrócenie <c>ItemView</c> odpowiedzialne za wyświetlenie przedmiotu.
        /// </summary>
        public async Task<ItemView> ConfigureAndGetItemView()
        {
            ItemView itemView = new ItemView(this);
            itemView.HorizontalAlignment = HorizontalAlignment.Stretch;
            itemView.VerticalAlignment = VerticalAlignment.Stretch;
            itemView.Margin = new Thickness(5, 10, 5, 10);
            itemView.setLblName(this.name);
            itemView.addNewLbl("Producent: " + this.manufacturer);
            itemView.addNewLbl("Model: " + this.model);

            addPropertiesToView(itemView);
            await Task.Delay(15);
            itemView.updateWrap();

            if (!File.Exists(this.fullFilePath))
                _ = GetNewImageAsync(itemView, this.bitmap, this.imgUrl, this.fullFilePath);
            else
                itemView.imgItemPrev.Source = new BitmapImage(new Uri(this.fullFilePath));

            return itemView;
        }

        /// <summary>
        /// Porównanie.
        /// </summary>
        public int CompareTo(object obj)
        {
            if (this.GetCategory().categoryName == ((Item)obj).GetCategory().categoryName)
            {
                return this.model.CompareTo(((Item)obj).model);
            }
            return this.GetCategory().categoryName.CompareTo(((Item)obj).GetCategory().categoryName);
        }
    }
}
