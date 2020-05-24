using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSubscriber : MonoBehaviour
{
   #region Unity Callbacks
   private void Awake()
   {
      GameManager.OnEndGame += VictorySound;
      DiceManager.OnRollBegin += DiceSound;
   }

   private void OnDestroy()
   {
      GameManager.OnEndGame -= VictorySound;
      DiceManager.OnRollBegin -= DiceSound;
   }
   #endregion

   #region Methods
   private void VictorySound()
   {
      SoundManager.PlaySound(SoundManager.Sound.Victory, 1f);
   }

   private void DiceSound()
   {
      SoundManager.PlaySound(SoundManager.Sound.DiceRoll, 1f);
   }
   #endregion
}
