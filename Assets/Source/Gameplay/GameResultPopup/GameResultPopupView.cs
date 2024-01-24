using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Gameplay.GameResultPopup
{
    public class GameResultPopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resultLabel;
        
        [field: SerializeField] public Button ToMainMenuButton { get; private set; }
        [field: SerializeField] public Button PlayButton { get; private set; }

        public bool Win { set => _resultLabel.text = value ? "You WIN!" : "You lose :("; }
    }
}