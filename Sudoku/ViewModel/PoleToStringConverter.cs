using Sudoku.Model;
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
            var val = (CellClass)value;

            if (val != null)
            {
                switch(val.CellState)
                {
                    case CellStateEnum.Blank:
                        return " ";
                    case CellStateEnum.UserInputCorrect:
                    case CellStateEnum.UserInputIncorrect:
                        return val.UserAnswer.ToString();
                    case CellStateEnum.Answer:
                        return val.Answer.ToString();
                    default:
                        return "0";
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
