using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{

    /// <summary>
    /// Klasa do obsług procesorów.
    /// </summary>
    [DataContract(Name = "ItemsCPU", IsReference = true)]
    internal class ItemCPU : Item, IViewItemStandard
    {
        /// <summary>
        /// Szybkość procesora.
        /// </summary>
        [DataMember]
        public int clockSpeed;

        /// <summary>
        /// Szybkość procesora w trybie boost.
        /// </summary>
        [DataMember]
        public int clockBoost;

        /// <summary>
        /// Ilosć pamięci cache procesora.
        /// </summary>
        [DataMember]
        public int cacheSize;

        /// <summary>
        /// Rdzenie procesora.
        /// </summary>
        [DataMember]
        public int cores;

        /// <summary>
        /// Wątki procesora.
        /// </summary>
        [DataMember]
        public int threads;

        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemCPU() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemCPU(string name, string model, string manufacturer, int clockSpeed, int clockBoost, int cacheSize, int cores, int threads, string imgUrl) : base(name, model, "CPU", manufacturer, imgUrl, "Procesor")
        {
            this.cacheSize = cacheSize;
            this.clockSpeed = clockSpeed;
            this.cores = cores;
            this.clockBoost = clockBoost;
            this.threads = threads;
        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            this.clockSpeed = int.Parse(createPropertyViews[0].getPropertyContent());
            this.clockBoost = int.Parse(createPropertyViews[1].getPropertyContent());
            this.cacheSize = int.Parse(createPropertyViews[2].getPropertyContent());
            this.cores = int.Parse(createPropertyViews[3].getPropertyContent());
            this.threads = int.Parse(createPropertyViews[4].getPropertyContent());
            createPropertyViews.RemoveRange(0, createPropertyViews.Count);

        }

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            createPropertyViews.Add(new CreatePropertyView("Taktowanie", "mHz"));
            createPropertyViews.Add(new CreatePropertyView("Taktowanie Turbo", "mHz"));
            createPropertyViews.Add(new CreatePropertyView("Cache", "MB"));
            createPropertyViews.Add(new CreatePropertyView("Rdzenie"));
            createPropertyViews.Add(new CreatePropertyView("Wątki"));

            foreach (var item in createPropertyViews)
            {
                item.addValidationNumberAndEmpty();
                stackPanel.Children.Add(item);
            }
        }

        /// <summary>
        /// Dodanie właściwości do widoku
        /// </summary>
        protected override void addPropertiesToView(ItemView itemView)
        {
            itemView.addNewLbl("Taktowanie: " + this.clockSpeed + "mHz");
            itemView.addNewLbl("Taktowanie Turbo: " + this.clockBoost + "mHz");
            itemView.addNewLbl("Cache: " + this.cacheSize + "MB");
            itemView.addNewLbl("Rdzenie: " + this.cores);
            itemView.addNewLbl("Wątki: " + this.threads);
            base.addPropertiesToView(itemView);
        }
    }
}
