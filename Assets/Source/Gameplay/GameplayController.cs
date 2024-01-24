using System;
using Source.Bandit;
using Source.Common;
using Source.Gameplay.GameResultPopup;
using UnityEngine;

namespace Source.Gameplay
{
    public class GameplayController : AMonoSingleton<GameplayController>
    {
        [SerializeField] private GameResultPopupView _gameResultPopupView;
        
        private EGameplayState _state;

        private EGameplayState State
        {
            set
            {
                var prev = _state;
                _state = value;
                StateChanged?.Invoke((prev, value));
            }
        }

        public Action<(EGameplayState from, EGameplayState to)> StateChanged;

        private void Start()
        {
            SubscribeToFinishTriggers();
            SubscribeToBandits();
        }

        private void SubscribeToFinishTriggers()
        {
            foreach (var finishTrigger in DI.GetMultiple<FinishTrigger>())
            {
                finishTrigger.Triggered += _ =>
                {
                    if(_state != EGameplayState.Playing)
                        return;
                
                    State = EGameplayState.Win;
                    ShowGameResult(true);
                };
            }
        }

        private void SubscribeToBandits()
        {
            foreach (var revealTrigger in DI.GetMultiple<BanditController>())
            {
                revealTrigger.PlayerRevealed += () =>
                {
                    if(_state != EGameplayState.Playing)
                        return;
                
                    State = EGameplayState.Lose;
                    ShowGameResult(false);
                };
            }
        }

        private void ShowGameResult(in bool win)
        {
            _gameResultPopupView.gameObject.SetActive(true);
            _gameResultPopupView.Win = win;
            _gameResultPopupView.PlayButton.onClick.AddListener(() => State = EGameplayState.Playing);
            _gameResultPopupView.ToMainMenuButton.onClick.AddListener(() => State = EGameplayState.Exit);
        }
    }
}