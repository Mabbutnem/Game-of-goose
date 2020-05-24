using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCell : ACell
{
   public EndCell(int index, Transform waypoint) : base(index, waypoint)
   {
      onMovedSound = SoundManager.Sound.NeutralCell;
   }

   public override void OnMoved(AGoose goose)
   {
      //Do Nothing...
   }

   public override void TryEndTurn(AGoose goose)
   {
      GameManager.EndGame();
   }
}
