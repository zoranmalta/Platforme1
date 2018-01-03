using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Platforme.util
{
    public class StringValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String v = value as string;
            if (String.IsNullOrEmpty(v))
            {
                return new ValidationResult(false, "Neispravan unos");
            }
            else
                return new ValidationResult(true, null);
        }
    }
}
