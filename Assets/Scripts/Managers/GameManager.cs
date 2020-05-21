using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   #region Variables
   //Singleton
   private static GameManager instance;

   //Readonly
   private static readonly string[] GOOSE_INITIALIZER = new string[]
   {
      "Player",
      "Bot",
      "Bot",
      "Bot",
   };

   //Public
   public static int NbGoose { get; set; } = 4;
   public static AGoose[] Gooses { get; private set; }
   public static int CurrentGooseIndex { get; private set; } = 0;

   //Private
   private static bool triggerNextTurn = false;

   //Events
   public static event Action OnBeginGame = () => { };
   public static event Action<AGoose> OnBeginTurn = goose => { };
   public static event Action<AGoose> OnEndTurn = goose => { };
   public static event Action OnEndGame = () => { };
   #endregion

   #region Unity Callbacks
   private void Awake()
   {
      //SINGLETON
      if (instance == null) instance = this;
      else GameObjectUtils.DebugErrorStop("Multiple instances of singleton.");
      //SINGLETON

      Transform gooseGameObjectTransform = GameObjectUtils.Find(">>> GOOSE").transform;
      Gooses = new AGoose[NbGoose];
      for(int i = 0; i < NbGoose; i++)
      {
         Gooses[i] = GenerateGoose(i, gooseGameObjectTransform.GetChild(i));
      }

      CurrentGooseIndex = -1;
   }

   private void Start()
   {
      BeginGame();
   }

   private void FixedUpdate()
   {
      if(triggerNextTurn)
      {
         triggerNextTurn = false;
         BeginTurn();
      }
   }
   #endregion

   #region Event Methods
   public static void BeginGame()
   {
      OnBeginGame();
      triggerNextTurn = true;
   }

   public static void BeginTurn()
   {
      CurrentGooseIndex++;
      if (CurrentGooseIndex >= Gooses.Length) CurrentGooseIndex = 0;
      AGoose currentGoose = Gooses[CurrentGooseIndex];

      OnBeginTurn(currentGoose);

      //If a gooze must pass its turn
      if (currentGoose.TurnInfo.NbTurnToPass > 0)
      {
         currentGoose.TurnInfo = TurnInfo.Builder().NbTurnToPass(currentGoose.TurnInfo.NbTurnToPass - 1).Build();
         EndTurn();
         return;
      }

      currentGoose.TakeTurn();
   }

   public static void EndTurn()
   {
      OnEndTurn(Gooses[CurrentGooseIndex]);
      triggerNextTurn = true;
   }

   public static void EndGame()
   {
      OnEndGame();
      Debug.Log("Player " + CurrentGooseIndex + " Wins !");
   }
   #endregion

   #region Init Gooses
   private AGoose GenerateGoose(int gooseIndex, Transform transform)
   {
      if (GOOSE_INITIALIZER[gooseIndex] == "Player")
      {
         return new PlayerGoose(transform);
      }
      else if (GOOSE_INITIALIZER[gooseIndex] == "Bot")
      {
         return new BotGoose(transform);
      }
      else
      {
         return new PlayerGoose(transform);
      }
   }
   #endregion
}