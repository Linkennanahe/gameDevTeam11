using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{


    public AudioMixer musicmixer,soundMixer;
    public Toggle musicToggle, soundToggle;
    public static bool musicBool, soundBool;
    public static float globalVolume;
    private void Start()
    {
        musicBool = true;
        soundBool = true;
        //globalVolume = -80f;
    }

    public void SetVolume(float volume) {

        musicmixer.SetFloat("volume",volume);
        soundMixer.SetFloat("sound", volume);
        globalVolume= volume;

    }

    public void MuteMusic()
    {
        if (musicToggle.isOn)
        {
            musicBool = true;
            musicmixer.SetFloat("volume", 0);

        }
        else
        {
            musicBool = false;
            musicmixer.SetFloat("volume", -80);
        }
    }


    public void MuteSound()
    {
        if (soundToggle.isOn)
        {
            soundBool = true;
            soundMixer.SetFloat("sound", 0);
        }
        else
        {
            soundBool= false;
            soundMixer.SetFloat("sound", -80);
        }
    }
   
}
