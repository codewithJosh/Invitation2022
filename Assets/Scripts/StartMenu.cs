using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private GameObject skinLockedHUD;
    [SerializeField] private GameObject skinPreviousUIButton;
    [SerializeField] private GameObject skinNextUIButton;
    [SerializeField] private GameObject mapLockedHUD;
    [SerializeField] private GameObject mapPreviousUIButton;
    [SerializeField] private GameObject mapNextUIButton;
    [SerializeField] private Image skinHUD;
    [SerializeField] private Image skinsTitleHUD;
    [SerializeField] private Image skinUITextHUD;
    [SerializeField] private Image mapHUD;
    [SerializeField] private Image mapsTitleHUD;
    [SerializeField] private Image mapUITextHUD;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private TextMeshProUGUI skinLockedUIText;
    [SerializeField] private TextMeshProUGUI skinUIText;
    [SerializeField] private TextMeshProUGUI mapLockedUIText;
    [SerializeField] private TextMeshProUGUI mapUIText;

    private enum StartMenuStates { idle, start, help, about, quit, select };
    private StartMenuStates startMenuState = StartMenuStates.idle;

    private int isFemale;
    private int lastSkinUsed;
    private int lastMapUsed;
    private int unlockedSkins;
    private int unlockedMaps;

    void Start()
    {

        FindObjectOfType<PlayerManager>().LoadPlayer();

        lastSkinUsed = FindObjectOfType<PlayerManager>().lastSkinUsed;
        lastMapUsed = FindObjectOfType<PlayerManager>().lastMapUsed;
        unlockedSkins = FindObjectOfType<PlayerManager>().unlockedSkins;
        unlockedMaps = FindObjectOfType<PlayerManager>().unlockedMaps;

    }

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

            FindObjectOfType<PlayerManager>().lastSkinUsed = lastSkinUsed;
            FindObjectOfType<PlayerManager>().lastMapUsed = lastMapUsed;
            FindObjectOfType<PlayerManager>().unlockedSkins = unlockedSkins;
            FindObjectOfType<PlayerManager>().unlockedMaps = unlockedMaps;

            if (lastSkinUsed <= unlockedSkins)
            {

                skinsTitleHUD.sprite = resources[1];
                skinUITextHUD.sprite = resources[5];
                skinLockedHUD.SetActive(false);

            }
            else
            {

                skinsTitleHUD.sprite = resources[0];
                skinUITextHUD.sprite = resources[4];
                skinLockedHUD.SetActive(true);

            }

            if (lastMapUsed <= unlockedMaps)
            {

                mapsTitleHUD.sprite = resources[3];
                mapUITextHUD.sprite = resources[5];
                mapLockedHUD.SetActive(false);

            }
            else
            {

                mapsTitleHUD.sprite = resources[2];
                mapUITextHUD.sprite = resources[4];
                mapLockedHUD.SetActive(true);

            }

            if (lastSkinUsed == 0)
            {

                skinPreviousUIButton.GetComponent<Button>().interactable = false;
                skinPreviousUIButton.GetComponent<Image>().sprite = resources[6];

            }
            else
            {

                skinPreviousUIButton.GetComponent<Button>().interactable = true;
                skinPreviousUIButton.GetComponent<Image>().sprite = resources[7];

            }

            if (lastSkinUsed < 5)
            {

                skinNextUIButton.GetComponent<Button>().interactable = true;
                skinNextUIButton.GetComponent<Image>().sprite = resources[9];

            }
            else
            {
                
                skinNextUIButton.GetComponent<Button>().interactable = false;
                skinNextUIButton.GetComponent<Image>().sprite = resources[8];

            }

            if (lastMapUsed == 0)
            {

                mapPreviousUIButton.GetComponent<Button>().interactable = false;
                mapPreviousUIButton.GetComponent<Image>().sprite = resources[6];

            }
            else
            {

                mapPreviousUIButton.GetComponent<Button>().interactable = true;
                mapPreviousUIButton.GetComponent<Image>().sprite = resources[7];

            }

            if (lastMapUsed < 5)
            {

                mapNextUIButton.GetComponent<Button>().interactable = true;
                mapNextUIButton.GetComponent<Image>().sprite = resources[9];

            }
            else
            {

                mapNextUIButton.GetComponent<Button>().interactable = false;
                mapNextUIButton.GetComponent<Image>().sprite = resources[8];
            }

            if (SimpleInput.GetButtonDown("OnPreviousSkin"))
            {

                if (lastSkinUsed != 0)
                {

                    lastSkinUsed--;

                }

            }

            if (SimpleInput.GetButtonDown("OnNextSkin"))
            {
                
                if (lastSkinUsed < 5)
                {

                    lastSkinUsed++;

                }

            }

            if (SimpleInput.GetButtonDown("OnPreviousMap"))
            {

                if (lastMapUsed != 0)
                {

                    lastMapUsed--;

                }

            }

            if (SimpleInput.GetButtonDown("OnNextMap"))
            {

                if (lastMapUsed < 5)
                {

                    lastMapUsed++;

                }

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
