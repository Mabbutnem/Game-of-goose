﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : ACell
{
   private static readonly int DEST_INDEX = 30;

   public MazeCell(int index, Transform waypoint) : base(index, waypoint)
   {
      onMovedSound = SoundManager.Sound.BadCell;
   }

   public override void OnMoved(AGoose goose)
   {
      MoveManager.MoveAtAfter(goose, DEST_INDEX);
   }

   public override void TryEndTurn(AGoose goose)
   {
      //Do Nothing..
   }
}
