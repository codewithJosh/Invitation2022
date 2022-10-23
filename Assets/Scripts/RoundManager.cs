using System.Collections;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI initialCountdownUIText;
    [SerializeField] private TextMeshProUGUI roundCountdownUIText;
    [SerializeField] private TextMeshProUGUI roundStepUIText;
    [SerializeField] private TextMeshProUGUI goldDivisionUIText;
    [SerializeField] private TextMeshProUGUI silverDivisionUIText;
    [SerializeField] private TextMeshProUGUI bronzeDivisionUIText;

    [HideInInspector] public Vector3 respawnPoint;
    private bool isDied;
    private int initialCountdown;
    private int roundCountdown;
    private int lastRoundStep;
    private int roundStep;
    private int isMale;
    private int lastMapUsed;
    private int lastDivisionUsed;
    private string tag;
    

    private void Awake()
    {

        respawnPoint = new Vector3(transform.position.x, GetRandomYPosition(), transform.position.z);
        transform.position = respawnPoint;
        FindObjectOfType<PlayerMovement>().playerState = PlayerMovement.PlayerStates.idle;

    }

    private float GetRandomYPosition()
    {

        return Random.Range(0, 1) switch
        {

            0 => 21.86727f,

            1 => 11.83727f,

            _ => 1.782271f,

        };

    }

    void Start()
    {

        initialCountdown = 3;

        FindObjectOfType<PlayerManager>().LoadPlayer();

        isMale = FindObjectOfType<PlayerManager>().isMale;
        lastMapUsed = FindObjectOfType<PlayerManager>().lastMapUsed;
        lastDivisionUsed = FindObjectOfType<PlayerManager>().lastDivisionUsed;
        roundCountdown = FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, lastDivisionUsed, 1];
        roundStep = FindObjectOfType<PlayerManager>().lastRoundStepUsed;

        int goldDivison = FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 2, 0];
        int silverDivision = FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 1, 0];
        int bronzeDivision = FindObjectOfType<PlayerManager>().MAP_INT[isMale, lastMapUsed, 0, 0];

        goldDivisionUIText.text = GetTime(goldDivison);
        silverDivisionUIText.text = GetTime(silverDivision);
        bronzeDivisionUIText.text = GetTime(bronzeDivision);

        

        StartCoroutine(CountdownToStart());
        OnStepState();

    }

    IEnumerator CountdownToStart()
    {

        initialCountdownUIText.color = Color.green;

        while (initialCountdown > 0)
        {

            initialCountdownUIText.text = "READY---" + initialCountdown.ToString();

            yield return new WaitForSeconds(1f);

            initialCountdown--;

        }

        initialCountdownUIText.color = Color.red;
        initialCountdownUIText.text = "GO!";
        FindObjectOfType<PlayerMovement>().canMove = true;
        StartCoroutine(TimeLeftToStart());

        yield return new WaitForSeconds(1f);

        initialCountdownUIText.gameObject.SetActive(false);

    }

    IEnumerator TimeLeftToStart()
    {

        while (roundCountdown >= 0)
        {

            float minutes = Mathf.FloorToInt(roundCountdown / 60);
            float seconds = Mathf.FloorToInt(roundCountdown % 60);

            if (minutes == 0 && seconds < 16)
            {

                roundCountdownUIText.color = Color.red;

            }

            roundCountdownUIText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);

            roundCountdown--;

        }

        FindObjectOfType<PlayerMovement>().canMove = false;
        FindObjectOfType<PlayerMovement>().isDying = true;

        yield return new WaitForSeconds(1f);


    }

    void Update()
    {



    }

    void FixedUpdate()
    {

        if (transform.position.y < -20f)
        {

            if (!isDied)
            {

                isDied = true;
                FindObjectOfType<PlayerMovement>().isDied = true;
                
            }

            int countdown = 1;
            StartCoroutine(OnRespawnToStart(countdown));

        }

    }

    public void OnStepState()
    {

        lastRoundStep += 1;
        roundStepUIText.text = lastRoundStep + "/" + roundStep;

    }

    private string GetTime(int _divisionTimeInSeconds)
    {

        float minutes = Mathf.FloorToInt(_divisionTimeInSeconds / 60);
        float seconds = Mathf.FloorToInt(_divisionTimeInSeconds % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    IEnumerator OnRespawnToStart(int _countdown)
    {

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        isDied = false;
        FindObjectOfType<PlayerMovement>().isDied = false;
        transform.position = respawnPoint;

    }

}
