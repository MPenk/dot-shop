using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace dot_shop
{
    /// <summary>
    /// Klasa od obsługi plików.
    /// </summary>
    class FileOperations
    {

        /// <summary>
        /// Zapisanie wszyskich przedmiotów oraz ich relacji do pliku .xml
        /// </summary>
        /// <param name="values">Lista kategorii przedmiotów.</param>
        public static void SaveDataToXmlFile<ItemCategory>(List<ItemCategory> values, string fullPathXML)
        {
            _ = Task.Run(() =>
            {

                var ds = new DataContractSerializer(typeof(List<ItemCategory>));
                var settings = new XmlWriterSettings { Indent = true };
                Directory.CreateDirectory(fullPathXML);
                Directory.CreateDirectory(fullPathXML + "\\data");
                using (var w = XmlWriter.Create(fullPathXML + "\\data\\data.xml", settings))
                    ds.WriteObject(w, values);

            });
        }

        /// <summary>
        /// Wczytanie wszykich przedmiotów oraz ich relacji z pliku .xml
        /// </summary>
        //TODO: Stworzyć osobą klasę do obsługi plików
        public static async Task<List<ItemCategory>> LoadDataFromXmlFileAsync(string fullPathXML)
        {
            return await Task.Run(() =>
            {
                if (File.Exists(fullPathXML + "\\data\\data.xml"))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(List<ItemCategory>));
                    using (FileStream stream = File.Open(fullPathXML + "\\data\\data.xml", FileMode.Open))
                    {
                        return serializer.ReadObject(stream) as List<ItemCategory>;
                    }
                }
                else
                    return null;
            });
        }

        /// <summary>
        /// Załadowanie podstawowych przedmiotów.
        /// </summary>
        public static void LoadDefault() 
        {
                DefaultItems.createDefaultItems();
        }

    }
}
