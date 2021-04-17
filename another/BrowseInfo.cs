using dot_shop.userControls;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace dot_shop
{
    /// <summary>
    /// Abstrakcyjna klasa, standaryzująca klasy gotowe do wyświetlenia przedmiotów w aplikacji.
    /// </summary>
    [DataContract(Name = "BrowseInfo", IsReference = true)]
    public abstract class BrowseInfo : IDisposable
    {
        /// <summary>
        /// Nazwa przedmiotu.
        /// </summary>
        [DataMember]
        protected string name { get; set; }

        /// <summary>
        /// Zdjęcie przedmiotu.
        /// </summary>
        protected BitmapImage bitmap;

        /// <summary>
        /// Pełna ścieżka do zdjęcia.
        /// </summary>
        [DataMember]
        protected string fullFilePath;

        /// <summary>
        /// Pełny adres URL do zdjęcia przedmiotu.
        /// </summary>
        [DataMember]
        protected string imgUrl;


        /// <summary>
        /// Konstruktor klasy <c>BrowseInfo</c>. Musi zawierać: <paramref name="name"/>, <paramref name="model"/>, <paramref name="manufacturer"/> oraz <paramref name="imgUrl"/>
        /// </summary>
        /// <param name="name">Nazwa przedmiotu</param>
        /// <param name="model">Model przedmiotu</param>
        /// <param name="manufacturer">Producent przedmiotu</param>
        /// <param name="imgUrl">URL zdjęcia przedmiotu</param>
        public BrowseInfo(string name, string imgUrl)
        {
            this.name = name;
            this.imgUrl = imgUrl;
            Guid photoID = System.Guid.NewGuid();
            this.fullFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\dot-Kom\\img\\" + photoID.ToString() + ".jpg";
        }
        public BrowseInfo() { }

        /// <summary>
        /// Asynchroniczne pobieranie nowego zdjęcia i zapisywanie go w elemencie <c>ItemView</c>.
        /// <para>Pobiera zdjęcie z sieci, następnie zaspisuje na dysku by ostatecznie otworzyć je i wyświetlić.</para>
        /// </summary>
        /// <param name="itemView">Obiekt do przekazania pobranego i zapisanego zdjęcia.</param>
        /// <param name="bitmapImage">Zmienna przpisana do obiektu <c>Item</c>, przypisująca dane zdjęcie do biektu.</param>
        /// <param name="imgFullUrl">Zmienna przetrzymująca pełny adres URL, potrzebny do popbrania zdjęcia.</param>
        /// <param name="fileFullPath">Zmienna przetrzymująca pełną ścieżkę do zdjęcia. Umożliwia zapisanie oraz odczytanie z dysku.</param>
        /// <returns>Obiekt klasy <c>ItemView</c></returns>
        static public async Task GetNewImageAsync(ItemView itemView, BitmapImage bitmapImage, string imgFullUrl, string fileFullPath)
        {
           itemView.imgItemPrev.Source =  await SaveImageAsync(bitmapImage, await DownloadImageAsync(imgFullUrl), fileFullPath);
        }

        static public async Task GetNewImageAsync(AdView AdView, BitmapImage bitmapImage, string imgFullUrl, string fileFullPath)
        {
            
            AdView.imgAdPrev.Source = await SaveImageAsync(bitmapImage, await DownloadImageAsync(imgFullUrl), fileFullPath);
        }
        static private async Task<BitmapImage> DownloadImageAsync(string imgFullUrl)
        {
            var bitmapDownload = new BitmapImage();
            bitmapDownload.BeginInit();
            bitmapDownload.UriSource = new Uri(imgFullUrl, UriKind.Absolute);
            bitmapDownload.EndInit();
            while (bitmapDownload.IsDownloading)
            {
                await Task.Delay(50);
            }
            bitmapDownload.Freeze();
            return bitmapDownload;
        }

        static private async Task<BitmapImage> SaveImageAsync(BitmapImage bitmapImage, BitmapImage bitmapDownload, string fileFullPath)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "dot-Kom\\img"));
            await using (var filestream = new FileStream(fileFullPath, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapDownload));
                encoder.QualityLevel = 50;
                encoder.Save(filestream);
            }
            bitmapImage = await LoadImageAsync(bitmapImage, fileFullPath);
            try
            {
                return bitmapImage;
            }
            catch (Exception)
            {
                //TODO: Dodać obsługę wyjątku!
            }
            return null;
        }


        static public async Task<BitmapImage> LoadImageAsync(BitmapImage bitmapImage, string fileFullPath)
        {
            
            await using (var filestream = new FileStream(fileFullPath, FileMode.Open))
            {
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = filestream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            try
            {
                return bitmapImage;
            }
            catch (Exception)
            {
                //TODO: Dodać obsługę wyjątku!
            }
            return null;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

