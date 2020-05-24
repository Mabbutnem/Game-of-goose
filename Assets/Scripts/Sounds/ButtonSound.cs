using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
   public void HoverSound()
   {
      SoundManager.PlaySound(SoundManager.Sound.Hover, 1f);
   }

   public void ClickSound()
   {
      SoundManager.PlaySound(SoundManager.Sound.Click, 1f);
   }
}
