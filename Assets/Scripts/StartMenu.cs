using System.Collections;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private Sprite[] resources;

    private enum StartMenuStates { idle, start, help, about, quit, select };
    private StartMenuStates startMenuState = StartMenuStates.idle;

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnIdle"))
        {

            OnAnimateFromStartMenu(StartMenuStates.idle);

        }
        if (SimpleInput.GetButtonDown("OnStart"))
        {

            OnAnimateFromStartMenu(StartMenuStates.start);

        }
        if (SimpleInput.GetButtonDown("OnHelp"))
        {

            OnAnimateFromStartMenu(StartMenuStates.help);

        }
        if (SimpleInput.GetButtonDown("OnAbout"))
        {

            OnAnimateFromStartMenu(StartMenuStates.about);

        }
        if (SimpleInput.GetButtonDown("OnQuit"))
        {

            OnAnimateFromStartMenu(StartMenuStates.quit);

        }
        
        if (startMenuState == StartMenuStates.start)
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

        if (startMenuState == StartMenuStates.select)
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

        startMenuState = StartMenuStates.select;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int)startMenuState);

    }

    private void OnAnimateFromStartMenu(StartMenuStates _startMenuState)
    {

        startMenuState = _startMenuState;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int)startMenuState);

    }

}
