using System.Collections.Generic;
using System.Windows.Controls;

namespace dot_shop
{
    interface IViewItemStandard
    {

        /// <summary>
        /// Wyświetlenie właściwości kategorii.
        /// </summary
        public void ShowPropertyCategory(ref StackPanel stackPanel, ref List<CreatePropertyView> createPropertyViews);

        /// <summary>
        /// Ustawienie właściwości przedmiotu.
        /// </summary>
        public void SetPropertyToItem(List<CreatePropertyView> createPropertyViews);
        
    }
}
