using System;
using System.Collections;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

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
