using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Logika interakcji dla klasy CreateItem.xaml
    /// </summary>
    public partial class CreateItem : Page
    {
        /// <summary>
        /// Lista <c>CreatePropertyView</c> z widokami tworzenia nowej właściwości <c>ItemProperty</c>.
        /// </summary>
        List<CreatePropertyView> createPropertyViews = new List<CreatePropertyView>();

        /// <summary>
        /// Przedmiot.
        /// </summary>
        Item item;

        /// <summary>
        /// Event po zamknięciu kreatora.
        /// </summary>
        public event EventHandler CloseItemCreator;

        /// <summary>
        /// Pusty konstruktor.
        /// </summary>
        public CreateItem()
        {
            InitializeComponent();
            refreshContent();
        }

        /// <summary>
        /// Utworzenie przedmiotu po wybraniu kategorii.
        /// </summary>
        private void createItemCategory(ComboBox cbox) 
        {
            switch (cbox.SelectedItem.ToString())
            {
                case "CPU":
                    this.item = new ItemCPU();
                    break;
                case "RAM":
                    this.item = new ItemRAM();
                    break;
                case "GPU":
                    this.item = new ItemGPU();
                    break;
                case "PowerSupply":
                    this.item = new ItemPowerSupply();
                    break;
                case "Motherboard":
                    this.item = new ItemMotherboard();
                    break;
                case "Drive":
                    this.item = new ItemDrive();
                    break;
                default:
                    this.item = new ItemOther();
                    break;
            }
        }

        /// <summary>
        /// Odświeżenie ComboBoxów.
        /// </summary>
        public void refreshContent()
        {
            cboxCategory.Items.Clear();
            foreach (var item in ItemCategory.itemCategories)
                cboxCategory.Items.Add(item.categoryName);
            cboxCategory.Items.Add("Dodaj nową kategorię...");
            cboxCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Obsługa przycisku odpowiedzialnego za dodanie nowego przedmiotu
        /// </summary>
        private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (tbxNewItemName.Text.Trim(' ') != "" && tbxNewItemModel.Text.Trim(' ') != "" && tbxNewItemManufacturer.Text.Trim(' ') != "")
            {
                bool PropertyCorrect = true;
                for (int i = 0; i < createPropertyViews.Count - 1; i++)
                {
                    if (createPropertyViews[i].ValidatinData() != CreatePropertyView.HeldData.AllData)
                    {
                        PropertyCorrect = false;
                    }
                }
                if (PropertyCorrect == true)
                {
                    if (cboxCategory.SelectedIndex == (cboxCategory.Items.Count - 1))
                    {
                        if (tbxNewItemCategoryName.Text.Trim(' ') != "")
                            CreateNewItem();
                    }
                    else
                    {
                        tbxNewItemCategoryName.Text = (string)cboxCategory.SelectedItem;
                        CreateNewItem();
                    }
                }

            }
        }

        /// <summary>
        /// Stworzenie nowego przedmiotu.
        /// </summary>
        private void CreateNewItem()
        {
            try
            {
                ((IViewItemStandard)item).SetPropertyToItem(createPropertyViews);

                if (tbxNewItemUrl.Text.Trim(' ').Length < 10)
                {
                    if (tbxNewItemUrl.Text.Trim(' ').Length > 6)
                    {
                        if (tbxNewItemUrl.Text.Trim(' ').Substring(0, 8) != "https://" || tbxNewItemUrl.Text.Trim(' ').Substring(0, 7) != "http://")
                            tbxNewItemUrl.Text = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtw-d17eeatE2c7ZeVbW1FX9zw0WoD4DQ3GA&usqp=CAU";
                    }
                    else
                        tbxNewItemUrl.Text = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTtw-d17eeatE2c7ZeVbW1FX9zw0WoD4DQ3GA&usqp=CAU";
                }
                item.setDefaultInformation(tbxNewItemName.Text, tbxNewItemModel.Text, tbxNewItemCategoryName.Text, tbxNewItemManufacturer.Text, tbxNewItemUrl.Text);
                
                CloseWindow();
            }
            catch (Exception)
            {
                //MessageBox.Show("Tworzenie przedmiotu ERRROR");
            }
        }

        /// <summary>
        /// Akcja po wybraniu kategorii.
        /// </summary>
        private void cboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbox = (ComboBox)sender;
            if (cbox.SelectedIndex == cbox.Items.Count - 1)
            {
                gridNewItemCategoryName.Visibility = Visibility.Visible;
                tbxNewItemCategoryName.Visibility = Visibility.Visible;
            }
            else
            {
                gridNewItemCategoryName.Visibility = Visibility.Collapsed;
                tbxNewItemCategoryName.Visibility = Visibility.Hidden;
            }
            sPanelProperties.Children.Clear();
            createPropertyViews.Clear();

            try
            {

                if (cbox.SelectedItem != null)
                {

                    createItemCategory(cbox);
                    ((IViewItemStandard)item).ShowPropertyCategory(ref sPanelProperties, ref createPropertyViews);

                }

            }
            catch (Exception)
            {
                //MessageBox.Show("Błąd podczas zmiany kategorii");
            }
            finally
            {
                AddNextCreatePropertyView(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// Akcja do utworzenia nowego widoku właściwości.
        /// </summary>
        public void AddNextCreatePropertyView(object sender, EventArgs e)
        {
            CreatePropertyView createPropertyView = new CreatePropertyView();
            createPropertyView.AddNextCreatePropertyView += AddNextCreatePropertyView;
            createPropertyView.RemoveNewPropertyView += RemoveNewPropertyView;
            this.createPropertyViews.Add(createPropertyView);
            sPanelProperties.Children.Add(createPropertyView);
        }

        /// <summary>
        /// Akcja do usunięciu ostatniego widoku właściwości.
        /// </summary>
        public void RemoveNewPropertyView(object sender, EventArgs e)
        {
            if (((CreatePropertyView)sender) != createPropertyViews[createPropertyViews.Count - 1])
            {
                sPanelProperties.Children.RemoveAt(createPropertyViews.Count - 1);
                createPropertyViews.RemoveAt(createPropertyViews.Count - 1);
            }
        }

        /// <summary>
        /// Zamknięcie okna kratora.
        /// </summary>
        private void CloseWindow()
        {
            if (CloseItemCreator != null)
            {
                CloseItemCreator(this, EventArgs.Empty);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
    }
}
