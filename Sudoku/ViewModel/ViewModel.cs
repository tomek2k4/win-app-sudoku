
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
using Sudoku.ViewModel.GameGenerator;

namespace Sudoku.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        private GameModel _model;
        private RelayCommand[,] _komendy = new RelayCommand[9, 9];
        private GamesManager _games;

        private DifficultyLevels _gameLevel = DifficultyLevels.Easy;

        private bool GameInProgress { get; set; }
        private bool PuzzleComplete { get; set; }


        #region . Constructors .

        public ViewModelClass()
        {
            Debug.WriteLine("Initialize View Model ...");
            
            //CellClass[,] cells = GenerateNewBoard();
            //cells[0, 0].CellState = CellStateEnum.Answer;
            //cells[0, 0].Answer = 5;

            _games = new GamesManager();                            // Instantiate a new game manager class
            _games.GamesManagerEvent += GamesManagerEventHandler;   // Set the event handler

            LoadSettings();                                         // Load settings

            GetNewGame(); //Initialize model
            PuzzleComplete = false;                                 // Clear some flags
            GameInProgress = false;
            if(_model == null)
            {
                _model = new GameModel(null);                           // Initialize the model with null
            }
            

            for (int i = 0; i < 9; ++i)
            {
                for (int j = 0; j < 9; ++j)
                {
                    var col = j;
                    var row = i;
                    _komendy[j,i] = new RelayCommand(
                        () => { ProcessCellClick(col, row); FirePropertyChanged("Cell" + col.ToString() + row.ToString()); },
                        () => { return _model[col, row].CellState != CellStateEnum.Answer; }
                        );
                }
            }
        }

        #endregion
        private void ProcessCellClick(Int32 col, Int32 row)
        {
            if (IsValidGame() && (_model[col, row].CellState != CellStateEnum.Answer))  // Is a game in progress and the cell is in the answer state
                IncrementUserAnswer(col, row);  // Yes, display the number pad and process it
        }

        private bool IsValidGame()
        {   // Return true if the there is a game in progress and the model is valid
            return ((_model != null) && (_model.CellList != null));
        }


        private void LoadSettings()
        {
            GameLevel = ConvertGameLevel(4);    // Load game level from settings
            // TODO: Load previous game if any.
            // If there was a previous game saved, then ask player
            // if they want to play old game or load a new game.
        }

        private static DifficultyLevels ConvertGameLevel(Int32 value)
        {
            try
            {
                if (Enum.IsDefined(typeof(DifficultyLevels), value))    // Is the value part of the enum?
                    return (DifficultyLevels)value;                     // Yes, return the value
            }
            catch (Exception)
            {
                // TODO: What to do here?
                // Maybe log error to Application.Event log?
            }
            return DifficultyLevels.VeryEasy;  
        }



        private void GetNewGame()
        {
            if (_games.GameCount(GameLevel) > 0)                    // Are there games available? 
            {
                _model = new GameModel(_games.GetGame(GameLevel));  // Yes, load a game in the model class
            }
        }


        /// <summary>
        /// Gets or sets the Game Level.
        /// </summary>
        public DifficultyLevels GameLevel
        {
            get
            {
                return _gameLevel;
            }
            set
            {
                bool bLoadNewGame = (_gameLevel != value);                      // Save whether the value changed or not
                _gameLevel = value;                                             // Save the value to our local variable                                         // Raise the property changed flag
            }
        }


        private void IncrementUserAnswer(Int32 col, Int32 row)
        {
            if(_model[col,row].UserAnswer == 9)
            {
                if (_model[col, row].CellState == CellStateEnum.Blank)
                {
                    _model[col, row].CellState = CellStateEnum.UserInputIncorrect;
                    _model[col, row].UserAnswer = 1;
                }
                else
                {
                    _model[col, row].CellState = CellStateEnum.Blank;
                }
            }
            else
            {
                _model[col, row].CellState = CellStateEnum.UserInputIncorrect;
                _model[col, row].UserAnswer++;
            }
        }

        private void GamesManagerEventHandler(object sender, GameManagerEventArgs e)
        {
            switch (e.Level)                                    // Which level raised the event
            {
                case DifficultyLevels.VeryEasy:                 // Very Easy level raised event
                    GameCountVeryEasy = e.Count.ToString();     // Save the count to the Very Easy level property
                    break;

                case DifficultyLevels.Easy:                     // Easy level raised the event
                    GameCountEasy = e.Count.ToString();         // Save the count to the Easy level property
                    break;

                case DifficultyLevels.Medium:                   // Medium level raised the event
                    GameCountMedium = e.Count.ToString();       // Save the count to the Medium level property
                    break;

                case DifficultyLevels.Hard:                     // Hard level raised the event
                    GameCountHard = e.Count.ToString();         // Save the count to the Hard level property
                    break;

                case DifficultyLevels.Expert:                   // Expert level raised the event
                    GameCountExpert = e.Count.ToString();       // Save the count to the Expert level property
                    break;
            }
        }




        /// <summary>
        /// Gets the game count for the Very Easy level.
        /// </summary>
        public string GameCountVeryEasy
        {
            get
            {
                return GetGameCount(DifficultyLevels.VeryEasy);
            }
            private set
            {
                //FirePropertyChanged("");
            }
        }

        /// <summary>
        /// Gets the game count for the Easy level.
        /// </summary>
        public string GameCountEasy
        {
            get
            {
                return GetGameCount(DifficultyLevels.Easy);
            }
            private set
            {
                //FirePropertyChanged("");
            }
        }

        /// <summary>
        /// Gets the game count for the Medium level.
        /// </summary>
        public string GameCountMedium
        {
            get
            {
                return GetGameCount(DifficultyLevels.Medium);
            }
            private set
            {
                //FirePropertyChanged("");
            }
        }

        /// <summary>
        /// Gets the game count for the Hard level.
        /// </summary>
        public string GameCountHard
        {
            get
            {
                return GetGameCount(DifficultyLevels.Hard);
            }
            private set
            {
                //FirePropertyChanged("");
            }
        }

        /// <summary>
        /// Gets the game count for the Expert level.
        /// </summary>
        public string GameCountExpert
        {
            get
            {
                return GetGameCount(DifficultyLevels.Expert);
            }
            private set
            {
                //FirePropertyChanged("");
            }
        }


        private string GetGameCount(DifficultyLevels level)
        {
            if (_games == null)                                         // Game manager class instantiated?
                return "0";                                             // No, then just return zero
            return _games.GameCount(level).ToString();                  // Yes, then return the actual game count
        }


        //private CellClass[,] GenerateNewBoard()
        //{
        //    CellClass[] cellsList = new CellClass[81];
        //    for (Int32 i = 0; i < 81; i++)                          // Loop through the array
        //        cellsList[i] = null;


        //    Int32 index = 0;
        //    do
        //    {
        //        CellClass item = new CellClass(index); // Create a new element with answer 1
        //        //item.UserAnswer = item.Region + 1;
        //        cellsList[index] = item;
        //        index++;
        //    } while (index < 81);

        //    return TransferGameToGrid(cellsList);
        //}

        //private CellClass[,] TransferGameToGrid(CellClass[] cellsList)
        //{
        //    CellClass[,] cellsArray = new CellClass[9, 9];                           // Initialize a new cell array
        //    foreach (CellClass item in cellsList)                                  // Loop the cell list
        //        cellsArray[item.Col, item.Row] = item;                               // Save the pointer
        //    return cellsArray;                                                       // Return the cell array
        //}

        #region . Properties: Cell Contents .

        // Property pointers to individual cells of the puzzle.

        #region . Properties: Row 1 Cells .
        public CellClass Cell00
        {
            get
            {
                return _model[0, 0];
            }
        }

        public CellClass Cell10
        {
            get
            {
                return _model[1, 0];
            }
        }

        public CellClass Cell20
        {
            get
            {
                return _model[2, 0];
            }
        }



        public CellClass Cell30
        {
            get
            {
                return _model[3, 0];
            }
        }
        public CellClass Cell40
        {
            get
            {
                return _model[4, 0];
            }
        }
        public CellClass Cell50
        {
            get
            {
                return _model[5, 0];
            }
        }
        public CellClass Cell60
        {
            get
            {
                return _model[6, 0];
            }
        }
        public CellClass Cell70
        {
            get
            {
                return _model[7, 0];
            }
        }
        public CellClass Cell80
        {
            get
            {
                return _model[8, 0];
            }
        }

        #endregion

        #region . Properties: Row 2 Cells .
        public CellClass Cell01
        {
            get
            {
                return _model[0, 1];
            }
        }

        public CellClass Cell11
        {
            get
            {
                return _model[1, 1];
            }
        }

        public CellClass Cell21
        {
            get
            {
                return _model[2, 1];
            }
        }

        public CellClass Cell31
        {
            get
            {
                return _model[3, 1];
            }
        }

        public CellClass Cell41
        {
            get
            {
                return _model[4, 1];
            }
        }

        public CellClass Cell51
        {
            get
            {
                return _model[5, 1];
            }
        }

        public CellClass Cell61
        {
            get
            {
                return _model[6, 1];
            }
        }

        public CellClass Cell71
        {
            get
            {
                return _model[7, 1];
            }
        }

        public CellClass Cell81
        {
            get
            {
                return _model[8, 1];
            }
        }

        #endregion

        #region . Properties: Row 3 Cells .
        public CellClass Cell02
        {
            get
            {
                return _model[0, 2];
            }
        }

        public CellClass Cell12
        {
            get
            {
                return _model[1, 2];
            }
        }

        public CellClass Cell22
        {
            get
            {
                return _model[2, 2];
            }
        }

        public CellClass Cell32
        {
            get
            {
                return _model[3, 2];
            }
        }

        public CellClass Cell42
        {
            get
            {
                return _model[4, 2];
            }
        }

        public CellClass Cell52
        {
            get
            {
                return _model[5, 2];
            }
        }

        public CellClass Cell62
        {
            get
            {
                return _model[6, 2];
            }
        }

        public CellClass Cell72
        {
            get
            {
                return _model[7, 2];
            }
        }

        public CellClass Cell82
        {
            get
            {
                return _model[8, 2];
            }
        }

        #endregion

        #region . Properties: Row 4 Cells .
        public CellClass Cell03
        {
            get
            {
                return _model[0, 3];
            }
        }

        public CellClass Cell13
        {
            get
            {
                return _model[1, 3];
            }
        }

        public CellClass Cell23
        {
            get
            {
                return _model[2, 3];
            }
        }

        public CellClass Cell33
        {
            get
            {
                return _model[3, 3];
            }
        }

        public CellClass Cell43
        {
            get
            {
                return _model[4, 3];
            }
        }

        public CellClass Cell53
        {
            get
            {
                return _model[5, 3];
            }
        }

        public CellClass Cell63
        {
            get
            {
                return _model[6, 3];
            }
        }

        public CellClass Cell73
        {
            get
            {
                return _model[7, 3];
            }
        }

        public CellClass Cell83
        {
            get
            {
                return _model[8, 3];
            }
        }

        #endregion

        #region . Properties: Row 5 Cells .
        public CellClass Cell04
        {
            get
            {
                return _model[0, 4];
            }
        }

        public CellClass Cell14
        {
            get
            {
                return _model[1, 4];
            }
        }

        public CellClass Cell24
        {
            get
            {
                return _model[2, 4];
            }
        }

        public CellClass Cell34
        {
            get
            {
                return _model[3, 4];
            }
        }

        public CellClass Cell44
        {
            get
            {
                return _model[4, 4];
            }
        }

        public CellClass Cell54
        {
            get
            {
                return _model[5, 4];
            }
        }

        public CellClass Cell64
        {
            get
            {
                return _model[6, 4];
            }
        }

        public CellClass Cell74
        {
            get
            {
                return _model[7, 4];
            }
        }

        public CellClass Cell84
        {
            get
            {
                return _model[8, 4];
            }
        }

        #endregion

        #region . Properties: Row 6 Cells .
        public CellClass Cell05
        {
            get
            {
                return _model[0, 5];
            }
        }

        public CellClass Cell15
        {
            get
            {
                return _model[1, 5];
            }
        }

        public CellClass Cell25
        {
            get
            {
                return _model[2, 5];
            }
        }

        public CellClass Cell35
        {
            get
            {
                return _model[3, 5];
            }
        }

        public CellClass Cell45
        {
            get
            {
                return _model[4, 5];
            }
        }

        public CellClass Cell55
        {
            get
            {
                return _model[5, 5];
            }
        }

        public CellClass Cell65
        {
            get
            {
                return _model[6, 5];
            }
        }

        public CellClass Cell75
        {
            get
            {
                return _model[7, 5];
            }
        }

        public CellClass Cell85
        {
            get
            {
                return _model[8, 5];
            }
        }

        #endregion

        #region . Properties: Row 7 Cells .
        public CellClass Cell06
        {
            get
            {
                return _model[0, 6];
            }
        }

        public CellClass Cell16
        {
            get
            {
                return _model[1, 6];
            }
        }

        public CellClass Cell26
        {
            get
            {
                return _model[2, 6];
            }
        }

        public CellClass Cell36
        {
            get
            {
                return _model[3, 6];
            }
        }

        public CellClass Cell46
        {
            get
            {
                return _model[4, 6];
            }
        }

        public CellClass Cell56
        {
            get
            {
                return _model[5, 6];
            }
        }

        public CellClass Cell66
        {
            get
            {
                return _model[6, 6];
            }
        }

        public CellClass Cell76
        {
            get
            {
                return _model[7, 6];
            }
        }

        public CellClass Cell86
        {
            get
            {
                return _model[8, 6];
            }
        }

        #endregion

        #region . Properties: Row 8 Cells .
        public CellClass Cell07
        {
            get
            {
                return _model[0, 7];
            }
        }

        public CellClass Cell17
        {
            get
            {
                return _model[1, 7];
            }
        }

        public CellClass Cell27
        {
            get
            {
                return _model[2, 7];
            }
        }

        public CellClass Cell37
        {
            get
            {
                return _model[3, 7];
            }
        }

        public CellClass Cell47
        {
            get
            {
                return _model[4, 7];
            }
        }

        public CellClass Cell57
        {
            get
            {
                return _model[5, 7];
            }
        }

        public CellClass Cell67
        {
            get
            {
                return _model[6, 7];
            }
        }

        public CellClass Cell77
        {
            get
            {
                return _model[7, 7];
            }
        }

        public CellClass Cell87
        {
            get
            {
                return _model[8, 7];
            }
        }

        #endregion

        #region . Properties: Row 9 Cells .
        public CellClass Cell08
        {
            get
            {
                return _model[0, 8];
            }
        }

        public CellClass Cell18
        {
            get
            {
                return _model[1, 8];
            }
        }

        public CellClass Cell28
        {
            get
            {
                return _model[2, 8];
            }
        }

        public CellClass Cell38
        {
            get
            {
                return _model[3, 8];
            }
        }

        public CellClass Cell48
        {
            get
            {
                return _model[4, 8];
            }
        }

        public CellClass Cell58
        {
            get
            {
                return _model[5, 8];
            }
        }

        public CellClass Cell68
        {
            get
            {
                return _model[6, 8];
            }
        }

        public CellClass Cell78
        {
            get
            {
                return _model[7, 8];
            }
        }

        public CellClass Cell88
        {
            get
            {
                return _model[8, 8];
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
                return _komendy[0, 0];
            }
        }

        public RelayCommand CommandCell10
        {
            get
            {
                return _komendy[1, 0];
            }
        }

        public RelayCommand CommandCell20
        {
            get
            {
                return _komendy[2, 0];
            }
        }



        public RelayCommand CommandCell30
        {
            get
            {
                return _komendy[3, 0];
            }
        }
        public RelayCommand CommandCell40
        {
            get
            {
                return _komendy[4, 0];
            }
        }
        public RelayCommand CommandCell50
        {
            get
            {
                return _komendy[5, 0];
            }
        }
        public RelayCommand CommandCell60
        {
            get
            {
                return _komendy[6, 0];
            }
        }
        public RelayCommand CommandCell70
        {
            get
            {
                return _komendy[7, 0];
            }
        }
        public RelayCommand CommandCell80
        {
            get
            {
                return _komendy[8, 0];
            }
        }

        #endregion

        #region . Properties: Row 2 Cells .
        public RelayCommand CommandCell01
        {
            get
            {
                return _komendy[0, 1];
            }
        }

        public RelayCommand CommandCell11
        {
            get
            {
                return _komendy[1, 1];
            }
        }

        public RelayCommand CommandCell21
        {
            get
            {
                return _komendy[2, 1];
            }
        }

        public RelayCommand CommandCell31
        {
            get
            {
                return _komendy[3, 1];
            }
        }

        public RelayCommand CommandCell41
        {
            get
            {
                return _komendy[4, 1];
            }
        }

        public RelayCommand CommandCell51
        {
            get
            {
                return _komendy[5, 1];
            }
        }

        public RelayCommand CommandCell61
        {
            get
            {
                return _komendy[6, 1];
            }
        }

        public RelayCommand CommandCell71
        {
            get
            {
                return _komendy[7, 1];
            }
        }

        public RelayCommand CommandCell81
        {
            get
            {
                return _komendy[8, 1];
            }
        }

        #endregion

        #region . Properties: Row 3 Cells .
        public RelayCommand CommandCell02
        {
            get
            {
                return _komendy[0, 2];
            }
        }

        public RelayCommand CommandCell12
        {
            get
            {
                return _komendy[1, 2];
            }
        }

        public RelayCommand CommandCell22
        {
            get
            {
                return _komendy[2, 2];
            }
        }

        public RelayCommand CommandCell32
        {
            get
            {
                return _komendy[3, 2];
            }
        }

        public RelayCommand CommandCell42
        {
            get
            {
                return _komendy[4, 2];
            }
        }

        public RelayCommand CommandCell52
        {
            get
            {
                return _komendy[5, 2];
            }
        }

        public RelayCommand CommandCell62
        {
            get
            {
                return _komendy[6, 2];
            }
        }

        public RelayCommand CommandCell72
        {
            get
            {
                return _komendy[7, 2];
            }
        }

        public RelayCommand CommandCell82
        {
            get
            {
                return _komendy[8, 2];
            }
        }

        #endregion

        #region . Properties: Row 4 Cells .
        public RelayCommand CommandCell03
        {
            get
            {
                return _komendy[0, 3];
            }
        }

        public RelayCommand CommandCell13
        {
            get
            {
                return _komendy[1, 3];
            }
        }

        public RelayCommand CommandCell23
        {
            get
            {
                return _komendy[2, 3];
            }
        }

        public RelayCommand CommandCell33
        {
            get
            {
                return _komendy[3, 3];
            }
        }

        public RelayCommand CommandCell43
        {
            get
            {
                return _komendy[4, 3];
            }
        }

        public RelayCommand CommandCell53
        {
            get
            {
                return _komendy[5, 3];
            }
        }

        public RelayCommand CommandCell63
        {
            get
            {
                return _komendy[6, 3];
            }
        }

        public RelayCommand CommandCell73
        {
            get
            {
                return _komendy[7, 3];
            }
        }

        public RelayCommand CommandCell83
        {
            get
            {
                return _komendy[8, 3];
            }
        }

        #endregion

        #region . Properties: Row 5 Cells .
        public RelayCommand CommandCell04
        {
            get
            {
                return _komendy[0, 4];
            }
        }

        public RelayCommand CommandCell14
        {
            get
            {
                return _komendy[1, 4];
            }
        }

        public RelayCommand CommandCell24
        {
            get
            {
                return _komendy[2, 4];
            }
        }

        public RelayCommand CommandCell34
        {
            get
            {
                return _komendy[3, 4];
            }
        }

        public RelayCommand CommandCell44
        {
            get
            {
                return _komendy[4, 4];
            }
        }

        public RelayCommand CommandCell54
        {
            get
            {
                return _komendy[5, 4];
            }
        }

        public RelayCommand CommandCell64
        {
            get
            {
                return _komendy[6, 4];
            }
        }

        public RelayCommand CommandCell74
        {
            get
            {
                return _komendy[7, 4];
            }
        }

        public RelayCommand CommandCell84
        {
            get
            {
                return _komendy[8, 4];
            }
        }

        #endregion

        #region . Properties: Row 6 Cells .
        public RelayCommand CommandCell05
        {
            get
            {
                return _komendy[0, 5];
            }
        }

        public RelayCommand CommandCell15
        {
            get
            {
                return _komendy[1, 5];
            }
        }

        public RelayCommand CommandCell25
        {
            get
            {
                return _komendy[2, 5];
            }
        }

        public RelayCommand CommandCell35
        {
            get
            {
                return _komendy[3, 5];
            }
        }

        public RelayCommand CommandCell45
        {
            get
            {
                return _komendy[4, 5];
            }
        }

        public RelayCommand CommandCell55
        {
            get
            {
                return _komendy[5, 5];
            }
        }

        public RelayCommand CommandCell65
        {
            get
            {
                return _komendy[6, 5];
            }
        }

        public RelayCommand CommandCell75
        {
            get
            {
                return _komendy[7, 5];
            }
        }

        public RelayCommand CommandCell85
        {
            get
            {
                return _komendy[8, 5];
            }
        }

        #endregion

        #region . Properties: Row 7 Cells .
        public RelayCommand CommandCell06
        {
            get
            {
                return _komendy[0, 6];
            }
        }

        public RelayCommand CommandCell16
        {
            get
            {
                return _komendy[1, 6];
            }
        }

        public RelayCommand CommandCell26
        {
            get
            {
                return _komendy[2, 6];
            }
        }

        public RelayCommand CommandCell36
        {
            get
            {
                return _komendy[3, 6];
            }
        }

        public RelayCommand CommandCell46
        {
            get
            {
                return _komendy[4, 6];
            }
        }

        public RelayCommand CommandCell56
        {
            get
            {
                return _komendy[5, 6];
            }
        }

        public RelayCommand CommandCell66
        {
            get
            {
                return _komendy[6, 6];
            }
        }

        public RelayCommand CommandCell76
        {
            get
            {
                return _komendy[7, 6];
            }
        }

        public RelayCommand CommandCell86
        {
            get
            {
                return _komendy[8, 6];
            }
        }

        #endregion

        #region . Properties: Row 8 Cells .
        public RelayCommand CommandCell07
        {
            get
            {
                return _komendy[0, 7];
            }
        }

        public RelayCommand CommandCell17
        {
            get
            {
                return _komendy[1, 7];
            }
        }

        public RelayCommand CommandCell27
        {
            get
            {
                return _komendy[2, 7];
            }
        }

        public RelayCommand CommandCell37
        {
            get
            {
                return _komendy[3, 7];
            }
        }

        public RelayCommand CommandCell47
        {
            get
            {
                return _komendy[4, 7];
            }
        }

        public RelayCommand CommandCell57
        {
            get
            {
                return _komendy[5, 7];
            }
        }

        public RelayCommand CommandCell67
        {
            get
            {
                return _komendy[6, 7];
            }
        }

        public RelayCommand CommandCell77
        {
            get
            {
                return _komendy[7, 7];
            }
        }

        public RelayCommand CommandCell87
        {
            get
            {
                return _komendy[8, 7];
            }
        }

        #endregion

        #region . Properties: Row 9 Cells .
        public RelayCommand CommandCell08
        {
            get
            {
                return _komendy[0, 8];
            }
        }

        public RelayCommand CommandCell18
        {
            get
            {
                return _komendy[1, 8];
            }
        }

        public RelayCommand CommandCell28
        {
            get
            {
                return _komendy[2, 8];
            }
        }

        public RelayCommand CommandCell38
        {
            get
            {
                return _komendy[3, 8];
            }
        }

        public RelayCommand CommandCell48
        {
            get
            {
                return _komendy[4, 8];
            }
        }

        public RelayCommand CommandCell58
        {
            get
            {
                return _komendy[5, 8];
            }
        }

        public RelayCommand CommandCell68
        {
            get
            {
                return _komendy[6, 8];
            }
        }

        public RelayCommand CommandCell78
        {
            get
            {
                return _komendy[7, 8];
            }
        }

        public RelayCommand CommandCell88
        {
            get
            {
                return _komendy[8, 8];
            }
        }

        #endregion

        #endregion

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
