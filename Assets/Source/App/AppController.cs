using Source.Common;
using Source.Gameplay;
using Source.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.App
{
    public class AppController : AMonoSingleton<AppController>
    {
        [Header("Scenes")]
        [SerializeField] private string _mainMenuSceneName;
        [SerializeField] private string _gameplaySceneName;
        
        private GameplayController _gameplayController;
        private MainMenuController _mainMenuController;
        
        public EAppState State { get; protected set; }

        private void Start()
        {
            DontDestroyOnLoad(this);
            OpenMainMenu();
        }
        
        private void OpenMainMenu()
            => SceneManager.LoadSceneAsync(_mainMenuSceneName).completed += OnMainMenuLoaded;

        private void OnMainMenuLoaded(AsyncOperation _)
        {
            State = EAppState.InMenu;

            _mainMenuController = DI.GetSingleton<MainMenuController>();
            _mainMenuController.StartButtonPressed += OnStartButtonPressed;
        }

        private void OnStartButtonPressed()
        {
            _mainMenuController.VisualizeLoading();
            OpenGame();
        }

        private void OpenGame()
        {
            SceneManager.LoadSceneAsync(_gameplaySceneName).completed += _ => 
            {
                _gameplayController = DI.GetSingleton<GameplayController>();
                _gameplayController.StateChanged += OnGameplayStateChanged;
                
                State = EAppState.Gameplay;
            };
        }

        private void OnGameplayStateChanged((EGameplayState fromState, EGameplayState toState) stateChange)
        {
            if(stateChange.fromState is EGameplayState.Lose or EGameplayState.Win && stateChange.toState is EGameplayState.Playing)
                OpenGame();
            else if(stateChange.toState is EGameplayState.Exit)
                OpenMainMenu();
        }
    }
}