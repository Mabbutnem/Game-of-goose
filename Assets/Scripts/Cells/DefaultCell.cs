using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCell : ACell
{
   public DefaultCell(int index, Transform waypoint) : base(index, waypoint) { }

   public override void OnMoved(AGoose goose)
   {
      goose.TurnInfo = TurnInfo.Builder().Build();
   }

   public override void TryEndTurn(AGoose goose)
   {
      GameManager.EndTurn();
   }
}
