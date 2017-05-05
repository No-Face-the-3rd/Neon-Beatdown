using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource[] audioSources;

    public void VolumeSliderController()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = slider.value;
            if (audioSource.volume < slider.value)
                audioSource.volume = audioSource.volume;
        }
    }
}