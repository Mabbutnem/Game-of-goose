using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGoose
{
   public int CurrentCellIndex { get; set; } = 0;
   public Transform Transform { get; }
   public TurnInfo TurnInfo { get; set; }
   public DiceResult LastDiceResult { get; private set; }

   public AGoose(Transform transform)
   {
      Transform = transform;
      TurnInfo = TurnInfo.Builder().OnBeginCell().Build();
   }

   public abstract void TakeTurn();

   public void MoveByDice(DiceResult diceResult)
   {
      LastDiceResult = diceResult;

      //If the player is in the well
      if (TurnInfo.InWell)
      {
         if(!diceResult.HasDone(6)) { GameManager.EndTurn(); return; } //Pass turn..
         TurnInfo = TurnInfo.Builder().Build();
      }

      //If the player is on begin cell
      if (TurnInfo.OnBeginCell)
      {
         //6 and 3
         if (diceResult.HasDone(6, 3))
         {
            MoveManager.MoveAt(this, 26, true);
            return;
         }

         //5 and 4
         if (diceResult.HasDone(5, 4))
         {
            MoveManager.MoveAt(this, 53, true);
            return;
         }
      }

      MoveManager.Move(this, diceResult.Total, true);
   }
}
