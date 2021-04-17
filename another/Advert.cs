using dot_shop.userControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace dot_shop
{
    /// <summary>
    /// Klasa reklamy.
    /// </summary>
    class Advert : BrowseInfo
    {
        /// <summary>
        /// Dodanie widoku reklamy.
        /// </summary>
        public AdView adView;

        /// <summary>
        /// Ilość reklam.
        /// </summary>
        public static int adCount = 9;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Advert(string name, string imgUrl) : base(name, imgUrl)
        {
            adView = new AdView(name);
            _ = LoadImage();
        }

        /// <summary>
        /// Załadowanie zdjęcia.
        /// </summary>
        private async Task LoadImage() 
        {
            this.fullFilePath = this.fullFilePath.Substring(0, this.fullFilePath.Length - 40) + name + ".jpg";
            if (File.Exists(fullFilePath))
                adView.imgAdPrev.Source = await LoadImageAsync(bitmap, this.fullFilePath);
            else
                _ = GetNewImageAsync(adView, bitmap, imgUrl, fullFilePath);
        }

        /// <summary>
        /// Generowanie reklamy.
        /// </summary>
        public static Advert GenerateAd(int number) 
        {
            Advert adView;
            switch (number)
            {
                case 0:
                    adView = new Advert("Cola", "http://penk.eu/download/ad/cola.jpg");
                    break;
                case 1:
                    adView = new Advert("KFC", "http://penk.eu/download/ad/kfc.jpg");
                    break;
                case 2:
                    adView = new Advert("Doritos", "http://penk.eu/download/ad/doritos.jpg");
                    break;
                case 3:
                    adView = new Advert("Chipsy", "http://penk.eu/download/ad/chipsy.jpg");
                    break;
                case 4:
                    adView = new Advert("Huawei", "http://penk.eu/download/ad/huawei.jpg");
                    break;
                case 5:
                    adView = new Advert("Ketchup", "http://penk.eu/download/ad/ketchup.jpg");
                    break;
                case 6:
                    adView = new Advert("McDonald", "http://penk.eu/download/ad/mcdonald.jpg");
                    break;
                case 7:
                    adView = new Advert("Michelin", "http://penk.eu/download/ad/michelin.jpg");
                    break;
                case 8:
                    adView = new Advert("UMG", "http://penk.eu/download/ad/umg.jpg");
                    break;
                default:
                    adView = null;
                    break;

            }
            return adView;
            
        }
    }
}
