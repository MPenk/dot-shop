using System.Runtime.Serialization;

namespace dot_shop
{
    /// <summary>
    /// Klasa przechowująca dodatkowe właściwości przedmiotów.
    /// </summary>
    [DataContract(Name = "ItemsProperties", IsReference = true)]
    public class ItemProperty
    {
        /// <summary>
        /// Nazwa właściwości.
        /// </summary>
        [DataMember]
        string propertyName { get; set; }

        /// <summary>
        /// Zawartość właściwości.
        /// </summary>
        [DataMember]
        string propertyContent { get; set; }

        /// <summary>
        /// Konstruktor, wymaga podania właściwości oraz zawartości.
        /// </summary>
        /// <param name="propertyContent">Zawartość właściwości</param>
        /// <param name="propertyName">Nazwa właściwości</param>
        public ItemProperty(string propertyName, string propertyContent)
        {
            this.propertyName = propertyName;
            this.propertyContent = propertyContent;
        }

        /// <summary>
        /// Pobranie nazwy właściwości.
        /// </summary>
        /// <returns>Nazwa właściwości jako <c>string</c></returns>
        public string GetPropertyName()
        {
            return this.propertyName;
        }

        /// <summary>
        /// Pobranie zawartości właściwości.
        /// </summary>
        /// <returns>Zawartość właściwości jako <c>string</c></returns>
        public string GetPropertyContent()
        {
            return this.propertyContent;
        }
    }
}
