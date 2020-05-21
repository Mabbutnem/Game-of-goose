using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotGoose : AGoose
{
   private static readonly float WAITING_TIME = 1f;

   public BotGoose(Transform transform) : base(transform) { }

   public override void TakeTurn()
   {
      DiceManager.RollAfter(WAITING_TIME);
   }
}
