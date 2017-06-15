using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton
public class AudioManager : MonoBehaviour {
    public AudioSource menuAudioSource;
    public AudioSource sfxAudioSource;
    
    public static AudioManager instance = null;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        // If there isn't an instance of AudioManager, make one
        if (instance == null)
            instance = this;
        // If an instance already exists, destroy it so only one there can only be one instance
        else if (instance != this)
            Destroy(gameObject);
        // Don't destroy so AudioManager isn't destroyed between scenes (or when reloading scenes)
        DontDestroyOnLoad(gameObject);
    }
}