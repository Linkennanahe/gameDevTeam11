using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulateSettingsMenu : MonoBehaviour
{


    public Toggle mtoggle,stoggle;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

        slider.value = SettingsMenu.globalVolume;

        if (SettingsMenu.musicBool)
            mtoggle.isOn = true;
        else
            mtoggle.isOn = false;
        
        
        if (SettingsMenu.soundBool)
            stoggle.isOn = true;
        else
            stoggle.isOn = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
