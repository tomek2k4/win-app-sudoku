




using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Sudoku.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        private Pole[][] m_Plansza = new Pole[][]
        {
            new Pole[3],
            new Pole[3],
            new Pole[3],
        };

        public Pole[][] Plansza
        {
            get
            {
                return m_Plansza;
            }
        }

        private RelayCommand[][] m_Komendy = new RelayCommand[3][];

        public RelayCommand[][] Komendy
        {
            get
            {
                return m_Komendy;
            }
        }

        private Pole nextSymbol = Pole.Krzyzyk;

        private Pole GetNextSymbol()
        {
            var x = nextSymbol;
            nextSymbol = (nextSymbol == Pole.Krzyzyk ? Pole.Kolko : Pole.Krzyzyk);
            return x;
        }

        public ViewModelClass()
        {
            for (int i = 0; i < 3; ++i)
            {
                m_Komendy[i] = new RelayCommand[3];
                for (int j = 0; j < 3; ++j)
                {
                    var x = i;
                    var y = j;
                    m_Komendy[i][j] = new RelayCommand(
                        () => { m_Plansza[x][y] = GetNextSymbol(); FirePropertyChanged("Plansza"); },
                        () => { return m_Plansza[x][y] == Pole.Puste; }
                        );
                }
            }
        }

        private void FirePropertyChanged(string name)
        {
            var _event = PropertyChanged;
            if( _event != null)
            {
                _event(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
