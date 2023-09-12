using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    // Start is called before the first frame update
    public static string name;

    [SerializeField]
    InputField textInput;
    [SerializeField]
    Text textMeshPro_alert;

    void Start()
    {
        
    }

    public void ChangeName() { 
    
        name= textInput.text;
        textMeshPro_alert.text= "Saved";
        Debug.Log(name);
        StartCoroutine(CleanText());
    }
    
    IEnumerator CleanText()
    {

        yield return new WaitForSeconds(0.5f);
        textMeshPro_alert.text = "";

    }

}
