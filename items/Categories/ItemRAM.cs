using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Klasa do obsług pamięci RAM.
    /// </summary>
    [DataContract(Name = "ItemsRAM", IsReference = true)]
    [KnownType(typeof(int))]
    [KnownType(typeof(string))]
    internal class ItemRAM : Item, IViewItemStandard
    {
        /// <summary>
        /// Wielkość pamięci.
        /// </summary>
        [DataMember]
        public int Size;

        /// <summary>
        /// Typ pamięci.
        /// </summary>
        [DataMember]
        public string Type;

        /// <summary>
        /// Prędkość pamięci.
        /// </summary>
        [DataMember]
        public int Speed;

        /// <summary>
        /// Opóźnienie pamięci.
        /// </summary>
        [DataMember]
        public string Lag;

        /// <summary>
        /// Ilość kości pamięci.
        /// </summary>
        [DataMember]
        public int NumberOfModules;


        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemRAM() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemRAM(string name, string model, string manufacturer, int Size, int NumberOfModules, string Type, int Speed, string Lag , string imgUrl) : base(name, model, "RAM", manufacturer, imgUrl, "RAM")
        {
            this.Size = Size;
            this.Type = Type;
            this.Speed = Speed;
            this.Lag = Lag;
            this.NumberOfModules = NumberOfModules;


        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            this.Size = int.Parse(createPropertyViews[0].getPropertyContent());
            this.NumberOfModules = int.Parse(createPropertyViews[1].getPropertyContent()); ;
            this.Type = createPropertyViews[2].getPropertyContent();
            this.Speed = int.Parse(createPropertyViews[3].getPropertyContent());
            this.Lag = createPropertyViews[4].getPropertyContent();

            createPropertyViews.RemoveRange(0, createPropertyViews.Count);

        }

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            createPropertyViews.Add(new CreatePropertyView("Pojemność całkowita", "GB"));
            createPropertyViews.Add(new CreatePropertyView("Liczba modułów"));
            createPropertyViews.Add(new CreatePropertyView("Rodzaj pamięci"));
            createPropertyViews.Add(new CreatePropertyView("Taktowanie", "mHz"));
            createPropertyViews.Add(new CreatePropertyView("Opóźnienie"));

            for (int i = 0; i < createPropertyViews.Count; i++)
            {
                if (i == 2 || i == 4)
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
            itemView.addNewLbl("Pojemność całkowita: " + this.Size + "GB");
            itemView.addNewLbl("Liczba modułów: " + NumberOfModules);
            itemView.addNewLbl("Rodzaj pamięci: " + this.Type);
            itemView.addNewLbl("Taktowanie: " + this.Speed + "mHz");
            itemView.addNewLbl("Opóźnienie: " + this.Lag );
            base.addPropertiesToView(itemView);
        }

    }
}
