using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsMode : MonoBehaviour
{
    public GameObject[] tutorialsText;
    public GameObject tutorialUi;
    // Start is called before the first frame update
    void Start()
    {
        this.ResetButtonOrder();
        if (Tutorials.tutorialMode)
            tutorialUi.SetActive(true);

    }

    // Update is called once per frame

    public bool ResetButtonOrderOnFailure = true;
    private Stack<string> _buttonOrder = new Stack<string>();



    private void ResetButtonOrder()
    {
        _buttonOrder.Clear();
        _buttonOrder.Push("D");
        _buttonOrder.Push("A");
        _buttonOrder.Push("W");
        _buttonOrder.Push("S");
        _buttonOrder.Push("Space");


    }

    public void OnButtonPressed(string token)
    {
        if (_buttonOrder.Peek() == token)
        {
            _buttonOrder.Pop();
            if (_buttonOrder.Count == 0)
            {
                //do what gets done at end of button order
            }
        }
        else
        {
            if (this.ResetButtonOrderOnFailure)
            {
                this.ResetButtonOrder();
            }
        }
    }

    private void Update()
    {
        
        //lets say that the button order has to do with XBOX controller inputs
        //and you have those inputs configured as the specified names in the input manager

        if (Input.GetKeyDown(KeyCode.D))
        {
            //this.OnButtonPressed("D");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(true);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);


        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //this.OnButtonPressed("A");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(true);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);


        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //this.OnButtonPressed("W");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(true);
            tutorialsText[4].SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //this.OnButtonPressed("S");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(true);


        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //this.OnButtonPressed("Space");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);
            tutorialUi.SetActive(false);
            Time.timeScale = 1.0f;
            return;

        }


    }


   
}
