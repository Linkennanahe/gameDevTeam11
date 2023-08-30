using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Counter : MonoBehaviour
{
    int score;
    int displayScore;
    [SerializeField]
    TextMeshProUGUI textMeshPro;
    void Start()
    {
        //Both scores start at 0
        score = int.Parse(textMeshPro.text);
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
                textMeshPro.text = displayScore.ToString(); //Write it to the UI
            }
            yield return new WaitForSeconds(0.5f); // I used .2 secs but you can update it as fast as you want
        }
    }


}
