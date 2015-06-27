using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.ViewModel.GameGenerator
{
    internal class GamesManager
    {
        #region . Variables, Constants, And other Declarations .

        #region . Variables .

        private GameCollection[] _games = new GameCollection[Common.MaxLevels];

        String _veryEasy = "318190402160705111507140109031618121216111715180413091911150814070216030413161211191817151712180316050911040815121917110314061609130508041112171114170603120509181416130712151811091207180913111604150511190604181703121914161107120315181115120418031917161813171506190112141712140309161508111309110815141206170618150211171419031206181407031501190911141512181306071703151109161814121809111214051703160402061813171119151507131916011402180104171605191218131318191711121605041605121308041907110214071905011816131906081317141502111513011218161419070605021819171311141719031401121605181108041516131917121411051713191218060802061114151703191317091602081114151518131214170601091902141118060305071711161903050402180215111317181916041316181405190217111417191611121518031119121706040803150603151819010704021814171512130109061";
        String _easy = "318190402160705111507140109031618121216111715180413091911150814070216030413161211191817151712180316050911040815121917110314061609130508041112171114170603120509181416130712151811091207180913111604150511190604181703121914161107120315181115120418031917161813171506190112141712140309161508111309110815141206170618150211171419031206181407031501190911141512181306071703151109161814121809111214051703160402061813171119151507131916011402180104171605191218131318191711121605041605121308041907110214071905011816131906081317141502111513011218161419070605021819171311141719031401121605181108041516131917121411051713191218060802061114151703191317091602081114151518131214170601091902141118060305071711161903050402180215111317181916041316181405190217111417191611121518031119121706040803150603151819010704021814171512130109061";
        String _medium = "318190402160705111507140109031618121216111715180413091911150814070216030413161211191817151712180316050911040815121917110314061609130508041112171114170603120509181416130712151811091207180913111604150511190604181703121914161107120315181115120418031917161813171506190112141712140309161508111309110815141206170618150211171419031206181407031501190911141512181306071703151109161814121809111214051703160402061813171119151507131916011402180104171605191218131318191711121605041605121308041907110214071905011816131906081317141502111513011218161419070605021819171311141719031401121605181108041516131917121411051713191218060802061114151703191317091602081114151518131214170601091902141118060305071711161903050402180215111317181916041316181405190217111417191611121518031119121706040803150603151819010704021814171512130109061";
        String _hard = "318190402160705111507140109031618121216111715180413091911150814070216030413161211191817151712180316050911040815121917110314061609130508041112171114170603120509181416130712151811091207180913111604150511190604181703121914161107120315181115120418031917161813171506190112141712140309161508111309110815141206170618150211171419031206181407031501190911141512181306071703151109161814121809111214051703160402061813171119151507131916011402180104171605191218131318191711121605041605121308041907110214071905011816131906081317141502111513011218161419070605021819171311141719031401121605181108041516131917121411051713191218060802061114151703191317091602081114151518131214170601091902141118060305071711161903050402180215111317181916041316181405190217111417191611121518031119121706040803150603151819010704021814171512130109061";
        String _expert = "318190402160705111507140109031618121216111715180413091911150814070216030413161211191817151712180316050911040815121917110314061609130508041112171114170603120509181416130712151811091207180913111604150511190604181703121914161107120315181115120418031917161813171506190112141712140309161508111309110815141206170618150211171419031206181407031501190911141512181306071703151109161814121809111214051703160402061813171119151507131916011402180104171605191218131318191711121605041605121308041907110214071905011816131906081317141502111513011218161419070605021819171311141719031401121605181108041516131917121411051713191218060802061114151703191317091602081114151518131214170601091902141118060305071711161903050402180215111317181916041316181405190217111417191611121518031119121706040803150603151819010704021814171512130109061";

        #endregion

        #region . Other Declarations .

        internal event EventHandler<GameManagerEventArgs> GamesManagerEvent;

        #endregion

        #endregion

        #region . Constructors .

        /// <summary>
        /// Initializes a new instance of the GamesManager class.
        /// </summary>
        internal GamesManager()
        {
            InitializeClass();
        }

        #endregion

        #region . Event Handlers .

        private void GameManagerEventHandler(object sender, GameManagerEventArgs e)
        {
            RaiseEvent(e);
        }

        #endregion

        #region . Methods .

        #region . Methods: Public .

        /// <summary>
        /// Stops the game manager thread.
        /// </summary>
        internal void StopGamesManager()
        {
            StopBackgroundTasks();              // Stop all background threads.
            SaveGames();                        // Save the games to the application config file.
        }

        /// <summary>
        /// Gets a game based on the specified difficulty level.
        /// </summary>
        /// <param name="level">Level of difficulty specified.</param>
        /// <returns>A 2D CellClass array of the game.</returns>
        internal CellClass[,] GetGame(DifficultyLevels level)
        {
            return _games[(int)level].GetGame;          // Get a game based on the specified difficulty level
        }

        /// <summary>
        /// Gets the number of games in the game queue of the specified level.
        /// </summary>
        /// <param name="level">Difficulty level of the count needed.</param>
        /// <returns>The number of games in the queue.</returns>
        internal Int32 GameCount(DifficultyLevels level)
        {
            return _games[(int)level].GameCount;
        }

        #endregion

        #region . Methods: Private .

        private void InitializeClass()
        {
            InitGameCollectionArray();                              // Initialize the game collection array
            LoadGames();                                            // Load any saved games from the config file
            StartBackgroundTasks();                                 // Start the background tasks
        }

        private void InitGameCollectionArray()
        {
            foreach (Int32 i in Enum.GetValues(typeof(DifficultyLevels)))   // Loop through the enum
                _games[i] = InitGameCollection((DifficultyLevels)i);        // For each level, initialize a game manager class
        }

        private GameCollection InitGameCollection(DifficultyLevels level)
        {
            GameCollection collection = new GameCollection(level);      // Instantiate a new game collection class
            collection.GameManagerEvent += GameManagerEventHandler;     // Set the event handler
            return collection;                                          // Return the pointer
        }

        private void LoadGames()
        {
            _games[0].LoadGames(_veryEasy);       // Load games from the config file
            _games[1].LoadGames(_easy);
            _games[2].LoadGames(_medium);
            _games[3].LoadGames(_hard);
            _games[4].LoadGames(_expert);
        }

        private void StartBackgroundTasks()
        {
            foreach (GameCollection item in _games)                     // Loop though the array
                item.StartThread();                                     // Start each background thread
        }

        private void StopBackgroundTasks()
        {
            foreach (GameCollection item in _games)                     // Loop through the array
                item.StopThread();                                      // Stop each background thread
        }

        private void SaveGames()
        {
            _veryEasy = _games[0].SaveGames();    // Save the games to the application config file
            _easy = _games[1].SaveGames();
            _medium = _games[2].SaveGames();
            _hard = _games[3].SaveGames();
            _expert = _games[4].SaveGames();                                // Now save it to disk
        }

        private void RaiseEvent(GameManagerEventArgs e)
        {
            EventHandler<GameManagerEventArgs> handler = GamesManagerEvent;     // Get a pointer to the event handler
            if (handler != null)                                                // Any listeners?
                handler(this, e);                                               // Yes, then raise the event
        }

        #endregion

        #endregion
    }
}
