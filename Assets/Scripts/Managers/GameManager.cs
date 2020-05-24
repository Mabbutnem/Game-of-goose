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
   private static readonly int NB_GOOSES = 4;

   //Public
   public static int NbPlayerGoose { get; set; } = 0;
   public static AGoose[] Gooses { get; private set; }
   public static int CurrentGooseIndex { get; private set; } = 0;

   //Private
   private static bool triggerNextTurn = false;
   private static Sprite[] goosesSprites;
   private static SpriteRenderer currentGooseRenderer;

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
      Gooses = new AGoose[NB_GOOSES];
      goosesSprites = new Sprite[NB_GOOSES];
      for (int i = 0; i < Gooses.Length; i++)
      {
         Gooses[i] = GenerateGoose(i, gooseGameObjectTransform.GetChild(i));
         goosesSprites[i] = gooseGameObjectTransform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
      }

      CurrentGooseIndex = -1;

      currentGooseRenderer = GameObjectUtils.Find("Current Goose").GetComponent<SpriteRenderer>();
   }

   private void OnDestroy()
   {
      //Empty events
      OnBeginGame = () => { };
      OnBeginTurn = goose => { };
      OnEndTurn =   goose => { };
      OnEndGame =   () => { };
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

   #region Initialisation
   private AGoose GenerateGoose(int gooseIndex, Transform transform)
   {
      if(gooseIndex < NbPlayerGoose)
      {
         return new PlayerGoose(transform);
      }
      else
      {
         return new BotGoose(transform);
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
      currentGooseRenderer.sprite = goosesSprites[CurrentGooseIndex]; //Refresh current goose sprite

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
      Debug.Log("Player " + CurrentGooseIndex + " Wins !");
      OnEndGame();
   }
   #endregion
}