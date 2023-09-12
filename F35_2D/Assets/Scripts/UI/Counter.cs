using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    int score;
    int displayScore;
    [SerializeField]
    Text text;
    void Start()
    {
        //Both scores start at 0
        score = int.Parse(text.text);
        displayScore = 0;
        StartCoroutine(ScoreUpdater());
    }
    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore >= score)
            {
                displayScore++; //Increment the display score by 1
                text.text = displayScore.ToString(); //Write it to the UI
            }
            yield return new WaitForSeconds(0.5f); // I used .2 secs but you can update it as fast as you want
        }
    }


}
