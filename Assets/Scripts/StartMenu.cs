using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private Image skinHUD;
    [SerializeField] private Image skinsTitleHUD;
    [SerializeField] private Image skinUITextHUD;
    [SerializeField] private Image skinLockedHUD;
    [SerializeField] private Image skinPreviousUIButton;
    [SerializeField] private Image skinNextUIButton;
    [SerializeField] private Image mapHUD;
    [SerializeField] private Image mapsTitleHUD;
    [SerializeField] private Image mapUITextHUD;
    [SerializeField] private Image mapLockedHUD;
    [SerializeField] private Image mapPreviousUIButton;
    [SerializeField] private Image mapNextUIButton;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private TextMeshProUGUI skinLockedUIText;
    [SerializeField] private TextMeshProUGUI skinUIText;
    [SerializeField] private TextMeshProUGUI mapLockedUIText;
    [SerializeField] private TextMeshProUGUI mapUIText;

    private enum StartMenuStates { idle, start, help, about, quit, select };
    private StartMenuStates startMenuState = StartMenuStates.idle;

    private int isFemale;

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

                isFemale = 0;
                FindObjectOfType<GameManager>().OnAnimate("male");
                int countdown = 1;
                StartCoroutine(SelectSectionToStart(countdown));

            }

            if (SimpleInput.GetButtonDown("OnFemale"))
            {

                isFemale = 1;
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
