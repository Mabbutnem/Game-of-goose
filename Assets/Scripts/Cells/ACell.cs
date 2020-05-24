using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACell
{
   public int Index { get; private set; }
   public Transform Waypoint { get; private set; }
   private AGoose occupant = null;
   public AGoose Occupant
   {
      get { return occupant; }
      set { if (Index != 0) occupant = value; }
   }
   protected SoundManager.Sound onMovedSound = 0;

   public ACell(int index, Transform waypoint)
   {
      Index = index;
      Waypoint = waypoint;
   }

   public abstract void OnMoved(AGoose goose);
   public abstract void TryEndTurn(AGoose goose);

   public void PlayOnMovedSound()
   {
      SoundManager.PlaySound(onMovedSound, 1f);
   }

   public bool Occupied()
   {
      return occupant != null;
   }
}
