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
        DiceRoll,
        BeginTurn,
        Teleport,
        Selection,
        Click,
        Victory,
        PlayerMove,
        TeleportSnake,
        TeleportLadder,
        Walk,
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

    /// <summary>
    /// Play the walk sound by instanciating one instance of a walksound gameobject.
    /// </summary>
    /// <param name="state"></param>
    public static void EnableWalkSound(bool state)
    {
        if (state)
        {
            if (!walkAudioSource)
            {
                GameObject walkSoundGameObject = new GameObject("SoundWalk");
                walkAudioSource = walkSoundGameObject.AddComponent<AudioSource>();
                walkAudioSource.clip = GetAudioClip(Sound.Walk);
                walkAudioSource.loop = true;
                walkAudioSource.volume = 2f;
                PlayRandomPitch(walkAudioSource);
            }
            else
                PlayRandomPitch(walkAudioSource);
        }
        else
            walkAudioSource.Pause();
    }

    /// <summary>
    /// Play a sound by randomising the pitch to get a different sound each time.
    /// </summary>
    /// <param name="audiosource"></param>
    private static void PlayRandomPitch(AudioSource audiosource)
    {
        audiosource.pitch = (Random.value < .5 ? 1 : -1) * Random.Range(1f, 3f);
        audiosource.Play();
    }
    #endregion
}
