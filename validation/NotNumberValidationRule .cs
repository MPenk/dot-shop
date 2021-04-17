using System;
using System.Globalization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Sprawdzenie czy obiekt jest numerem.
    /// </summary>
    public class NotNumberValidationRule : ValidationRule
    {
        /// <summary>
        /// Sprawdzenie czy obiekt jest numerem.
        /// </summary>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Trim(' ') == "")
            {
                return new ValidationResult(false, "Pole jest wymagane");
            }
            try
            {
                int.Parse(value.ToString());
                return ValidationResult.ValidResult;
            }
            catch (Exception)
            {

                return new ValidationResult(false, "To musi być liczba naturalna");
            }            
        }
    }
}
