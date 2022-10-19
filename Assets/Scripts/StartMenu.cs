using UnityEngine;

public class StartMenu : MonoBehaviour
{

    private enum startMenuStates { idle, start, help, about, quit, select, male, female };
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

            startMenuState = startMenuStates.male;

        }
        if (SimpleInput.GetButtonDown("OnFemale"))
        {

            startMenuState = startMenuStates.female;

        }

        FindObjectOfType<GameManager>().GetAnimator.SetInteger("startMenuState", (int)startMenuState);

    }

}
