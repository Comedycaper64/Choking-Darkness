using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    
    [SerializeField] GameObject tutorial1;
    [SerializeField] GameObject tutorialHelpUI;
    [SerializeField] GameObject tutorial2;
    [SerializeField] private bool tutorial1Open;
    public bool tutorialOpen = false;
    public bool canOpenTutorial = true; 
    private bool tutorial2Open = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canOpenTutorial)
        {
            if (Input.GetKeyDown(KeyCode.T) && tutorialOpen)
            {
                tutorial1.SetActive(false);
                tutorial2.SetActive(false);
                tutorial1Open = false;
                tutorial2Open = false;
                tutorialOpen = false;
                tutorialHelpUI.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.T) && !tutorialOpen)
            {
                tutorial1.SetActive(true);
                tutorial1Open = true;
                tutorialOpen = true;
                tutorialHelpUI.SetActive(false);
            }

            if (tutorial1Open && (Input.GetKeyDown(KeyCode.D)))
            {
                tutorial1.SetActive(false);
                tutorial2.SetActive(true);
                tutorial1Open = false;
                tutorial2Open = true;
            }
            else if (tutorial2Open && (Input.GetKeyDown(KeyCode.A)))
            {
                tutorial1.SetActive(true);
                tutorial2.SetActive(false);
                tutorial1Open = true;
                tutorial2Open = false;
            }
        }
        else
        {
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            tutorial1Open = false;
            tutorial2Open = false;
            tutorialOpen = false;
            tutorialHelpUI.SetActive(true);
        }
}
}
