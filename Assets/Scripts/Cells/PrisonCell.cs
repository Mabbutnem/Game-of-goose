using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonCell : ACell
{
   private static readonly int TURN_TO_PASS = 2;

   public PrisonCell(int index, Transform waypoint) : base(index, waypoint) { }

   public override void OnMoved(AGoose goose)
   {
      goose.TurnInfo = TurnInfo.Builder().NbTurnToPass(TURN_TO_PASS).Build();
   }

   public override void TryEndTurn(AGoose goose)
   {
      GameManager.EndTurn();
   }
}
