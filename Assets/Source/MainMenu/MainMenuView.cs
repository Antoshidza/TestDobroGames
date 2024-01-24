using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.MainMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingLabel;
        [SerializeField] private GameObject _menuMainPanel;
        
        [field: SerializeField]
        public Button StartButton { get; private set; }
        
        [field: SerializeField]
        public TMP_Text BestScoreLabel { get; private set; }

        public bool IsLoading
        {
            set 
            { 
                _loadingLabel.SetActive(value);
                _menuMainPanel.SetActive(!value);
            }
        }
    }
}