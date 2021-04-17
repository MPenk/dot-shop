using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Klasa do obsług dysków.
    /// </summary>
    [DataContract(Name = "ItemDrive", IsReference = true)]
    [KnownType(typeof(int))]
    [KnownType(typeof(string))]
    internal class ItemDrive : Item, IViewItemStandard
    {
        /// <summary>
        /// Typ dysku.
        /// </summary>
        [DataMember]
        public string type;

        /// <summary>
        /// Interfejs dysku.
        /// </summary>
        [DataMember]
        public string connectionInterface;

        /// <summary>
        /// Wielkosć dysku.
        /// </summary>
        [DataMember]
        public int capacity;


        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemDrive() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemDrive(string name, string model, string manufacturer, string type, string connectionInterface, int capacity, string imgUrl) : base(name, model, "Drive", manufacturer, imgUrl, "Dyski")
        {
            this.type = type;
            this.connectionInterface = connectionInterface;
            this.capacity = capacity;

        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            this.type = createPropertyViews[0].getPropertyContent();
            this.connectionInterface = createPropertyViews[1].getPropertyContent();
            this.capacity = int.Parse(createPropertyViews[2].getPropertyContent());

            createPropertyViews.RemoveRange(0, createPropertyViews.Count);
        }

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            createPropertyViews.Add(new CreatePropertyView("Typ"));
            createPropertyViews.Add(new CreatePropertyView("Interfejs"));
            createPropertyViews.Add(new CreatePropertyView("Pojemność", "GB"));

            for (int i = 0; i < createPropertyViews.Count; i++)
            {
                if (i == 2)
                    createPropertyViews[i].addValidationNumberAndEmpty();
                else
                    createPropertyViews[i].addValidationEmpty();

                stackPanel.Children.Add(createPropertyViews[i]);
            }

        }

        /// <summary>
        /// Dodanie właściwości do widoku
        /// </summary>
        protected override void addPropertiesToView(ItemView itemView)
        {
            itemView.addNewLbl("Typ: " + this.type);
            itemView.addNewLbl("Interfejs: " + this.connectionInterface);
            itemView.addNewLbl("Pojemność: " + this.capacity + "GB");
            base.addPropertiesToView(itemView);
        }
    }
}
