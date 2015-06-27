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
        String _easy =     "603071214111908051201080319151607141415090607080201130906111705130402080517141812191306011308121401160509070812030106040705190109060513171804121704051918121103061903161211041715181711050803061214190208140907150103061802071509131611140614091112070518031305011604181912170107120308190406051519030406021817110406181715011319121507030912061814110906011408050712030408021713010619050614051117180203190711091216131518141213081514190107160302071615040911080105041809070316020809060311021415170514161801020917130701030619140502181908021517030401060613091715081214010205111304191618070417081112061315090802041916010703050109070413150806121316151208070119140119160708050213041402181609131705011713150102140608190211130805191416071805041216071911131617190304111812051518110907120304160306171401181509021914120503060117081";
        String _medium =   "312160107091408051805190213041701060107041615081219130411020306050807190513071918120106140916080704010503120208031511061914170609110412071305080714150809031602011607091103081214151803120405091607010514010217160803091302160509011718041719080602040105130401150708031912061916070314120501081208130901051406070105041806071319121309141507120106181512110603081907040708160419010512031917030112160804050611080915040203171415020318170609010204150816030711091813170201091405060106191704150308121705060409021103081418091601030507021301121815071609141503170104181902060806110302190405171209140507161308010104181213051706191912051706010804031607030908041201051208150607110309041611091403150708120304070912180511060716030819020104151902081511041603170115040716030802191509010314160217080413061208170905110807120105190406031";
        String _hard =     "409080716031201150305170211080614091612110504090803070908060115140307121104031807121905160507020913160108141811190402050706030206140318070519011703050619011402180904051611080307021708020304051609111311060917020415080815170203040106190416031719110812050112190508060703140217040116090518030503010802071904161609081415030201071709051203011604080118020916041503170416031518170211090805091401020716030207040305160918110301061807090412050913011614150817020514080712031109160602071109081305040614080912031705010902111704051806131705130118060902040208061409011317050411090315070208061503071206081411090109150813020604070806141507091103121317020611041509080108040715090312061519130106020804070702161308041501191405170201130906180306120409081715011901180607150203140804111503071609121613190802010407050207050914060118031";
        String _expert =   "102140519030607181607150402080319011819030107061502040708010913040216150315060801120704090204090716050118130413020605091801070906170308010415021501180214070903161705140108020619030116020513091807040308191406070112150801030204160715090509170311080406121204060709150311080602181907040513110413050612011908070907110805030214060618091501020304070702141906130518010503010708140209160805021317090601140419061805011703021301071412060805190906050204180107130207131109150416080114081603070902050608021701040509131917030605081114020401150903120706080502191107130408061306010418190205070704181502160301091209140306150807010815070209011613040103061804070902151103071612050804190605080407091213010204190113080607150512060904031118070419110508070306121718030206011915040301150819040702160806040701021519030907021315060401180219080105070403160104160308190502071507030206041911181605090711030208040403011912180706151802070514060109030701040803021615191908150607110304021316020409050807110";
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
