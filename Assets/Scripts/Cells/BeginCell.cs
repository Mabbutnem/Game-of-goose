using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginCell : ACell
{
   public BeginCell(int index, Transform waypoint) : base(index, waypoint)
   {
      onMovedSound = SoundManager.Sound.NeutralCell;
   }

   public override void OnMoved(AGoose goose)
   {
      goose.TurnInfo = TurnInfo.Builder().OnBeginCell().Build();
   }

   public override void TryEndTurn(AGoose goose)
   {
      GameManager.EndTurn();
   }
}
