using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dot_shop
{
    /// <summary>
    /// Logika interakcji dla klasy ItemView.xaml
    /// <para>Umożliwia utworzenie witoku przedmiotu klasy <c>Item</c>.</para>
    /// </summary>
    public partial class ItemView : UserControl
    {
        /// <summary>
        /// Lista labeli.
        /// </summary>
        List<Label> labels = new List<Label>();

        /// <summary>
        /// Konstruktor <c>ItemView</c>, umożliwia utowrzenie widoku przemiotu.
        /// </summary>
        /// <param name="item">Przedmiot do wyświetlenia.</param>
        public ItemView(Item item)
        {
            InitializeComponent();
            //this.item = item;
        }

        /// <summary>
        /// Ustawienie nazwy przedmiotu.
        /// </summary>
        /// <param name="name">Nazwa przedmiotu do wyświetlenia</param>
        public void setLblName(string name) {
            lblItemName.Content = name;
        }

        /// <summary>
        /// Utworzenia nowego obiektu typu <c>Label</c> opowiedzialnego za wyświetlanie właściwości przedmiotu.
        /// </summary>
        /// <param name="text">Zawartość do wyświetlenia</param>
        public void addNewLbl(string text)
        {
            Label newLbl = new Label();
            newLbl.Content = text;
            newLbl.HorizontalAlignment = HorizontalAlignment.Stretch;
            newLbl.VerticalAlignment = VerticalAlignment.Stretch;
            labels.Add(newLbl);
        }

        /// <summary>
        /// Aktualizacja widoku.
        /// </summary>
        public void updateWrap() {
            wrapP.Children.Clear();
            foreach (var newLbl in labels)
            {
                wrapP.Children.Add(newLbl);

            }

        }
    }
}
