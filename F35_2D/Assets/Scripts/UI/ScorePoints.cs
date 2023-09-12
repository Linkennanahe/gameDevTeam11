using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePoints : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoreText , finalPoints;
    public static int counter;
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = counter.ToString();
        finalPoints.text = scoreText.text;
    }

    public static void AddPointToTheScore() { 
    
        counter++;
    }



}
