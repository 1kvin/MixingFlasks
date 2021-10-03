using Scripts.GameLogic;
using UnityEngine;

namespace Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private GameObject winPanel;

        public void ShowWinPanel()
        {
            winPanel.SetActive(true);
        }
        
        public void RestartButton()
        {
            game.RestartLevel();
        }
        
        public void HomeButton()
        {
            
        }

        public void UndoButton()
        {
            game.UndoMove();
        }

        public void NextButton()
        {
            game.StartNewGame();
            winPanel.SetActive(false);
        }
    }
}
