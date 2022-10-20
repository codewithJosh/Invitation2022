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

            OnAnimateFromStartMenu(startMenuStates.idle);

        }
        if (SimpleInput.GetButtonDown("OnStart"))
        {

            OnAnimateFromStartMenu(startMenuStates.start);

        }
        if (SimpleInput.GetButtonDown("OnHelp"))
        {

            OnAnimateFromStartMenu(startMenuStates.help);

        }
        if (SimpleInput.GetButtonDown("OnAbout"))
        {

            OnAnimateFromStartMenu(startMenuStates.about);

        }
        if (SimpleInput.GetButtonDown("OnQuit"))
        {

            OnAnimateFromStartMenu(startMenuStates.quit);

        }
        
        if (startMenuState == startMenuStates.start)
        {

            if (SimpleInput.GetButtonDown("OnMale"))
            {

                FindObjectOfType<GameManager>().OnAnimate("male");
                int countdown = 1;
                StartCoroutine(SelectSectionToStart(countdown));

            }

            if (SimpleInput.GetButtonDown("OnFemale"))
            {

                FindObjectOfType<GameManager>().OnAnimate("female");
                int countdown = 1;
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

    }

    IEnumerator SelectSectionToStart(int _countdown)
    {

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        startMenuState = startMenuStates.select;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int)startMenuState);

    }

    private void OnAnimateFromStartMenu(startMenuStates _startMenuState)
    {

        startMenuState = _startMenuState;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int)startMenuState);

    }

}
