using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveName : MonoBehaviour
{
    // Start is called before the first frame update
    public static string name;

    [SerializeField]
    TextMeshProUGUI textMeshPro;
    [SerializeField]
    TextMeshProUGUI textMeshPro_alert;

    void Start()
    {
        
    }

    public void ChangeName() { 
    
        name= textMeshPro.text;
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
