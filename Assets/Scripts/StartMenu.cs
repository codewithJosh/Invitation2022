using System;
using System.Collections;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private Sprite[] resources;

    private enum startMenuStates { idle, start, help, about, quit, select };
    private startMenuStates startMenuState = startMenuStates.idle;

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnIdle"))
        {

            startMenuState = startMenuStates.idle;

        }
        if (SimpleInput.GetButtonDown("OnStart"))
        {

            startMenuState = startMenuStates.start;

        }
        if (SimpleInput.GetButtonDown("OnHelp"))
        {

            startMenuState = startMenuStates.help;

        }
        if (SimpleInput.GetButtonDown("OnAbout"))
        {

            startMenuState = startMenuStates.about;

        }
        if (SimpleInput.GetButtonDown("OnQuit"))
        {

            startMenuState = startMenuStates.quit;

        }
        
        if (startMenuState == startMenuStates.start)
        {

            if (SimpleInput.GetButtonDown("OnMale"))
            {

                FindObjectOfType<GameManager>().OnAnimate("male");
                int countdown = 2;
                StartCoroutine(SelectSectionToStart(countdown));

            }

            if (SimpleInput.GetButtonDown("OnFemale"))
            {

                FindObjectOfType<GameManager>().OnAnimate("female");
                int countdown = 2;
                StartCoroutine(SelectSectionToStart(countdown));

            }

        }

        if (startMenuState == startMenuStates.select)
        {

            if (SimpleInput.GetButtonDown("OnPreviousSkin"))
            {

                

            }

            if (SimpleInput.GetButtonDown("OnNextSkin"))
            {

                

            }

            if (SimpleInput.GetButtonDown("OnPreviousMap"))
            {



            }

            if (SimpleInput.GetButtonDown("OnNextMap"))
            {



            }

            if (SimpleInput.GetButtonDown("OnRun"))
            {



            }

        }


        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int)startMenuState);

    }

    IEnumerator SelectSectionToStart(int _countdown)
    {

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        startMenuState = startMenuStates.select;

    }

}
