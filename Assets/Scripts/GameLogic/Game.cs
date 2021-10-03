using Scripts.Effects;
using Scripts.GameLogic.Actions;
using Scripts.GameLogic.Entity;
using Scripts.Generator;
using Scripts.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.GameLogic
{
   public class Game : MonoBehaviour
   {
      [SerializeField] private LevelGenerator levelGenerator;
      [SerializeField] private GameUI gameUI;
      [SerializeField] private LiquidMixEffect liquidMixEffect;
      
      private readonly CheckWinAction checkWinAction = new CheckWinAction();
      private readonly WinAction winAction = new WinAction();
      private Map map;
      private FlaskMixer flaskMixer;
      public bool IsLevelComplete { get; private set; }

      private void Awake()
      {
         flaskMixer = new FlaskMixer(this, liquidMixEffect);
      }

      public void CheckWin()
      {
         if (checkWinAction.Check(levelGenerator.Flasks))
         {
            IsLevelComplete = true;
            winAction.Win(gameUI);
         }
      }

      private void Start()
      {
         StartNewGame();
      }

      public void UndoMove()
      {
         flaskMixer.UndoLastMove();
      }

      public void RestartLevel()
      {
         flaskMixer.Clear();
         levelGenerator.Clear();
         levelGenerator.ReGenerateMap(flaskMixer.FlaskSelect, map);
      }

      public void StartNewGame()
      {
         flaskMixer.Clear();
         levelGenerator.Clear();
         map = levelGenerator.Generate(flaskMixer.FlaskSelect);
         IsLevelComplete = false;
      }
   }
}
