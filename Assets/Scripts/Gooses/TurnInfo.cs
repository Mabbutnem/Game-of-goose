using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInfo
{
   #region Builder
   public class TurnInfoBuilder
   {
      private bool onBeginCell = false;
      private int nbTurnToPass = 0;
      private bool inWell = false;
      private bool hasWon = false;

      public TurnInfo Build() { return new TurnInfo(onBeginCell, nbTurnToPass, inWell, hasWon); }

      public TurnInfoBuilder OnBeginCell()
      {
         this.onBeginCell = true;
         return this;
      }

      public TurnInfoBuilder NbTurnToPass(int nbTurnToPass)
      {
         this.nbTurnToPass = nbTurnToPass;
         return this;
      }

      public TurnInfoBuilder InWell()
      {
         inWell = true;
         return this;
      }

      public TurnInfoBuilder HasWon()
      {
         hasWon = true;
         return this;
      }
   }
   public static TurnInfoBuilder Builder() { return new TurnInfoBuilder(); }
   #endregion

   #region Class
   public bool OnBeginCell { get; }
   public int NbTurnToPass { get; }
   public bool InWell { get; }
   public bool HasWon { get; }

   private TurnInfo(bool onBeginCell, int nbTurnToPass, bool inWell, bool hasWon)
   {
      OnBeginCell = onBeginCell;
      NbTurnToPass = nbTurnToPass;
      InWell = inWell;
      HasWon = hasWon;
   }
   #endregion
}
