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

namespace dot_shop.userControls
{
    /// <summary>
    /// Logika interakcji dla klasy AdView.xaml
    /// </summary>
    public partial class AdView : UserControl
    {
        /// <summary>
        /// Konstruktor widoku reklamy.
        /// </summary>
        public AdView(string name)
        {
            InitializeComponent();
            lblName.Content = name;
        }
    }
}
