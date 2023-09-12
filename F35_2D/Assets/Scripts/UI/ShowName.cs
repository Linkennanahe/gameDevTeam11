using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowName : MonoBehaviour
{
    string newName;
    [SerializeField]
    Text text;
    private void Awake()
    {
        newName=SaveName.name;
        text.text = newName;
    }

    
}
