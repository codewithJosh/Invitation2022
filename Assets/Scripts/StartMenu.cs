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

    private enum StartMenuStates { idle, start, help, about, quit, select};
    private StartMenuStates startMenuState = StartMenuStates.idle;

    private int[,,] MAP_INT;
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

            "SHANON", 
            "CRYPTO",
            "N/A",
            "N/A",
            "N/A"

        };

        femaleSkinNames = new string[] 
        { 

            "KATE",
            "ARISSA",
            "N/A",
            "N/A",
            "N/A"

        };

        mapNames = new string[] 
        { 

            "MAPITA",
            "LUNETA PARK",
            "N/A",
            "N/A",
            "N/A"

        };

        MAP_INT = new int[5, 3, 2]
        {

            // MAPITA
            { 

                { 0, 60 }, 
                { 0, 55 }, 
                { 0, 50 } 

            },

            // LUNETA PARK
            { 

                { 0, 60 }, 
                { 0, 55 }, 
                { 0, 50 } 

            },

            // N/A
            { 

                { 0, 60 },
                { 0, 55 },
                { 0, 50 } 

            },

            // N/A
            { 

                { 0, 60 }, 
                { 0, 55 }, 
                { 0, 50 } 

            },

            // N/A
            { 

                { 0, 60 }, 
                { 0, 55 }, 
                { 0, 50 } 

            }

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

                OnCharacterPicking(true);

            }

            if (SimpleInput.GetButtonDown("OnFemale"))
            {

                OnCharacterPicking(false);

            }

        }

        if (startMenuState == StartMenuStates.select)
        {

            FindObjectOfType<PlayerManager>().MAP_INT = MAP_INT;
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

    private void OnCharacterPicking(bool _isMale)
    {

        isMale = _isMale;
        FindObjectOfType<GameManager>().OnAnimate(_isMale ? "male" : "female");
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

        FindObjectOfType<PlayerManager>().lastSkinUsed = lastSkinUsed;
        FindObjectOfType<PlayerManager>().lastMapUsed = lastMapUsed;
        FindObjectOfType<PlayerManager>().SavePlayer();

        PlayerPrefs.SetInt("index", lastMapUsed + 3);
        SceneManager.LoadScene(1);


    }

}
