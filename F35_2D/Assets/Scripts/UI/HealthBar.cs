using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    Slider healthBar;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
        healthBar = GetComponent<Slider>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
