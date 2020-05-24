using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
#region Variables
    // Private.
    private static AudioSource walkAudioSource;

    // Public.
    public enum Sound
   {
      Hover,
      Click,
      DiceRoll,
      Victory,
      NeutralCell,
      GoodCell,
      BadCell,
    }
    #endregion

#region Implementation
    /// <summary>
    ///  Play a sound by instanciating a sound gameobject.
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="volume"></param>
    public static void PlaySound(Sound sound, float volume)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound), volume);
    }

    /// <summary>
    /// Get the audio clip linked to a specific Sound.
    /// </summary>
    /// <param name="sound"></param>
    /// <returns></returns>
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" + sound + " not found !");
        return null;
    }
   #endregion
}
