using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private GameObject skinLockedHUD;
    [SerializeField] private GameObject mapLockedHUD;
    [SerializeField] private Image skinHUD;
    [SerializeField] private Image skinFrameHUD;
    [SerializeField] private Image skinsTitleHUD;
    [SerializeField] private Image skinUITextHUD;
    [SerializeField] private Image skinPreviousUIButton;
    [SerializeField] private Image skinNextUIButton;
    [SerializeField] private Image mapHUD;
    [SerializeField] private Image mapFrameHUD;
    [SerializeField] private Image mapsTitleHUD;
    [SerializeField] private Image mapUITextHUD;
    [SerializeField] private Image mapPreviousUIButton;
    [SerializeField] private Image mapNextUIButton;
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

            OnAnimateFromStartMenu(StartMenuStates.idle);
            OnCancel();

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

            FindObjectOfType<PlayerManager>().lastMapUsed = lastMapUsed;
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
                skinFrameHUD.sprite = resources[11];
                skinUIText.color = Color.white;
                skinLockedHUD.SetActive(false);

            }
            else
            {

                skinsTitleHUD.sprite = resources[0];
                skinUITextHUD.sprite = resources[4];
                skinFrameHUD.sprite = resources[10];
                skinUIText.color = Color.black;
                skinLockedHUD.SetActive(true);

            }

            if (lastMapUsed <= unlockedMaps)
            {

                mapsTitleHUD.sprite = resources[3];
                mapUITextHUD.sprite = resources[5];
                mapFrameHUD.sprite = resources[11];
                mapUIText.color = Color.white;
                mapLockedHUD.SetActive(false);

            }
            else
            {

                mapsTitleHUD.sprite = resources[2];
                mapUITextHUD.sprite = resources[4];
                mapFrameHUD.sprite = resources[10];
                mapUIText.color = Color.black;
                mapLockedHUD.SetActive(true);

            }

            if (lastSkinUsed == 0)
            {

                skinPreviousUIButton.sprite = resources[6];

            }
            else
            {

                skinPreviousUIButton.sprite = resources[7];

            }
            
            if (lastSkinUsed < 4)
            {

                skinNextUIButton.sprite = resources[9];

            }
            else
            {

                skinNextUIButton.sprite = resources[8];

            }

            if (lastMapUsed == 0)
            {

                mapPreviousUIButton.sprite = resources[6];

            }
            else
            {

                mapPreviousUIButton.sprite = resources[7];

            }

            if (lastMapUsed < 4)
            {

                mapNextUIButton.sprite = resources[9];

            }
            else
            {

                mapNextUIButton.sprite = resources[8];

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

    }

    private void OnAbout()
    {

        int randomState = UnityEngine.Random.Range(0, 2);

        aboutBackground.sprite = abouts[randomState];

    }

}
