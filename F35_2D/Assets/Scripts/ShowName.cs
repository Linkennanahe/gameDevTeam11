using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowName : MonoBehaviour
{
    string newName;
    [SerializeField]
    TextMeshProUGUI textMeshPro;
    private void Awake()
    {
        newName=SaveName.name;
        textMeshPro.text = newName;
    }

    
}
