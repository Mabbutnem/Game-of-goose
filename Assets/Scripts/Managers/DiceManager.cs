using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
   #region Variables
   //Singleton
   private static DiceManager instance;

   //Readonly
   private static readonly int NB_FACE_ROLL = 12;
   private static readonly float FACE_ROLL_FREQ = 0.1f;

   //Private
   private static SpriteRenderer dice1;
   private static SpriteRenderer dice2;
   private static DiceResult diceResult = new DiceResult(0, 0);

   //Event
   public static event Action<DiceResult> OnRollEnd = dr => { };

   //Editor
   [SerializeField] protected Sprite[] diceFaceSprites = new Sprite[6];
   #endregion

   #region Unity Callbacks
   private void Awake()
   {
      //SINGLETON
      if (instance == null) instance = this;
      else GameObjectUtils.DebugErrorStop("Multiple instances of singleton.");
      //SINGLETON

      dice1 = GameObjectUtils.Find("Dice 1").GetComponent<SpriteRenderer>();
      dice2 = GameObjectUtils.Find("Dice 2").GetComponent<SpriteRenderer>();

      //Subsribe to game events
      GameManager.OnBeginTurn += SubMoveByDiceWhenRollEnd;
      GameManager.OnEndTurn += UnsubMoveByDiceWhenRollEnd;
   }

   private void OnDestroy()
   {
      //Unsubsribe to game events
      GameManager.OnBeginTurn -= SubMoveByDiceWhenRollEnd;
      GameManager.OnEndTurn -= UnsubMoveByDiceWhenRollEnd;
   }

   private void SubMoveByDiceWhenRollEnd(AGoose goose) { OnRollEnd += goose.MoveByDice; }
   private void UnsubMoveByDiceWhenRollEnd(AGoose goose) { OnRollEnd -= goose.MoveByDice; }
   #endregion

   #region Methods
   public static void Roll()
   {
      instance.StartCoroutine(instance.RollRoutine());
   }

   public static void RollAfter(float seconds)
   {
      instance.StartCoroutine(instance.RollAfterRoutine(seconds));
   }
   #endregion

   #region Routines
   protected IEnumerator RollRoutine()
   {
      diceResult = new DiceResult(Random.Range(1, 7), Random.Range(1, 7));
      for (int i = 0; i < NB_FACE_ROLL; i++)
      {
         dice1.sprite = diceFaceSprites[Random.Range(0, 6)];
         dice2.sprite = diceFaceSprites[Random.Range(0, 6)];
         yield return new WaitForSeconds(FACE_ROLL_FREQ);
      }
      dice1.sprite = diceFaceSprites[diceResult.Dice1 - 1];
      dice2.sprite = diceFaceSprites[diceResult.Dice2 - 1];
      OnRollEnd(diceResult);
   }

   protected IEnumerator RollAfterRoutine(float seconds)
   {
      yield return new WaitForSeconds(seconds);
      Roll();
   }
   #endregion
}
