using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellCell : ACell
{
   public WellCell(int index, Transform waypoint) : base(index, waypoint)
   {
      onMovedSound = SoundManager.Sound.BadCell;
   }

   public override void OnMoved(AGoose goose)
   {
      goose.TurnInfo = TurnInfo.Builder().InWell().Build();
   }

   public override void TryEndTurn(AGoose goose)
   {
      GameManager.EndTurn();
   }
}
