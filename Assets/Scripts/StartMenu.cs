using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button skinPreviousUIButton;
    [SerializeField] private Button skinNextUIButton;
    [SerializeField] private Button mapPreviousUIButton;
    [SerializeField] private Button mapNextUIButton;
    [SerializeField] private GameObject skinLockedHUD;
    [SerializeField] private GameObject mapLockedHUD;
    [SerializeField] private Image skinHUD;
    [SerializeField] private Image skinFrameHUD;
    [SerializeField] private Image skinsTitleHUD;
    [SerializeField] private Image skinUITextHUD;
    [SerializeField] private Image mapHUD;
    [SerializeField] private Image mapFrameHUD;
    [SerializeField] private Image mapsTitleHUD;
    [SerializeField] private Image mapUITextHUD;
    [SerializeField] private Image aboutBackground;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private Sprite[] maleSkins;
    [SerializeField] private Sprite[] femaleSkins;
    [SerializeField] private Sprite[] maps;
    [SerializeField] private Sprite[] abouts;
    [SerializeField] private TextMeshProUGUI skinUIText;
    [SerializeField] private TextMeshProUGUI mapUIText;

    private enum StartMenuStates { idle, start, help, about, quit, select };
    private StartMenuStates startMenuState = StartMenuStates.idle;

    private string[] maleSkinNames;
    private string[] femaleSkinNames;
    private string[] mapNames;

    private bool isMale;
    private int lastSkinUsed;
    private int lastMapUsed;
    private int unlockedSkins;
    private int unlockedMaps;

    void Start()
    {

        maleSkinNames = new string[] 
        { 

            "Shanon", 
            "Crypto",
            "N/A",
            "N/A",
            "N/A"

        };

        femaleSkinNames = new string[] 
        { 

            "Kate",
            "Arissa",
            "N/A",
            "N/A",
            "N/A"

        };

        mapNames = new string[] 
        { 

            "Mapita",
            "Luneta Park",
            "N/A",
            "N/A",
            "N/A"

        };

        PlayerModel playerManager = Database.LoadPlayer();

        if (playerManager == null)
        {

            FindObjectOfType<PlayerManager>().NewPlayer();

        }

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

            OnBack();

        }

        if (SimpleInput.GetButtonDown("OnStart"))
        {

            OnAnimateFromStartMenu(StartMenuStates.start);
            OnCancel();

        }

        if (SimpleInput.GetButtonDown("OnHelp"))
        {

            OnAnimateFromStartMenu(StartMenuStates.help);
            OnCancel();

        }

        if (SimpleInput.GetButtonDown("OnAbout"))
        {

            OnAnimateFromStartMenu(StartMenuStates.about);
            OnCancel();
            OnAbout();

        }

        if (SimpleInput.GetButtonDown("OnQuit"))
        {

            OnAnimateFromStartMenu(StartMenuStates.quit);
            OnCancel();

        }

        if (startMenuState == StartMenuStates.start)
        {

            if (SimpleInput.GetButtonDown("OnMale"))
            {

                isMale = true;
                FindObjectOfType<GameManager>().OnAnimate("male");
                int countdown = 1;
                StartCoroutine(SelectSectionToStart(countdown));

            }

            if (SimpleInput.GetButtonDown("OnFemale"))
            {

                isMale = false;
                FindObjectOfType<GameManager>().OnAnimate("female");
                int countdown = 1;
                StartCoroutine(SelectSectionToStart(countdown));

            }

        }

        if (startMenuState == StartMenuStates.select)
        {

            FindObjectOfType<PlayerManager>().unlockedSkins = unlockedSkins;
            FindObjectOfType<PlayerManager>().unlockedMaps = unlockedMaps;

            skinHUD.sprite = isMale ? maleSkins[lastSkinUsed] : femaleSkins[lastSkinUsed];
            skinUIText.text = isMale ? maleSkinNames[lastSkinUsed] : femaleSkinNames[lastSkinUsed];
            mapHUD.sprite = maps[lastMapUsed];
            mapUIText.text = mapNames[lastMapUsed];

            if (lastSkinUsed <= unlockedSkins)
            {

                skinsTitleHUD.sprite = resources[1];
                skinUITextHUD.sprite = resources[5];
                skinFrameHUD.sprite = resources[7];
                skinUIText.color = Color.white;
                skinLockedHUD.SetActive(false);

            }
            else
            {

                skinsTitleHUD.sprite = resources[0];
                skinUITextHUD.sprite = resources[4];
                skinFrameHUD.sprite = resources[6];
                skinUIText.color = Color.black;
                skinLockedHUD.SetActive(true);

            }

            if (lastMapUsed <= unlockedMaps)
            {

                mapsTitleHUD.sprite = resources[3];
                mapUITextHUD.sprite = resources[5];
                mapFrameHUD.sprite = resources[7];
                mapUIText.color = Color.white;
                mapLockedHUD.SetActive(false);

            }
            else
            {

                mapsTitleHUD.sprite = resources[2];
                mapUITextHUD.sprite = resources[4];
                mapFrameHUD.sprite = resources[6];
                mapUIText.color = Color.black;
                mapLockedHUD.SetActive(true);

            }

            if (lastSkinUsed == 0)
            {

                skinPreviousUIButton.interactable = false;

            }
            else
            {

                skinPreviousUIButton.interactable = true;

            }

            if (lastSkinUsed < 4)
            {

                skinNextUIButton.interactable = true;

            }
            else
            {

                skinNextUIButton.interactable = false;

            }

            if (lastMapUsed == 0)
            {

                mapPreviousUIButton.interactable = false;

            }
            else
            {

                mapPreviousUIButton.interactable = true;

            }

            if (lastMapUsed < 4)
            {

                mapNextUIButton.interactable = true;

            }
            else
            {

                mapNextUIButton.interactable = false;

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

                if (lastSkinUsed < 4)
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

                if (lastMapUsed < 4)
                {

                    lastMapUsed++;

                }

            }

            if (SimpleInput.GetButtonDown("OnRun"))
            {



            }

        }

        if (startMenuState == StartMenuStates.quit)
        {

            if (SimpleInput.GetButtonDown("OnAffirmativeQuit"))
            {

                int countdown = 3;
                StartCoroutine(QuitToStart(countdown));

            }

            if (SimpleInput.GetButtonDown("OnNegativeQuit"))
            {

                OnBack();

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

    private void OnCancel()
    {

        if (lastSkinUsed > unlockedSkins)
        {

            lastSkinUsed = FindObjectOfType<PlayerManager>().lastSkinUsed;

        }

        if (lastMapUsed > unlockedMaps)
        {

            lastMapUsed = FindObjectOfType<PlayerManager>().lastMapUsed;

        }

    }

    private void OnAbout()
    {

        int randomState = UnityEngine.Random.Range(0, 2);

        aboutBackground.sprite = abouts[randomState];

    }

    private void OnBack()
    {

        OnAnimateFromStartMenu(StartMenuStates.idle);
        OnCancel();

    }

    IEnumerator QuitToStart(int _countdown)
    {

        OnBack();

        while (_countdown > 0)
        {

            if (_countdown == 3)
            {

                FindObjectOfType<GameManager>().OnAnimate("offStartMenu");

            }

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        PlayerPrefs.SetInt("index", 1);
        Application.Quit();

    }

}
