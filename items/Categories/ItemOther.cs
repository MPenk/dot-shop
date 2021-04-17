using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Klasa do obsług innych przedmiotów.
    /// </summary>
    [DataContract(Name = "ItemsOrther", IsReference = true)]
    class ItemOther : Item, IViewItemStandard
    {

        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public ItemOther() { }

        /// <summary>
        /// Konstruktor z parametrami.
        /// </summary>
        public ItemOther(string name, string model, string category, string manufacturer, string imgUrl) : base(name, model, category.Replace(' ', '_'), manufacturer, imgUrl)
        {
        }


        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Dodanie właściwości do widoku
        /// </summary>
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews)
        {
            //throw new NotImplementedException();
        }
    }
}
