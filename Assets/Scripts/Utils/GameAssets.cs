using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
#region Variables
    // Private.
    private static GameAssets _instance;
    public static GameAssets instance
    {
        get
        {
            if (_instance == null) _instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _instance;
        }
    }

    // Public.
    public SoundAudioClip[] soundAudioClipArray;
    #endregion

#region Class
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    #endregion

}
