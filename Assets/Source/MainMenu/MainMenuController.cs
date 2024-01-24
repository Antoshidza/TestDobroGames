using System;
using Source.Common;
using UnityEngine;

namespace Source.MainMenu
{
    public class MainMenuController : AMonoSingleton<MainMenuController>
    {
        [SerializeField] private MainMenuView _mainMenuView;

        public event Action StartButtonPressed;

        public void VisualizeLoading() => _mainMenuView.IsLoading = true;

        private void Start()
        {
            _mainMenuView.BestScoreLabel.text = "best score: not implemented";
            _mainMenuView.StartButton.onClick.AddListener(OnStartButtonPressed);
        }

        private void OnStartButtonPressed() => StartButtonPressed?.Invoke();
    }
}