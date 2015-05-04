using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace LoginModule.ViewModels
{
    class IsIpAddress : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String address = value as string;
            if (address != null)
            {
                IPAddress a;
                if (Regex.IsMatch(address,@"\d+\.\d+\.\d+\.\d+") && IPAddress.TryParse(address, out a))
                {
                    return new ValidationResult(true, a);
                }
            }
            return new ValidationResult(false, value);
        }
    }
}
