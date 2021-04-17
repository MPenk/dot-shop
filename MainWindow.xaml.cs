using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;

namespace dot_shop
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    [Serializable]
    public partial class MainWindow : Window
    {

        /////////////////////////////////////////////////////////
        //DANE
        /////////////////////////////////////////////////////////

        /// <summary>
        /// Lista <c>CheckBox'ów</c> z wybranymi kategoriami <c>ItemCategories</c>.
        /// </summary>
        List<CheckBox> viewedCategoryCheckBoxList = new List<CheckBox>();

        /// <summary>
        /// Lista <c>Item</c> z przedmiotami do wyświetlenia.
        /// </summary>
        List<Item> itemsToView = new List<Item>();

        /// <summary>
        /// Ilość wyświetlonych przedmiotów.
        /// </summary>
        int itemViewedCount = 0;

        /// <summary>
        /// Przesunięcie scrolla.
        /// </summary>
        int scrollValueRendered = 0;

        /// <summary>
        /// Informacja czy przedmioty dodawane są do UI
        /// </summary>
        bool isRendering = false;

        /// <summary>
        /// Ścieżka do pliku zapisu danych.
        /// </summary>
        string fullPathXML = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\dot-Kom";

        /// <summary>
        /// Randomizer.
        /// </summary>
        Random rnd = new Random();

        /// <summary>
        /// Status sortowania
        /// </summary>
        SortStatus sortStatus = SortStatus.NoSort;


        /// <summary>
        /// Enum dla statusu sortowania
        /// </summary>
        enum SortStatus
        {
            NoSort, SortowanieUp, SortowanieDown
        }

        /////////////////////////////////////////////////////////
        //Inicjalizacja
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Metoda startowa interfejsu
        /// </summary>
        public MainWindow()
        {

            InitializeComponent();
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dot-Kom"));
            //DefaultItems.createDefaultItems();
            //SerializeDataValue(ItemCategory.itemCategories);
            new ItemCategory("CPU");
            new ItemCategory("GPU");
            new ItemCategory("RAM");
            new ItemCategory("PowerSupply", "Zasilacze");
            new ItemCategory("Drive", "Dyski");
            new ItemCategory("Motherboard", "Płyty_Główne");
            _ = FileOperations.LoadDataFromXmlFileAsync(fullPathXML);
        }


        /////////////////////////////////////////////////////////
        //Osbługa kategorii
        /////////////////////////////////////////////////////////

        /// <summary>
        /// Wypisuje wszyskie kategorie z <paramref name="itemCategories"/> w <c> Checkbox </c>
        /// </summary>
        /// <param name="itemCategories">Lista kategorii</param>
        public void RefreshCategories(List<ItemCategory> itemCategories)
        {
            //Usunięcie wyświetlanych kategorii
            if (sPanelViewCategories.Children.Count > 0)
            {
                sPanelViewCategories.Children.Clear();
            }

            //Tworzenie checkBoxów
            foreach (var category in itemCategories)
            {
                CheckBox checkBox = new CheckBox();
                var badged = new MaterialDesignThemes.Wpf.Badged();
                badged.Content = checkBox;
                badged.Badge = category.getCountItemsInCategories();
                checkBox.Name = "chckBoxCategorie" + category.categoryName;
                checkBox.Style = (Style)FindResource("MaterialDesignFilterChipPrimaryCheckBox");
                checkBox.Content = category.categoryViewedName;
                checkBox.Margin = new Thickness(0, 2, 0, 2);
                checkBox.VerticalContentAlignment = VerticalAlignment.Top;
                checkBox.Click += ChckBoxFilter_Click;
                sPanelViewCategories.Children.Add(badged);
                viewedCategoryCheckBoxList.Add(checkBox);
            }
        }

        /// <summary>
        /// Aktualizuje pokazywane przedmioty po naciśnieciu <c>CheckBox</c> z wybraną kategorią.
        /// </summary>
        private void ChckBoxFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _ = ShowBrowse();
            }
            catch (Exception)
            {
                //TODO: Obsłużyć wyjątek.
            }
        }


        /////////////////////////////////////////////////////////
        //Osbługa wyświetlanych przedmiotów
        /////////////////////////////////////////////////////////

        /// <summary>
        /// Usuwa wszyskie rekordy z listy kategorii <c>ItemCategory.itemCategories</c>
        /// </summary>
        private void RemoveItemsViewed()
        {
            ItemCategory.itemCategories.Clear();
            viewedCategoryCheckBoxList.Clear();
            stackPList.Children.Clear();
        }

        /// <summary>
        /// Pokazuje pierwsze 5 przedmiotów z wybranych kategorii w <c>stackPList</c>
        /// </summary>
        public async Task ShowBrowse()
        {
            sPanelViewCategories.IsEnabled = false;
            stackPList.Children.Clear();
            itemsToView.Clear();
            itemViewedCount = 0;
            scrollViewer.ScrollToVerticalOffset(0);
            
            //Dodanie przedmitów do itemToView
            foreach (var category in viewedCategoryCheckBoxList)
            {
                if (category.IsChecked == true)
                {
                    ItemCategory itemCategory = ItemCategory.itemCategories.Find(s =>
                    {
                        return s.categoryName.Equals(category.Name.Substring(16));
                    });
                    if (itemCategory != null)
                    {
                        foreach (var item in itemCategory.GetItems())
                        {
                            itemsToView.Add(item);
                        }
                    }
                }
            }
            SortItemToView();
            scrollValueRendered = 0;
            this.lblItemViewedCount.Content = "Wyświelane: " + itemsToView.Count;
            int toShow = 8;
            if (itemsToView.Count < toShow)
            {
                if (itemsToView.Count==0)
                {
                    scrollViewer.ScrollToVerticalOffset(0);
                }
                toShow = itemsToView.Count;
            }

            //Pokazanie pierwszych 8 lub mniej przedmiotów
            for (int i = 0; i < toShow; i++)
            {
                try
                {
                    ItemView itemViews = await itemsToView[i].ConfigureAndGetItemView();
                    await Task.Delay(10);
                    stackPList.Children.Add(itemViews);
                    if ((i + 1) % 5 == 0)
                        ShowAd();
                }
                catch (Exception exc)
                {
                    //TODO: Stworzyć wyjątek
                    MessageBox.Show("TO NIE POWINNO SIĘ POJAWIĆ: " + exc.Message);
                }

            }
            itemViewedCount += toShow;
            sPanelViewCategories.IsEnabled = true;


        }

        /// <summary>
        /// Sortowanie przedmiotów do wyświetlenia.
        /// </summary>
        private void SortItemToView() 
        {
            switch (sortStatus)
            {
                case SortStatus.NoSort:
                    break;
                case SortStatus.SortowanieUp:
                    itemsToView.Sort();
                    break;
                case SortStatus.SortowanieDown:
                    itemsToView.Sort(new ItemCompareNameDown());
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Wyświetlenie reklamy.
        /// </summary>
        public void ShowAd() 
        { 
           stackPList.Children.Add(Advert.GenerateAd(rnd.Next(0, Advert.adCount)).adView);
        }


        /////////////////////////////////////////////////////////
        //Osbługa przycisków do plików XML
        /////////////////////////////////////////////////////////
        //TODO: USUNĄĆ! Dodać automatyczne wczytywanie pliku gdy jest dostępny na komputerze.
        //TODO: USUNĄĆ! Dodać automatyczny zapis pliku gdy tylko zostanie dodawny nowy przedmiot

        /// <summary>
        /// Obsługa przycisku odpowidzialnego za wczytywanie pliku z danymi.
        /// </summary>
        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemsViewed();
            var itemCopy = await FileOperations.LoadDataFromXmlFileAsync(this.fullPathXML);
            if(itemCopy!= null)
                ItemCategory.itemCategories = itemCopy;
            RefreshCategories(ItemCategory.itemCategories);
        }

        /// <summary>
        /// Obsługa przycisku odpowidzialnego za zapisywanie pliku z danymi.
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FileOperations.SaveDataToXmlFile(ItemCategory.itemCategories, this.fullPathXML);
        }

        /// <summary>
        /// Obsługa przycisku odpowidzialnego za usuwanie przedmiotów.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            RemoveItemsViewed();
            RefreshCategories(ItemCategory.itemCategories);
        }

        /// <summary>
        /// Obsługa przycisku odpowidzialnego za dodanie podstawowych przedmiotów.
        /// </summary>
        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            FileOperations.LoadDefault();
            viewedCategoryCheckBoxList.Clear();
            RefreshCategories(ItemCategory.itemCategories);
            _ = ShowBrowse();
        }


        /////////////////////////////////////////////////////////
        //Osbługa dodawania przedmiotów
        /////////////////////////////////////////////////////////
        /// <summary>
        /// Obsługa przycisku odpowidzialnego za pokazanie okna do dodawania nowego przedmiotu.
        /// </summary>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var pageCreateItem = new CreateItem();
            dialogHostCreateItem.Content = null;
            dialogHostCreateItem.NavigationService.RemoveBackEntry();
            dialogHostCreateItem.Content = pageCreateItem;
            pageCreateItem.CloseItemCreator += CloseItemCreator;
        }

        /// <summary>
        /// Odświeżenie kategorii po dodaniu przedmiotu.
        /// </summary>
        private void CloseItemCreator(object sender, EventArgs e)
        {
            viewedCategoryCheckBoxList.Clear();
            stackPList.Children.Clear();
            RefreshCategories(ItemCategory.itemCategories);

        }


        /////////////////////////////////////////////////////////
        //Osbługa podstawowego interfejsu
        /////////////////////////////////////////////////////////

        /// <summary>
        /// Zmiana wielkości okna dodawania  przedmiotu
        /// </summary>
        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dialogHostCreateItem.Width = window.ActualWidth * 0.75;
            dialogHostCreateItem.Height = window.ActualHeight * 0.75;
        }

        /// <summary>
        /// Obsługa przesuwania okna.
        /// </summary>
        private void gridTopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        /// <summary>
        /// Obsługa zamknięcia okna.
        /// </summary>
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        /// <summary>
        /// Obsługa zminimalizowania okna
        /// </summary>
        private void ButtonMinimalize_Click(object sender, RoutedEventArgs e)
        {
            window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Obsługa zminimalizowania okna
        /// </summary>
        private void gridTopBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
            else
                window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Obsługa przełączenia na pełny ekran.
        /// </summary>
        private void ButtonFull_Click(object sender, RoutedEventArgs e)
        {
            if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
            else
                window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// Obsługa przełączenia sortowania.
        /// </summary>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            switch (sortStatus)
            {
                case SortStatus.NoSort:
                    sortStatus = SortStatus.SortowanieUp;
                    this.iconSort.Kind = MaterialDesignThemes.Wpf.PackIconKind.SortAscending;
                    break;
                case SortStatus.SortowanieUp:
                    sortStatus = SortStatus.SortowanieDown;
                    this.iconSort.Kind = MaterialDesignThemes.Wpf.PackIconKind.SortDescending;
                    break;
                case SortStatus.SortowanieDown:
                    sortStatus = SortStatus.NoSort;
                    this.iconSort.Kind = MaterialDesignThemes.Wpf.PackIconKind.Sort;
                    break;
                default:
                    break;
            }
            _ = ShowBrowse();
        }




        /////////////////////////////////////////////////////////
        //Osbługa scrolla
        /////////////////////////////////////////////////////////
        
        /// <summary>
        /// Zmienna do przesuwania muszką.
        /// </summary>
        protected Point scrollMousePoint = new Point();
        /// <summary>
        /// Zmienna do przesuwania muszką.
        /// </summary>
        protected double vOff = 1;

        /// <summary>
        /// Obsługuje przesuwanie myszką.
        /// </summary>
        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + e.Delta);
        }

        /// <summary>
        /// Obsługuje przesuwanie myszką.
        /// </summary>
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollViewer);
            vOff = scrollViewer.VerticalOffset;
            scrollViewer.CaptureMouse();
        }

        /// <summary>
        /// Obsługuje przesuwanie myszką.
        /// </summary>
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToVerticalOffset(vOff + (scrollMousePoint.Y - e.GetPosition(scrollViewer).Y));
            }
        }

        /// <summary>
        /// Obsługuje przesuwanie myszką.
        /// </summary>
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        /// <summary>
        /// Obsługuje scrolla. Po zjechaniu odpowiedziednio w dół załadowuje kolejne 3 obiekty do <c>stackPList</c>
        /// </summary>
        private async void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (isRendering)
                return;
            if (scrollViewer.VerticalOffset - scrollValueRendered > 300)
            {
                int difference = itemsToView.Count - itemViewedCount;
                if (difference > 0)
                {
                    isRendering = true;
                    scrollValueRendered += 300;
                    int j = 5;
                    if (difference < j)
                    {
                        j = difference;
                    }
                    for (int i = itemViewedCount; i < itemViewedCount + j; i++)
                    {
                        
                        
                        try
                        {
                            if (i % j == 0) 
                                ShowAd();
                            ItemView itemViews = await itemsToView[i].ConfigureAndGetItemView();
                            stackPList.Children.Add(itemViews);
                            
                        }
                        catch (Exception)
                        {
                            //TODO: Stworzyć wyjątek
                            //MessageBox.Show("TO NIGDY NIE POWINNO SIĘ POJAWIĆ: " + exc.Message);
                        }
                    }
                    itemViewedCount+= j;
                    isRendering = false;
                }
            }
        }

    }
}
