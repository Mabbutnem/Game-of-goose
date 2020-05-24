using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotelCell : ACell
{
   private static readonly int TURN_TO_PASS = 1;

   public HotelCell(int index, Transform waypoint) : base(index, waypoint)
   {
      onMovedSound = SoundManager.Sound.BadCell;
   }

   public override void OnMoved(AGoose goose)
   {
      goose.TurnInfo = TurnInfo.Builder().NbTurnToPass(TURN_TO_PASS).Build();
   }

   public override void TryEndTurn(AGoose goose)
   {
      GameManager.EndTurn();
   }
}
