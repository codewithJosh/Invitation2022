using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Animator animator;
    private enum startMenuState { idle, start, help, about, quit };
    private startMenuState state = startMenuState.idle;

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnIdleButton"))
        {

            state = startMenuState.idle;

        }
        if (SimpleInput.GetButtonDown("OnStartButton"))
        {

            state = startMenuState.start;

        }
        if (SimpleInput.GetButtonDown("OnHelpButton"))
        {

            state = startMenuState.help;

        }
        if (SimpleInput.GetButtonDown("OnAboutButton"))
        {

            state = startMenuState.about;

        }
        if (SimpleInput.GetButtonDown("OnQuitButton"))
        {

            state = startMenuState.quit;

        }

        animator.SetInteger("state", (int) state);

    }

}
