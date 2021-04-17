using System;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Logika interakcji dla klasy CreateropertyView.xaml
    /// </summary>
    public partial class CreatePropertyView : UserControl
    {
        /// <summary>
        /// Nazwa własciwości.
        /// </summary>
        private string propertyName;

        /// <summary>
        /// Zawartość własciwości.
        /// </summary>
        private string propertyContent;

        /// <summary>
        /// Event po utworzeniu nowego widoku właściwości.
        /// </summary>
        public event EventHandler AddNextCreatePropertyView;

        /// <summary>
        /// Event po usunięciu nowgego widoku właściwości .
        /// </summary>
        public event EventHandler RemoveNewPropertyView;

        /// <summary>
        /// Ostatnie wypełnienie danych.
        /// </summary>
        private HeldData lastHeldData;

        /// <summary>
        /// Konstruktor pusty..
        /// </summary>
        public CreatePropertyView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor pobierający tylko nazwę.
        /// </summary>
        public CreatePropertyView(string propertyName)
        {
            InitializeComponent();

            tboxPropertyName.Text = propertyName;
            tboxPropertyName.IsEnabled = false;
        }

        /// <summary>
        /// Konstruktor pobierający nazwę i wielkość.
        /// </summary>
        public CreatePropertyView(string propertyName, string unit)
        {
            InitializeComponent();

            tboxPropertyName.Text = propertyName;
            tboxPropertyName.IsEnabled = false;
            lblPropertyContentUnit.Content = unit;
        }

        /// <summary>
        /// Zwrócenie zwartości wielkości.
        /// </summary>
        public string getPropertyContent() 
        {
            return this.propertyContent;
        }

        /// <summary>
        /// Enum wypełnienia danych.
        /// </summary>
        public enum HeldData
        {
            NoData, OnlyContent, OnlyName, AllData
        }

        /// <summary>
        /// Sprawdzenie poprawności danych w kontrolce (czy nie są puste)
        /// </summary>
        public HeldData ValidatinData()
        {
            propertyContent = (this.tboxPropertyContent.Text).Trim(' ');
            propertyName = (this.tboxPropertyName.Text).Trim(' ');
            if (propertyName != "" && propertyContent != "")
            {
                return HeldData.AllData;
            }
            if (propertyName != "")
            { 
                return HeldData.OnlyName;
            }
            if (propertyContent != "")
            {
                return HeldData.OnlyContent;
            }
            return HeldData.NoData;
        }

        /// <summary>
        /// Dodanie sprawdzenia pustej zawartości właściwości.
        /// </summary>
        public void addValidationEmpty() 
        {
            BindPropertyContentEmpty.ValidatesOnTargetUpdated = true;
            tboxPropertyContent.Text = ".";
            tboxPropertyContent.Text = "";
        }

        /// <summary>
        /// Dodanie sprawdzenia pustej/nie numerycznej zawartości właściwości.
        /// </summary>
        public void addValidationNumberAndEmpty()
        {
            BindPropertyContentNumber.ValidatesOnTargetUpdated = true;
            tboxPropertyContent.Text = ".";
            tboxPropertyContent.Text = "";
        }

        /// <summary>
        /// Tworzy odpowiednie wydarzenia po zmianie tekstu w kontrolkach typu <c>TextBox</c>.
        /// </summary>
        private void tboxProperty_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (ValidatinData()==HeldData.AllData && lastHeldData!= HeldData.AllData)
            {
                if (AddNextCreatePropertyView != null)
                {
                    AddNextCreatePropertyView(this, EventArgs.Empty);
                    lastHeldData = HeldData.AllData;
                }
            }
            else
            {
                if (AddNextCreatePropertyView != null && lastHeldData == HeldData.AllData && ValidatinData() != HeldData.AllData)
                {
                    RemoveNewPropertyView(this, EventArgs.Empty);
                    lastHeldData = ValidatinData();
                }
            }

        }
    }
}
