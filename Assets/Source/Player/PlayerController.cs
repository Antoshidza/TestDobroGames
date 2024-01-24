using Source.Common;
using Source.Gameplay;
using StarterAssets;
using UnityEngine;

namespace Source.Player
{
    public class PlayerController : AMonoSingleton<PlayerController>
    {
        [SerializeField] private StarterAssetsInputs _input;
        [SerializeField] private ThirdPersonController _thirdPersonController;
        
        private int _playerLayer;

        public bool InputEnabled { set => _thirdPersonController.enabled = value; }

        private void Awake() => _playerLayer = LayerMask.NameToLayer("Player");

        private void Start() 
            => DI.GetSingleton<GameplayController>().StateChanged += state 
                => InputEnabled = state.to == EGameplayState.Playing;

        private void Update() => gameObject.layer = _input.move == Vector2.zero ? 0 : _playerLayer;
    }
}