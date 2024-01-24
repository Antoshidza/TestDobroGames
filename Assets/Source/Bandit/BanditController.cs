using System;
using Source.Common;
using Source.Gameplay.Triggers;
using Source.Guardians;
using UnityEngine;

namespace Source.Bandit
{
    public class BanditController : MonoBehaviour
    {
        [SerializeField] private int _fovAngle = 85;
        [SerializeField] private float _farViewDistance = 3f;
        [SerializeField] private float _nearViewDistance = 1f;
        
        [SerializeField] private BanditView _banditView;
        [SerializeField] private WaypointAgent _waypointAgent;
        [SerializeField] private RevealTrigger _revealTrigger;
        
        public event Action PlayerRevealed;
        
        private void Awake() => this.DIRegisterMultiple();

        private void Start()
        {
            _revealTrigger.Triggered += OnPlayerRevealed;

            _revealTrigger.FOVAngle = _fovAngle;
            _revealTrigger.NearDistance = _nearViewDistance;
            _revealTrigger.FarViewDistance = _farViewDistance;

            _banditView.NearViewDistance = _nearViewDistance;
            _banditView.Initialize(_fovAngle, _farViewDistance);
        }

        private void OnDestroy() => _revealTrigger.Triggered -= OnPlayerRevealed;

        private async void OnPlayerRevealed(GameObject playerCharacter)
        {
            _waypointAgent.enabled = false;
            transform.LookAt(playerCharacter.transform);

            _revealTrigger.Triggered -= OnPlayerRevealed;
            await _banditView.ShootTo(transform.position, playerCharacter.transform);
            PlayerRevealed?.Invoke();
        }
    }
}