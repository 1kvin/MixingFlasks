using Scripts.UI;

namespace Scripts.GameLogic.Actions
{
    public class WinAction
    {
        public void Win(GameUI gameUI)
        {
            gameUI.ShowWinPanel();
        }
    }
}
