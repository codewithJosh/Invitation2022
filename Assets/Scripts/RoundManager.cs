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
    private int initialCountdown;
    private int roundCountdown;
    private int lastRoundStep;
    private int roundStep;
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

        return Random.Range(0, 3) switch
        {

            0 => 25f,

            1 => 15f,

            _ => 5f,

        };

    }

    void Start()
    {

        initialCountdown = 8;

        FindObjectOfType<PlayerManager>().LoadPlayer();

        lastMapUsed = FindObjectOfType<PlayerManager>().lastMapUsed;
        lastDivisionUsed = FindObjectOfType<PlayerManager>().lastDivisionUsed;
        roundCountdown = FindObjectOfType<PlayerManager>().MAP_INT[lastMapUsed, lastDivisionUsed, 1];

        int goldDivison = FindObjectOfType<PlayerManager>().MAP_INT[lastMapUsed, 2, 0];
        int silverDivision = FindObjectOfType<PlayerManager>().MAP_INT[lastMapUsed, 1, 0];
        int bronzeDivision = FindObjectOfType<PlayerManager>().MAP_INT[lastMapUsed, 0, 0];

        goldDivisionUIText.text = GetTime(goldDivison);
        silverDivisionUIText.text = GetTime(silverDivision);
        bronzeDivisionUIText.text = GetTime(bronzeDivision);

        roundStep = FindObjectOfType<PlayerManager>().lastRoundStepUsed;

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

        yield return new WaitForSeconds(1f);


    }

    void Update()
    {



    }

    void FixedUpdate()
    {

        if (transform.position.y < -20f)
        {

            transform.position = respawnPoint;

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

}
