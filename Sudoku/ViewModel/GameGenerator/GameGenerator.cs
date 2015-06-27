﻿
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sudoku.Model;
using System;
using System.Threading.Tasks;

namespace Sudoku.ViewModel.GameGenerator
{
    internal class GameGenerator
    {
        #region . Variables, Constants, And other Declarations .

        #region . Variables .

        private DifficultyLevels _level;

        #endregion

        #region . Other Declarations .

        internal event EventHandler<GameGeneratorEventArgs> GameGeneratorEvent;

        #endregion

        #endregion

        #region . Constructors .

        /// <summary>
        /// Initializes a new instance of the GameGenerator class.
        /// </summary>
        /// <param name="level">Level of difficulty for this game generator instance to create.</param>
        internal GameGenerator(DifficultyLevels level)
        {
            _level = level;
        }

        #endregion

        #region . Properties: Public .

        /// <summary>
        /// Gets a flag that indicates whether this class is busy or not.
        /// </summary>
        internal bool Busy { get; private set; }

        #endregion

        #region . Methods .

        #region . Methods: Public .

        /// <summary>
        /// Starts a new background thread to create the game.
        /// </summary>
        internal void CreateNewGame()
        {
            Task t = new Task(GenerateNewGame);    // Instantiate a new thread
            t.Start();                                                  // Start the thread
        }

        #endregion

        #region . Methods: Private .

        private void GenerateNewGame()
        {
            CellClass[,] cells = GenerateNewBoard();                    // Generate a new game
            RaiseEvent(cells);                                          // Raise an event to tell whoever is listening that we're done
        }

        private CellClass[,] GenerateNewBoard()
        {
            try
            {
                Busy = true;                                                // Raise the busy flag
                CellClass[,] cells;                                         // Initialize some variables
                PopulatePuzzle cPopulate = new PopulatePuzzle();
                MaskPuzzle cMask = new MaskPuzzle(_level);
                do
                {
                    cells = cPopulate.GeneratePuzzle();                     // Create a new game
                    cMask.MaskBoard(cells);                                 // Try to mask cells
                } while (cMask.NotGood);                                    // Was mask successful?  If not, then loop again
                return cells;                                               // Return the generated game
            }
            finally
            {
                Busy = false;                                               // Clear busy flag
            }
        }

        protected virtual void RaiseEvent(CellClass[,] cells)
        {
            EventHandler<GameGeneratorEventArgs> handler = GameGeneratorEvent;
            if (handler != null)
            {
                GameGeneratorEventArgs e = new GameGeneratorEventArgs(cells);
                handler(this, e);
            }
        }

        #endregion

        #endregion
    }
}
