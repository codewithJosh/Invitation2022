using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Animator animator;
    private enum startMenuStates { idle, start, help, about, quit, select, male, female };
    private startMenuStates startMenuState = startMenuStates.idle;

    void Update() 
    {

        if (SimpleInput.GetButtonDown("OnIdleButton"))
        {

            startMenuState = startMenuStates.idle;

        }
        if (SimpleInput.GetButtonDown("OnStartButton"))
        {

            startMenuState = startMenuStates.start;

        }
        if (SimpleInput.GetButtonDown("OnHelpButton"))
        {

            startMenuState = startMenuStates.help;

        }
        if (SimpleInput.GetButtonDown("OnAboutButton"))
        {

            startMenuState = startMenuStates.about;

        }
        if (SimpleInput.GetButtonDown("OnQuitButton"))
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

        animator.SetInteger("startMenuState", (int) startMenuState);

    }

    public Animator GetAnimator
    {

        get { return animator; }

    }

}
