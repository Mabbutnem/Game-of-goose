using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCell : ACell
{
   private static readonly int DEST_INDEX = 0;

   public DeathCell(int index, Transform waypoint) : base(index, waypoint) { }

   public override void OnMoved(AGoose goose)
   {
      MoveManager.MoveAtAfter(goose, DEST_INDEX);
   }

   public override void TryEndTurn(AGoose goose)
   {
      //Do Nothing..
   }
}
