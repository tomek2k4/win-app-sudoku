using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Sudoku.ViewModel
{
    public class PoleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (Pole)value;
            switch (val)
            {
                case Pole.Puste:
                    return " ";
                case Pole.Kolko:
                    return "O";
                case Pole.Krzyzyk:
                    return "X";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
