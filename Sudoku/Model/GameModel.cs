using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    class GameModel
    {

        #region . Variables .

        private CellClass[,] _cells;                            // Array of cells for the puzzle that is playing.
        private List<CellClass>[] _regionList;                  // Array of cells arranged by region.

        #endregion

        #region . Constructors .

        /// <summary>
        /// Initialized a new instance of the GameModel class.
        /// </summary>
        /// <param name="cells">Array of cells to initialize this class with.</param>
        internal GameModel(CellClass[,] cells)
        {
            InitClass(cells);                                   // Call the initialization routine.
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of cells of the puzzle that is currently playing.
        /// </summary>
        internal List<CellClass> CellList { get; private set; }


        /// <summary>
        /// Gets or sets the number of empty cells left in the puzzle.
        /// </summary>
        internal Int32 EmptyCount { get; set; }


        #endregion


        private void InitClass(CellClass[,] cells)
        {
            if (cells != null)                                      // If the input parameter is not null
            {
                _cells = cells;                                     // Save it.
                InitRegionList();                                   // Initialize the region list.
                ConvertToList();                                    // Convert the 2D array to a list.
                CountEmpties();                                     // Count number of empty cells.
            }
        }

        private void InitRegionList()
        {
            _regionList = new List<CellClass>[9];                   // Initialize the array with 9 elements.
            for (Int32 i = 0; i < 9; i++)                           // Loop through the array.
                _regionList[i] = new List<CellClass>();             // Initialize each element with a list.
        }

        private void ConvertToList()
        {
            CellList = new List<CellClass>(_cells.Length);          // Initialize the list.
            for (Int32 col = 0; col < 9; col++)                     // Loop through the columns
                for (Int32 row = 0; row < 9; row++)                 // Loop through the rows
                    AddCell(_cells[col, row]);                      // Add each cell to the lists
        }

        private void CountEmpties()
        {
            EmptyCount = 0;                                             // Zero the counter
            foreach (CellClass item in CellList)                        // Loop through the list of cells
                if (item.CellState == CellStateEnum.Blank)              // If the state is blank
                    EmptyCount++;                                       // Then increment the count
        }


        private void AddCell(CellClass cell)
        {
            if (cell != null)                                       // Is input a valid object?
            {                                                       // Yes.
                CellList.Add(cell);                                 // Add the cell to the main list.
                _regionList[cell.Region].Add(cell);                 // Add the cell the corresponding region list.
            }
            else
                throw new Exception("Cell cannot be null.");        // TODO: Maybe add this to the event log instead?
        }

    }
}
