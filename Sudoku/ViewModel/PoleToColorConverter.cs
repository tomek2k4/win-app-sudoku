using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Sudoku.ViewModel
{
    class PoleToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (CellClass)value;

            if (val != null)
            {
                switch(val.CellState)
                {
                    case CellStateEnum.Blank:
                        return "Black";
                    case CellStateEnum.UserInputCorrect:
                    case CellStateEnum.UserInputIncorrect:
                        return "Black";
                    case CellStateEnum.Answer:
                        return "Brown";
                    default:
                        return "Black";
                }
            }
            else
            {
                return "0";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
