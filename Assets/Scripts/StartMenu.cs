using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button skinPreviousUIButton;
    [SerializeField] private Button skinNextUIButton;
    [SerializeField] private Button mapPreviousUIButton;
    [SerializeField] private Button mapNextUIButton;
    [SerializeField] private GameObject skinLockedHUD;
    [SerializeField] private GameObject mapLockedHUD;
    [SerializeField] private GameObject bronzeDivisionCheckHUD;
    [SerializeField] private GameObject silverDivisionCheckHUD;
    [SerializeField] private GameObject goldDivisionCheckHUD;
    [SerializeField] private GameObject skinForeground;
    [SerializeField] private GameObject mapForeground;
    [SerializeField] private GameObject skinUnknownHUD;
    [SerializeField] private GameObject mapUnknownHUD;
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
    [SerializeField] private TextMeshProUGUI finishWithinUIText;
    [SerializeField] private TextMeshProUGUI secondsUIText;
    [SerializeField] private TextMeshProUGUI mapCountdownUIText;
    [SerializeField] private TextMeshProUGUI skinLockedUIText;
    [SerializeField] private TextMeshProUGUI mapLockedUIText;

    private enum StartMenuStates { idle, start, help, about, quit, select};
    private StartMenuStates startMenuState = StartMenuStates.idle;
    private enum DivisionStates { idle, bronze, silver, gold};
    private DivisionStates divisionState = DivisionStates.idle;

    private int[,,,] MAP_INT;
    private string[,,] SkinNames;
    private string[,] mapNames;
    private int[] MAP_ROUND_STEP_INT;

    private int isMale;
    private int lastSkinUsed;
    private int lastMapUsed;
    private int lastDivisionUsed;
    private int lastRoundStepUsed;
    private int unlockedSkins;
    private int unlockedMaps;

    void Start()
    {

        SkinNames = new string[5, 2, 2]
        {

           {

                { "SHANNON", "" },
                { "KATE", "" } 

           },

           { 

                { "CRYPTO", "UNLOCK NEXT MAP" },
                { "ARISSA", "UNLOCK NEXT MAP" } 

           },

           { 

                { "N/A", "UNLOCK NEXT MAP" },
                { "N/A", "UNLOCK NEXT MAP" } 

           },

           { 

                { "N/A", "UNLOCK NEXT MAP" },
                { "N/A", "UNLOCK NEXT MAP" } 

           },

           { 

                { "N/A", "UNLOCK NEXT MAP" },
                { "N/A", "UNLOCK NEXT MAP" } 

           }

        };

        mapNames = new string[5, 2] 
        {

            { "MAPITA", "" },
            { "LUNETA PARK", "UNLOCK 1 GOLD MAP" },
            { "MARAWI", "UNLOCK 2 GOLD MAP" },
            { "CORON", "UNLOCK 3 GOLD MAP" },
            { "SAN JUANICO", "UNLOCK 4 GOLD MAP" }

        };

        MAP_INT = new int[2, 5, 3, 2]
        {

            // FEMALE
            { 

                // MAPITA
                {

                    { 0, 200 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // LUNETA PARK
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // N/A
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // N/A
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // N/A
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                } 

            },

            // MALE
            {

                // MAPITA
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // LUNETA PARK
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // N/A
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // N/A
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                },

                // N/A
                {

                    { 0, 70 },
                    { 0, 65 },
                    { 0, 60 }

                }

            }

        };

        MAP_ROUND_STEP_INT = new int[]
        {

            3,
            4,
            4,
            4,
            4,

        };

        PlayerModel playerManager = Database.LoadPlayer();

        if (playerManager == null)
        {

            FindObjectOfType<PlayerManager>().NewPlayer(MAP_INT);

        }

        FindObjectOfType<PlayerManager>().LoadPlayer();

        MAP_INT = FindObjectOfType<PlayerManager>().MAP_INT;
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

            OnNavigation(StartMenuStates.start);

        }

        if (SimpleInput.GetButtonDown("OnHelp"))
        {

            OnNavigation(StartMenuStates.help);

        }

        if (SimpleInput.GetButtonDown("OnAbout"))
        {

            OnNavigation(StartMenuStates.about);
            OnAbout();

        }

        if (SimpleInput.GetButtonDown("OnQuit"))
        {

            OnNavigation(StartMenuStates.quit);

        }

        if (startMenuState == StartMenuStates.start)
        {

            if (SimpleInput.GetButtonDown("OnMale"))
            {

                OnCharacterPicking(1);

            }

            if (SimpleInput.GetButtonDown("OnFemale"))
            {

                OnCharacterPicking(0);

            }

        }

        if (startMenuState == StartMenuStates.select)
        {

            FindObjectOfType<PlayerManager>().MAP_INT = MAP_INT;
            FindObjectOfType<PlayerManager>().unlockedSkins = unlockedSkins;
            FindObjectOfType<PlayerManager>().unlockedMaps = unlockedMaps;

            skinHUD.sprite = isMale == 1 ? maleSkins[lastSkinUsed] : femaleSkins[lastSkinUsed];
            mapHUD.sprite = maps[lastMapUsed];
            skinUIText.text = isMale == 1 ? SkinNames[lastSkinUsed, 0, 0] : SkinNames[lastSkinUsed, 1, 0];
            mapUIText.text = mapNames[lastMapUsed, 0];

            if (lastSkinUsed <= unlockedSkins)
            {

                skinsTitleHUD.sprite = resources[1];
                skinUITextHUD.sprite = resources[5];
                skinFrameHUD.sprite = resources[7];
                skinLockedUIText.text = "";
                skinUIText.color = Color.white;
                skinLockedHUD.SetActive(false);
                skinForeground.SetActive(false);
                skinUnknownHUD.SetActive(false);

            }
            else
            {

                skinsTitleHUD.sprite = resources[0];
                skinUITextHUD.sprite = resources[4];
                skinFrameHUD.sprite = resources[6];
                skinLockedUIText.text = isMale == 1 ? SkinNames[lastSkinUsed, 0, 1] : SkinNames[lastSkinUsed, 1, 1];
                skinUIText.color = Color.black;
                skinLockedHUD.SetActive(true);
                skinForeground.SetActive(true);
                skinUnknownHUD.SetActive(true);

            }

            if (lastMapUsed <= unlockedMaps)
            {

                bronzeDivisionCheckHUD.SetActive(FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 0, 0] != 0 ? true : false);
                silverDivisionCheckHUD.SetActive(FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 1, 0] != 0 ? true : false);
                goldDivisionCheckHUD.SetActive(FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 2, 0] != 0 ? true : false);
                mapsTitleHUD.sprite = resources[3];
                mapUITextHUD.sprite = resources[5];
                mapLockedUIText.text = "";
                mapUIText.color = Color.white;
                mapLockedHUD.SetActive(false);
                mapForeground.SetActive(false);
                mapUnknownHUD.SetActive(false);
                finishWithinUIText.enabled = true;
                mapCountdownUIText.enabled = true;
                mapCountdownUIText.text = FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, lastDivisionUsed, 1].ToString();
                secondsUIText.enabled = true;

                if (FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 2, 0] != 0)
                {

                    mapFrameHUD.sprite = resources[10];

                }
                else if (FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 1, 0] != 0)
                {

                    mapFrameHUD.sprite = resources[9];

                }
                else if (FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 0, 0] != 0)
                {

                    mapFrameHUD.sprite = resources[8];

                }
                else
                {

                    mapFrameHUD.sprite = resources[7];

                }

                if (FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 0, 0] == 0)
                {

                    lastDivisionUsed = 0;
                    OnAnimateFromSelectSection(DivisionStates.bronze);

                }
                else if (FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 1, 0] == 0)
                {

                    lastDivisionUsed = 1;
                    OnAnimateFromSelectSection(DivisionStates.silver);

                }
                else if (FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 2, 0] == 0)
                {

                    lastDivisionUsed = 2;
                    OnAnimateFromSelectSection(DivisionStates.gold);

                }
                else
                {

                    lastDivisionUsed = 2;
                    OnAnimateFromSelectSection(DivisionStates.idle);

                }

            }
            else
            {

                bronzeDivisionCheckHUD.SetActive(false);
                silverDivisionCheckHUD.SetActive(false);
                goldDivisionCheckHUD.SetActive(false);
                OnAnimateFromSelectSection(DivisionStates.idle);
                finishWithinUIText.enabled = false;
                mapCountdownUIText.enabled = false;
                secondsUIText.enabled = false;
                mapsTitleHUD.sprite = resources[2];
                mapUITextHUD.sprite = resources[4];
                mapFrameHUD.sprite = resources[6];
                mapLockedUIText.text = mapNames[lastMapUsed, 1];
                mapUIText.color = Color.black;
                mapLockedHUD.SetActive(true);
                mapForeground.SetActive(true);
                mapUnknownHUD.SetActive(true);

            }

            skinPreviousUIButton.interactable = lastSkinUsed == 0 ? false : true;
            skinNextUIButton.interactable = lastSkinUsed < 4 ? true : false;
            mapPreviousUIButton.interactable = lastMapUsed == 0 ? false : true;
            mapNextUIButton.interactable = lastMapUsed < 4 ? true : false;

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

                FindObjectOfType<GameManager>().OnAnimate("run");

            }

            if (SimpleInput.GetButtonDown("OnAffirmativeRun"))
            {

                OnAffirmativeRun();
                FindObjectOfType<GameManager>().OnAnimate("run");

            }

            if (SimpleInput.GetButtonDown("OnNegativeRun"))
            {

                FindObjectOfType<GameManager>().OnAnimate("run");

            }

        }

        if (startMenuState == StartMenuStates.quit)
        {

            if (SimpleInput.GetButtonDown("OnAffirmativeQuit"))
            {

                int countdown = 2;
                StartCoroutine(QuitToStart(countdown));

            }

        }

    }

    private void OnNavigation(StartMenuStates _startMenuState)
    {

        OnAnimateFromStartMenu(_startMenuState);
        OnCancel();

    }

    private void OnCharacterPicking(int _isMale)
    {

        isMale = _isMale;
        FindObjectOfType<GameManager>().OnAnimate(_isMale == 1 ? "male" : "female");
        int countdown = 1;
        StartCoroutine(SelectSectionToStart(countdown));

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

            if (_countdown == 2)
            {

                FindObjectOfType<GameManager>().OnAnimate("offStartMenu");

            }

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        PlayerPrefs.SetInt("index", 1);
        Application.Quit();

    }

    private void OnAffirmativeRun()
    {

        lastSkinUsed = lastSkinUsed > unlockedSkins ? FindObjectOfType<PlayerManager>().lastSkinUsed : lastSkinUsed;
        lastMapUsed = lastMapUsed > unlockedMaps ? FindObjectOfType<PlayerManager>().lastMapUsed : lastMapUsed;
        lastRoundStepUsed = MAP_ROUND_STEP_INT[lastMapUsed];

        FindObjectOfType<PlayerManager>().isMale = isMale;
        FindObjectOfType<PlayerManager>().lastSkinUsed = lastSkinUsed;
        FindObjectOfType<PlayerManager>().lastMapUsed = lastMapUsed;
        FindObjectOfType<PlayerManager>().lastDivisionUsed = lastDivisionUsed;
        FindObjectOfType<PlayerManager>().lastRoundStepUsed = lastRoundStepUsed;
        FindObjectOfType<PlayerManager>().SavePlayer();

        PlayerPrefs.SetInt("index", lastMapUsed + 3);
        SceneManager.LoadScene(1);

    }

    private void OnAnimateFromSelectSection(DivisionStates _divisionState)
    {

        divisionState = _divisionState;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("divisionState", (int) divisionState);

    }

}
