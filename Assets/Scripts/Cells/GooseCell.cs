using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooseCell : ACell
{
   public GooseCell(int index, Transform waypoint) : base(index, waypoint) { }

   public override void OnMoved(AGoose goose)
   {
      MoveManager.MoveAfter(goose, goose.LastDiceResult.Total);
   }

   public override void TryEndTurn(AGoose goose)
   {
      //Do Nothing..
   }
}
