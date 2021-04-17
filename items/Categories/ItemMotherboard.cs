using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{
    [DataContract(Name = "ItemMotherboard", IsReference = true)]
    [KnownType(typeof(int))]
    [KnownType(typeof(string))]
    internal class ItemMotherboard : Item, IViewItemStandard
    {
        /// <summary>
        /// Chipset płyty głównej.
        /// </summary>
        [DataMember]
        public string chipset;

        /// <summary>
        /// Socket płyty głównej.
        /// </summary>
        [DataMember]
        public string socket;

        /// <summary>
        /// Typ ramu płyty głównej.
        /// </summary>
        [DataMember]
        public string RAM;


        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemMotherboard() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemMotherboard(string name, string model, string manufacturer, string chipset, string socket, string RAM, string imgUrl) : base(name, model, "Motherboard", manufacturer, imgUrl, "Płyty_główne")
        {
            this.chipset = chipset;
            this.socket = socket;
            this.RAM = RAM;

        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            this.chipset = createPropertyViews[0].getPropertyContent();
            this.socket = createPropertyViews[1].getPropertyContent();
            this.RAM = createPropertyViews[2].getPropertyContent();

            createPropertyViews.RemoveRange(0, createPropertyViews.Count);
        }

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            createPropertyViews.Add(new CreatePropertyView("Chipset"));
            createPropertyViews.Add(new CreatePropertyView("Socket"));
            createPropertyViews.Add(new CreatePropertyView("Obsługiwany RAM"));

            for (int i = 0; i < createPropertyViews.Count; i++)
            {
                createPropertyViews[i].addValidationEmpty();

                stackPanel.Children.Add(createPropertyViews[i]);
            }

        }

        /// <summary>
        /// Dodanie właściwości do widoku
        /// </summary>
        protected override void addPropertiesToView(ItemView itemView)
        {
            itemView.addNewLbl("Chipset: " + this.chipset);
            itemView.addNewLbl("Socket: " + this.socket);
            itemView.addNewLbl("Obsługiwany RAM: " + this.RAM);
            base.addPropertiesToView(itemView);
        }
    }
}
