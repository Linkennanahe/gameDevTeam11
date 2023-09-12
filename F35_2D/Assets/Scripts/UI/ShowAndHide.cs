using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;


    public void ShowTheObject() { 
        gameObject.SetActive(true);
        Time.timeScale = 0;
    
    }

    public void HideTheObject() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
