using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Klasa do obsług karty graficznej.
    /// </summary>
    [DataContract(Name = "ItemsGPU", IsReference = true)]
    [KnownType(typeof(int))]
    [KnownType(typeof(string))]
    internal class ItemGPU : Item, IViewItemStandard
    {
        /// <summary>
        /// Wielkość pamięci karty graficznej.
        /// </summary>
        [DataMember]
        public int VRAM;

        /// <summary>
        /// Rodzaj pamięci karty graficznej.
        /// </summary>
        [DataMember]
        public string VRAMtype;

        /// <summary>
        /// TDP karty graficznej.
        /// </summary>
        [DataMember]
        public int TDP;


        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemGPU() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemGPU(string name, string model, string manufacturer, int VRAM, string VRAMtype, int TDP, string imgUrl) : base(name, model, "GPU", manufacturer, imgUrl, "Grafika")
        {
            this.VRAM = VRAM;
            this.VRAMtype = VRAMtype;
            this.TDP = TDP;

        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            this.VRAM = int.Parse(createPropertyViews[0].getPropertyContent());
            this.VRAMtype = createPropertyViews[1].getPropertyContent();
            this.TDP = int.Parse(createPropertyViews[2].getPropertyContent());

            createPropertyViews.RemoveRange(0, createPropertyViews.Count);
        }

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            createPropertyViews.Add(new CreatePropertyView("Pamęć", "GB"));
            createPropertyViews.Add(new CreatePropertyView("Typ pamięci"));
            createPropertyViews.Add(new CreatePropertyView("TDP", "Watt"));

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
            itemView.addNewLbl("Pamęć: " + this.VRAM + "GB");
            itemView.addNewLbl("Typ pamięci: " + this.VRAMtype);
            itemView.addNewLbl("TDP: " + this.TDP + "Watt");
            base.addPropertiesToView(itemView);
        }
    }
}
