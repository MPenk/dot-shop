using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Klasa do obsług zasilaczy.
    /// </summary>
    [DataContract(Name = "ItemPowerSupply", IsReference = true)]
    [KnownType(typeof(int))]
    [KnownType(typeof(string))]
    internal class ItemPowerSupply : Item, IViewItemStandard
    {
        /// <summary>
        /// Moc zasilacza.
        /// </summary>
        [DataMember]
        public int power;

        /// <summary>
        /// Certyfikat zasilacza.
        /// </summary>
        [DataMember]
        public string certificate;


        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemPowerSupply() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemPowerSupply(string name, string model, string manufacturer, int power, string certificate, string imgUrl) : base(name, model, "PowerSupply", manufacturer, imgUrl, "Zasilacze")
        {
            this.power = power;
            this.certificate = certificate;

        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            this.power = int.Parse(createPropertyViews[0].getPropertyContent());
            this.certificate = createPropertyViews[1].getPropertyContent();

            createPropertyViews.RemoveRange(0, createPropertyViews.Count);
        }

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            createPropertyViews.Add(new CreatePropertyView("Moc", "Watt"));
            createPropertyViews.Add(new CreatePropertyView("Certyfikat"));

            for (int i = 0; i < createPropertyViews.Count; i++)
            {
                if (i==1)
                    createPropertyViews[i].addValidationEmpty();
                else
                    createPropertyViews[i].addValidationNumberAndEmpty();

                stackPanel.Children.Add(createPropertyViews[i]);
            }

        }

        /// <summary>
        /// Dodanie właściwości do widoku
        /// </summary>
        protected override void addPropertiesToView(ItemView itemView)
        {
            itemView.addNewLbl("Moc: " + this.power + "Watt");
            itemView.addNewLbl("Certyfikat: " + this.certificate);
            base.addPropertiesToView(itemView);
        }
    }
}
