using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoose : AGoose
{
   public PlayerGoose(Transform transform) : base(transform) { }

   public override void TakeTurn()
   {
      DiceManager.Roll();
   }
}
