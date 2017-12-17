using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//https://stackoverflow.com/questions/34713126/stringformat-on-binding
namespace WishList.Models
{
    public class StringToPriceFormatter
    {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                if (value == null)
                    return null;

                Double price = Double.Parse(value.ToString());
                return price.ToString("€");
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        
    }
}
