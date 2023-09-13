using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject mainMenu, loadingMenu;
    [SerializeField]
    float timeOfLoading;

    void Start() {

        StartCoroutine(SwitchUI(timeOfLoading));
    
    }


    IEnumerator SwitchUI(float time) { 
        yield return new WaitForSeconds(time);

        if (mainMenu != null && loadingMenu != null) {
            mainMenu.SetActive(true);
            loadingMenu.SetActive(false);   
        
        }
    
    
    }

}
