using System;
using Scripts.GameLogic.Actions;
using Scripts.GameLogic.Entity;
using Scripts.Generator;
using Scripts.UI;
using UnityEngine;

namespace Scripts.GameLogic
{
   public class Game : MonoBehaviour
   {
      [SerializeField] private LevelGenerator levelGenerator;
      [SerializeField] private GameUI gameUI;
      [SerializeField] private LiquidMixSound liquidMixSound;
      private FlaskMixer flaskMixer;
      private readonly CheckWinAction checkWinAction = new CheckWinAction();
      private readonly WinAction winAction = new WinAction();
      private Map map;
      public bool IsLevelComplete { get; private set; }

      private void Awake()
      {
         flaskMixer = new FlaskMixer(this, liquidMixSound);
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
         map = levelGenerator.Generate(flaskMixer.FlaskSelect, 6, 2);
         IsLevelComplete = false;
      }
   }
}
