




using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        #region . Variables, Constants, And other Declarations .

        #region . Variables .


        #endregion

        #region . Other Declarations .

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion

        #region . Constructors .
        // Declared private so no one else can.
        private ViewModelClass()
        { }

        #endregion

        #region . Methods .

        #region . Interface Implementation .

        // This routine is normally called from the Set accessor of each property
        // that is bound to the a WPF control.  We use the [CallMemberName] attribute
        // so that the property name of the caller will be substituted as the argument.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #endregion

    }
}
