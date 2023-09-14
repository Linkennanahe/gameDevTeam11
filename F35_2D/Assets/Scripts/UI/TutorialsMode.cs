using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsMode : MonoBehaviour
{
    public GameObject[] tutorialsText;
    public GameObject tutorialUi;
    private bool tutorials, w, s, a, d, eOrq, space;
    // Start is called before the first frame update
    void Start()
    {
        w=false; s=false; a=false; 
        d=false; eOrq=false; space=false;

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

        if (Input.GetKeyDown(KeyCode.W)&& Tutorials.tutorialMode)
        {

            //this.OnButtonPressed("D");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(true);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);
            tutorialsText[5].SetActive(false);
            w = true;
            Tutorials.tutorialMode = false;

        }
        if (Input.GetKeyDown(KeyCode.S)&& w)
        {
            //this.OnButtonPressed("A");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(true);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);
            tutorialsText[4].SetActive(false);
            w = false;
            s =true;


        }
        if (Input.GetKeyDown(KeyCode.A)&& s)
        {
            //this.OnButtonPressed("W");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(true);
            tutorialsText[4].SetActive(false);
            tutorialsText[5].SetActive(false);
            s = false;
            a =true;

        }
        if (Input.GetKeyDown(KeyCode.D) && a)
        {
            //this.OnButtonPressed("S");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(true);
            tutorialsText[5].SetActive(false);
            a = false;
            d =true;

        }
        if ((Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.E)) && d)
        {
            //this.OnButtonPressed("Q" OR "E");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);
            tutorialsText[5].SetActive(true);
            d = false;
            eOrq =true;

        }
        if (Input.GetKeyDown(KeyCode.Space) && eOrq)
        {
            //this.OnButtonPressed("Space");
            tutorialsText[0].SetActive(false);
            tutorialsText[1].SetActive(false);
            tutorialsText[2].SetActive(false);
            tutorialsText[3].SetActive(false);
            tutorialsText[4].SetActive(false);
            tutorialsText[5].SetActive(false);
            tutorialUi.SetActive(false);
            Time.timeScale = 1.0f;
            eOrq=false; 
            return;

        }


    }


   
}
