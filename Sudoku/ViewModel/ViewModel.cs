
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

using Sudoku.Model;

namespace Sudoku.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        private CellClass[,] _cells;
        #region . Constructors .

        public ViewModelClass()
        {
            Debug.WriteLine("Initialize View Model ...");
            //_cells = new GameModel(null);
            _cells = GenerateNewBoard();

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    var x = i;
                    var y = j;
                    m_Komendy[i,j] = new RelayCommand(
                        () => {  IncrementUserAnswer( Plansza[x,y]); FirePropertyChanged("Cell"+x.ToString()+y.ToString()); },
                        () => { return Plansza[x, y].Answer == 0; }
                        );
                }
            }
        }

        #endregion

        public void IncrementUserAnswer(CellClass cell)
        {
            if(cell.UserAnswer == 9)
            {
                cell.UserAnswer = 1;
            }
            else
            {
                cell.UserAnswer++;
            }
        }



        private CellClass[,] GenerateNewBoard()
        {
            CellClass[] cellsList = new CellClass[81];
            for (Int32 i = 0; i < 81; i++)                          // Loop through the array
                cellsList[i] = null;  


            Int32 index = 0;
            do
            {
                CellClass item = new CellClass(index, 0 ); // Create a new element with answer 1
                item.UserAnswer = item.Region + 1;
                cellsList[index] = item;
                index++;
            } while (index<81);

            return TransferGameToGrid(cellsList);
        }

        private CellClass[,] TransferGameToGrid(CellClass[] cellsList)
        {
            CellClass[,] cellsArray = new CellClass[9, 9];                           // Initialize a new cell array
            foreach (CellClass item in cellsList)                                  // Loop the cell list
                cellsArray[item.Col, item.Row] = item;                               // Save the pointer
            return cellsArray;                                                       // Return the cell array
        }


        public CellClass[,] Plansza
        {
            get
            {
                return _cells;
            }
        }

        private RelayCommand[,] m_Komendy = new RelayCommand[9, 9];

        public RelayCommand[,] Komendy
        {
            get
            {
                return m_Komendy;
            }
        }




        #region . Properties: Cell Contents .

        // Property pointers to individual cells of the puzzle.

        #region . Properties: Row 1 Cells .
        public CellClass Cell00
        {
            get
            {
                return Plansza[0, 0];
            }
        }

        public CellClass Cell10
        {
            get
            {
                return Plansza[1, 0];
            }
        }

        public CellClass Cell20
        {
            get
            {
                return Plansza[2, 0];
            }
        }



        public CellClass Cell30
        {
            get
            {
                return Plansza[3, 0];
            }
        }
        public CellClass Cell40
        {
            get
            {
                return Plansza[4, 0];
            }
        }
        public CellClass Cell50
        {
            get
            {
                return Plansza[5, 0];
            }
        }
        public CellClass Cell60
        {
            get
            {
                return Plansza[6, 0];
            }
        }
        public CellClass Cell70
        {
            get
            {
                return Plansza[7, 0];
            }
        }
        public CellClass Cell80
        {
            get
            {
                return Plansza[8, 0];
            }
        }

        #endregion

        #region . Properties: Row 2 Cells .
        public CellClass Cell01
        {
            get
            {
                return Plansza[0, 1];
            }
        }

        public CellClass Cell11
        {
            get
            {
                return Plansza[1, 1];
            }
        }

        public CellClass Cell21
        {
            get
            {
                return Plansza[2, 1];
            }
        }

        public CellClass Cell31
        {
            get
            {
                return Plansza[3, 1];
            }
        }

        public CellClass Cell41
        {
            get
            {
                return Plansza[4, 1];
            }
        }

        public CellClass Cell51
        {
            get
            {
                return Plansza[5, 1];
            }
        }

        public CellClass Cell61
        {
            get
            {
                return Plansza[6, 1];
            }
        }

        public CellClass Cell71
        {
            get
            {
                return Plansza[7, 1];
            }
        }

        public CellClass Cell81
        {
            get
            {
                return Plansza[8, 1];
            }
        }

        #endregion

        #region . Properties: Row 3 Cells .
        public CellClass Cell02
        {
            get
            {
                return Plansza[0, 2];
            }
        }

        public CellClass Cell12
        {
            get
            {
                return Plansza[1, 2];
            }
        }

        public CellClass Cell22
        {
            get
            {
                return Plansza[2, 2];
            }
        }

        public CellClass Cell32
        {
            get
            {
                return Plansza[3, 2];
            }
        }

        public CellClass Cell42
        {
            get
            {
                return Plansza[4, 2];
            }
        }

        public CellClass Cell52
        {
            get
            {
                return Plansza[5, 2];
            }
        }

        public CellClass Cell62
        {
            get
            {
                return Plansza[6, 2];
            }
        }

        public CellClass Cell72
        {
            get
            {
                return Plansza[7, 2];
            }
        }

        public CellClass Cell82
        {
            get
            {
                return Plansza[8, 2];
            }
        }

        #endregion

        #region . Properties: Row 4 Cells .
        public CellClass Cell03
        {
            get
            {
                return Plansza[0, 3];
            }
        }

        public CellClass Cell13
        {
            get
            {
                return Plansza[1, 3];
            }
        }

        public CellClass Cell23
        {
            get
            {
                return Plansza[2, 3];
            }
        }

        public CellClass Cell33
        {
            get
            {
                return Plansza[3, 3];
            }
        }

        public CellClass Cell43
        {
            get
            {
                return Plansza[4, 3];
            }
        }

        public CellClass Cell53
        {
            get
            {
                return Plansza[5, 3];
            }
        }

        public CellClass Cell63
        {
            get
            {
                return Plansza[6, 3];
            }
        }

        public CellClass Cell73
        {
            get
            {
                return Plansza[7, 3];
            }
        }

        public CellClass Cell83
        {
            get
            {
                return Plansza[8, 3];
            }
        }

        #endregion

        #region . Properties: Row 5 Cells .
        public CellClass Cell04
        {
            get
            {
                return Plansza[0, 4];
            }
        }

        public CellClass Cell14
        {
            get
            {
                return Plansza[1, 4];
            }
        }

        public CellClass Cell24
        {
            get
            {
                return Plansza[2, 4];
            }
        }

        public CellClass Cell34
        {
            get
            {
                return Plansza[3, 4];
            }
        }

        public CellClass Cell44
        {
            get
            {
                return Plansza[4, 4];
            }
        }

        public CellClass Cell54
        {
            get
            {
                return Plansza[5, 4];
            }
        }

        public CellClass Cell64
        {
            get
            {
                return Plansza[6, 4];
            }
        }

        public CellClass Cell74
        {
            get
            {
                return Plansza[7, 4];
            }
        }

        public CellClass Cell84
        {
            get
            {
                return Plansza[8, 4];
            }
        }

        #endregion

        #region . Properties: Row 6 Cells .
        public CellClass Cell05
        {
            get
            {
                return Plansza[0, 5];
            }
        }

        public CellClass Cell15
        {
            get
            {
                return Plansza[1, 5];
            }
        }

        public CellClass Cell25
        {
            get
            {
                return Plansza[2, 5];
            }
        }

        public CellClass Cell35
        {
            get
            {
                return Plansza[3, 5];
            }
        }

        public CellClass Cell45
        {
            get
            {
                return Plansza[4, 5];
            }
        }

        public CellClass Cell55
        {
            get
            {
                return Plansza[5, 5];
            }
        }

        public CellClass Cell65
        {
            get
            {
                return Plansza[6, 5];
            }
        }

        public CellClass Cell75
        {
            get
            {
                return Plansza[7, 5];
            }
        }

        public CellClass Cell85
        {
            get
            {
                return Plansza[8, 5];
            }
        }

        #endregion

        #region . Properties: Row 7 Cells .
        public CellClass Cell06
        {
            get
            {
                return Plansza[0, 6];
            }
        }

        public CellClass Cell16
        {
            get
            {
                return Plansza[1, 6];
            }
        }

        public CellClass Cell26
        {
            get
            {
                return Plansza[2, 6];
            }
        }

        public CellClass Cell36
        {
            get
            {
                return Plansza[3, 6];
            }
        }

        public CellClass Cell46
        {
            get
            {
                return Plansza[4, 6];
            }
        }

        public CellClass Cell56
        {
            get
            {
                return Plansza[5, 6];
            }
        }

        public CellClass Cell66
        {
            get
            {
                return Plansza[6, 6];
            }
        }

        public CellClass Cell76
        {
            get
            {
                return Plansza[7, 6];
            }
        }

        public CellClass Cell86
        {
            get
            {
                return Plansza[8, 6];
            }
        }

        #endregion

        #region . Properties: Row 8 Cells .
        public CellClass Cell07
        {
            get
            {
                return Plansza[0, 7];
            }
        }

        public CellClass Cell17
        {
            get
            {
                return Plansza[1, 7];
            }
        }

        public CellClass Cell27
        {
            get
            {
                return Plansza[2, 7];
            }
        }

        public CellClass Cell37
        {
            get
            {
                return Plansza[3, 7];
            }
        }

        public CellClass Cell47
        {
            get
            {
                return Plansza[4, 7];
            }
        }

        public CellClass Cell57
        {
            get
            {
                return Plansza[5, 7];
            }
        }

        public CellClass Cell67
        {
            get
            {
                return Plansza[6, 7];
            }
        }

        public CellClass Cell77
        {
            get
            {
                return Plansza[7, 7];
            }
        }

        public CellClass Cell87
        {
            get
            {
                return Plansza[8, 7];
            }
        }

        #endregion

        #region . Properties: Row 9 Cells .
        public CellClass Cell08
        {
            get
            {
                return Plansza[0, 8];
            }
        }

        public CellClass Cell18
        {
            get
            {
                return Plansza[1, 8];
            }
        }

        public CellClass Cell28
        {
            get
            {
                return Plansza[2, 8];
            }
        }

        public CellClass Cell38
        {
            get
            {
                return Plansza[3, 8];
            }
        }

        public CellClass Cell48
        {
            get
            {
                return Plansza[4, 8];
            }
        }

        public CellClass Cell58
        {
            get
            {
                return Plansza[5, 8];
            }
        }

        public CellClass Cell68
        {
            get
            {
                return Plansza[6, 8];
            }
        }

        public CellClass Cell78
        {
            get
            {
                return Plansza[7, 8];
            }
        }

        public CellClass Cell88
        {
            get
            {
                return Plansza[8, 8];
            }
        }

        #endregion

        #endregion


        #region . Properties: Cell Commands .

        // Property pointers to individual cells of the puzzle.

        #region . Properties: Row 1 Cells .
        public RelayCommand CommandCell00
        {
            get
            {
                return Komendy[0, 0];
            }
        }

        public RelayCommand CommandCell10
        {
            get
            {
                return Komendy[1, 0];
            }
        }

        public RelayCommand CommandCell20
        {
            get
            {
                return Komendy[2, 0];
            }
        }



        public RelayCommand CommandCell30
        {
            get
            {
                return Komendy[3, 0];
            }
        }
        public RelayCommand CommandCell40
        {
            get
            {
                return Komendy[4, 0];
            }
        }
        public RelayCommand CommandCell50
        {
            get
            {
                return Komendy[5, 0];
            }
        }
        public RelayCommand CommandCell60
        {
            get
            {
                return Komendy[6, 0];
            }
        }
        public RelayCommand CommandCell70
        {
            get
            {
                return Komendy[7, 0];
            }
        }
        public RelayCommand CommandCell80
        {
            get
            {
                return Komendy[8, 0];
            }
        }

        #endregion

        #region . Properties: Row 2 Cells .
        public RelayCommand CommandCell01
        {
            get
            {
                return Komendy[0, 1];
            }
        }

        public RelayCommand CommandCell11
        {
            get
            {
                return Komendy[1, 1];
            }
        }

        public RelayCommand CommandCell21
        {
            get
            {
                return Komendy[2, 1];
            }
        }

        public RelayCommand CommandCell31
        {
            get
            {
                return Komendy[3, 1];
            }
        }

        public RelayCommand CommandCell41
        {
            get
            {
                return Komendy[4, 1];
            }
        }

        public RelayCommand CommandCell51
        {
            get
            {
                return Komendy[5, 1];
            }
        }

        public RelayCommand CommandCell61
        {
            get
            {
                return Komendy[6, 1];
            }
        }

        public RelayCommand CommandCell71
        {
            get
            {
                return Komendy[7, 1];
            }
        }

        public RelayCommand CommandCell81
        {
            get
            {
                return Komendy[8, 1];
            }
        }

        #endregion

        #region . Properties: Row 3 Cells .
        public RelayCommand CommandCell02
        {
            get
            {
                return Komendy[0, 2];
            }
        }

        public RelayCommand CommandCell12
        {
            get
            {
                return Komendy[1, 2];
            }
        }

        public RelayCommand CommandCell22
        {
            get
            {
                return Komendy[2, 2];
            }
        }

        public RelayCommand CommandCell32
        {
            get
            {
                return Komendy[3, 2];
            }
        }

        public RelayCommand CommandCell42
        {
            get
            {
                return Komendy[4, 2];
            }
        }

        public RelayCommand CommandCell52
        {
            get
            {
                return Komendy[5, 2];
            }
        }

        public RelayCommand CommandCell62
        {
            get
            {
                return Komendy[6, 2];
            }
        }

        public RelayCommand CommandCell72
        {
            get
            {
                return Komendy[7, 2];
            }
        }

        public RelayCommand CommandCell82
        {
            get
            {
                return Komendy[8, 2];
            }
        }

        #endregion

        #region . Properties: Row 4 Cells .
        public RelayCommand CommandCell03
        {
            get
            {
                return Komendy[0, 3];
            }
        }

        public RelayCommand CommandCell13
        {
            get
            {
                return Komendy[1, 3];
            }
        }

        public RelayCommand CommandCell23
        {
            get
            {
                return Komendy[2, 3];
            }
        }

        public RelayCommand CommandCell33
        {
            get
            {
                return Komendy[3, 3];
            }
        }

        public RelayCommand CommandCell43
        {
            get
            {
                return Komendy[4, 3];
            }
        }

        public RelayCommand CommandCell53
        {
            get
            {
                return Komendy[5, 3];
            }
        }

        public RelayCommand CommandCell63
        {
            get
            {
                return Komendy[6, 3];
            }
        }

        public RelayCommand CommandCell73
        {
            get
            {
                return Komendy[7, 3];
            }
        }

        public RelayCommand CommandCell83
        {
            get
            {
                return Komendy[8, 3];
            }
        }

        #endregion

        #region . Properties: Row 5 Cells .
        public RelayCommand CommandCell04
        {
            get
            {
                return Komendy[0, 4];
            }
        }

        public RelayCommand CommandCell14
        {
            get
            {
                return Komendy[1, 4];
            }
        }

        public RelayCommand CommandCell24
        {
            get
            {
                return Komendy[2, 4];
            }
        }

        public RelayCommand CommandCell34
        {
            get
            {
                return Komendy[3, 4];
            }
        }

        public RelayCommand CommandCell44
        {
            get
            {
                return Komendy[4, 4];
            }
        }

        public RelayCommand CommandCell54
        {
            get
            {
                return Komendy[5, 4];
            }
        }

        public RelayCommand CommandCell64
        {
            get
            {
                return Komendy[6, 4];
            }
        }

        public RelayCommand CommandCell74
        {
            get
            {
                return Komendy[7, 4];
            }
        }

        public RelayCommand CommandCell84
        {
            get
            {
                return Komendy[8, 4];
            }
        }

        #endregion

        #region . Properties: Row 6 Cells .
        public RelayCommand CommandCell05
        {
            get
            {
                return Komendy[0, 5];
            }
        }

        public RelayCommand CommandCell15
        {
            get
            {
                return Komendy[1, 5];
            }
        }

        public RelayCommand CommandCell25
        {
            get
            {
                return Komendy[2, 5];
            }
        }

        public RelayCommand CommandCell35
        {
            get
            {
                return Komendy[3, 5];
            }
        }

        public RelayCommand CommandCell45
        {
            get
            {
                return Komendy[4, 5];
            }
        }

        public RelayCommand CommandCell55
        {
            get
            {
                return Komendy[5, 5];
            }
        }

        public RelayCommand CommandCell65
        {
            get
            {
                return Komendy[6, 5];
            }
        }

        public RelayCommand CommandCell75
        {
            get
            {
                return Komendy[7, 5];
            }
        }

        public RelayCommand CommandCell85
        {
            get
            {
                return Komendy[8, 5];
            }
        }

        #endregion

        #region . Properties: Row 7 Cells .
        public RelayCommand CommandCell06
        {
            get
            {
                return Komendy[0, 6];
            }
        }

        public RelayCommand CommandCell16
        {
            get
            {
                return Komendy[1, 6];
            }
        }

        public RelayCommand CommandCell26
        {
            get
            {
                return Komendy[2, 6];
            }
        }

        public RelayCommand CommandCell36
        {
            get
            {
                return Komendy[3, 6];
            }
        }

        public RelayCommand CommandCell46
        {
            get
            {
                return Komendy[4, 6];
            }
        }

        public RelayCommand CommandCell56
        {
            get
            {
                return Komendy[5, 6];
            }
        }

        public RelayCommand CommandCell66
        {
            get
            {
                return Komendy[6, 6];
            }
        }

        public RelayCommand CommandCell76
        {
            get
            {
                return Komendy[7, 6];
            }
        }

        public RelayCommand CommandCell86
        {
            get
            {
                return Komendy[8, 6];
            }
        }

        #endregion

        #region . Properties: Row 8 Cells .
        public RelayCommand CommandCell07
        {
            get
            {
                return Komendy[0, 7];
            }
        }

        public RelayCommand CommandCell17
        {
            get
            {
                return Komendy[1, 7];
            }
        }

        public RelayCommand CommandCell27
        {
            get
            {
                return Komendy[2, 7];
            }
        }

        public RelayCommand CommandCell37
        {
            get
            {
                return Komendy[3, 7];
            }
        }

        public RelayCommand CommandCell47
        {
            get
            {
                return Komendy[4, 7];
            }
        }

        public RelayCommand CommandCell57
        {
            get
            {
                return Komendy[5, 7];
            }
        }

        public RelayCommand CommandCell67
        {
            get
            {
                return Komendy[6, 7];
            }
        }

        public RelayCommand CommandCell77
        {
            get
            {
                return Komendy[7, 7];
            }
        }

        public RelayCommand CommandCell87
        {
            get
            {
                return Komendy[8, 7];
            }
        }

        #endregion

        #region . Properties: Row 9 Cells .
        public RelayCommand CommandCell08
        {
            get
            {
                return Komendy[0, 8];
            }
        }

        public RelayCommand CommandCell18
        {
            get
            {
                return Komendy[1, 8];
            }
        }

        public RelayCommand CommandCell28
        {
            get
            {
                return Komendy[2, 8];
            }
        }

        public RelayCommand CommandCell38
        {
            get
            {
                return Komendy[3, 8];
            }
        }

        public RelayCommand CommandCell48
        {
            get
            {
                return Komendy[4, 8];
            }
        }

        public RelayCommand CommandCell58
        {
            get
            {
                return Komendy[5, 8];
            }
        }

        public RelayCommand CommandCell68
        {
            get
            {
                return Komendy[6, 8];
            }
        }

        public RelayCommand CommandCell78
        {
            get
            {
                return Komendy[7, 8];
            }
        }

        public RelayCommand CommandCell88
        {
            get
            {
                return Komendy[8, 8];
            }
        }

        #endregion

        #endregion








        //    private Pole[][] m_Plansza = new Pole[][]
    //    {
    //        new Pole[3],
    //        new Pole[3],
    //        new Pole[3],
    //    };

    //    public Pole[][] Plansza
    //    {
    //        get
    //        {
    //            return m_Plansza;
    //        }
    //    }



    //    private Pole nextSymbol = Pole.Krzyzyk;

    //    private Pole GetNextSymbol()
    //    {
    //        var x = nextSymbol;
    //        nextSymbol = (nextSymbol == Pole.Krzyzyk ? Pole.Kolko : Pole.Krzyzyk);
    //        return x;
    //    }

    //    public ViewModelClass()
    //    {
    //        for (int i = 0; i < 3; ++i)
    //        {
    //            m_Komendy[i] = new RelayCommand[3];
    //            for (int j = 0; j < 3; ++j)
    //            {
    //                var x = i;
    //                var y = j;
    //                m_Komendy[i][j] = new RelayCommand(
    //                    () => { m_Plansza[x][y] = GetNextSymbol(); FirePropertyChanged("Plansza"); },
    //                    () => { return m_Plansza[x][y] == Pole.Puste; }
    //                    );
    //            }
    //        }
    //    }

        private void FirePropertyChanged(string name)
        {
            var _event = PropertyChanged;
            if (_event != null)
            {
                _event(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
