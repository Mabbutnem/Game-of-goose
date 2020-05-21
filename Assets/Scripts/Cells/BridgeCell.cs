using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCell : ACell
{
   private static readonly int DEST_INDEX = 12;

   public BridgeCell(int index, Transform waypoint) : base(index, waypoint) { }

   public override void OnMoved(AGoose goose)
   {
      MoveManager.MoveAtAfter(goose, DEST_INDEX);
   }

   public override void TryEndTurn(AGoose goose)
   {
      //Do Nothing..
   }
}
