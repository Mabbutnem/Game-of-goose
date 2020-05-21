using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceResult
{
   public int Dice1 { get; }
   public int Dice2 { get; }
   public int Total { get; }

   public DiceResult(int dice1, int dice2)
   {
      Dice1 = dice1;
      Dice2 = dice2;
      Total = Dice1 + Dice2;
   }

   public bool HasDone(params int[] results)
   {
      foreach(int result in results)
      {
         if(result != Dice1 && result != Dice2)
         {
            return false;
         }
      }

      return true;
   }
}
