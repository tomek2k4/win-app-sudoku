using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


using Sudoku.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sudoku
{

    

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private ViewModelClass _viewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        #region . Properties: Public .

        /// <summary>
        /// Gets or sets the ViewModel pointer for this class.
        /// </summary>
        internal ViewModelClass ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;                             // Save a pointer to the ViewModel class
                this.DataContext = value;                       // Set the datacontext of the WPF form to the view model.
            }
        }

        #endregion



    }





}
