using System;
using System.Globalization;
using System.Windows.Controls;

namespace dot_shop
{
    /// <summary>
    /// Sprawdzenie czy obiekt jest pusty.
    /// </summary>
    public class NotEmptyValidationRule : ValidationRule
    {
        /// <summary>
        /// Sprawdzenie czy obiekt jest pusty.
        /// </summary>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString().Trim(' ') == "")
            {
                return new ValidationResult(false, "Pole jest wymagane");
            }
            return ValidationResult.ValidResult;
        }
    }
}
