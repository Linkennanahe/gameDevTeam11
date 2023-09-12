using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{

    public static bool tutorialMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToTutorialsMode() {
    
        tutorialMode = true;
        Time.timeScale = 0f;
    }

    public void SwitchToTutorialsModeoFF()
    {

        tutorialMode = false;
        Time.timeScale = 0f;
    }


}
