using System.Collections;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{

    public Vector3 respawnPoint;
    public int countdownTime;
    public int timeLeftTime;
    public int stepState;
    public int finalStep;
    public string tag;
    public TextMeshProUGUI countDownUIText;
    public TextMeshProUGUI timeLeftUIText;
    public TextMeshProUGUI stepUIText;

    private void Awake()
    {

        respawnPoint = new Vector3(transform.position.x, getRandomYPosition(), transform.position.z);
        transform.position = respawnPoint;
        FindObjectOfType<PlayerMovement>().playerState = PlayerMovement.PlayerStates.idle;

    }

    private float getRandomYPosition()
    {

        switch (Random.Range(0, 3))
        {

            case 0:
                return 25f;
            case 1:
                return 15f;

        }
        return 5f;

    }

    void Start()
    {

        StartCoroutine(CountdownToStart());
        OnStepState();

    }

    IEnumerator CountdownToStart()
    {

        countDownUIText.color = Color.green;

        while (countdownTime > 0)
        {

            countDownUIText.text = "READY---" + countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;

        }

        countDownUIText.color = Color.red;
        countDownUIText.text = "GO!";
        FindObjectOfType<PlayerMovement>().canMove = true;
        StartCoroutine(TimeLeftToStart());

        yield return new WaitForSeconds(1f);

        countDownUIText.gameObject.SetActive(false);

    }

    IEnumerator TimeLeftToStart()
    {

        while (timeLeftTime >= 0)
        {

            float minutes = Mathf.FloorToInt(timeLeftTime / 60);
            float seconds = Mathf.FloorToInt(timeLeftTime % 60);

            if (minutes == 0 && seconds < 16)
            {

                timeLeftUIText.color = Color.red;

            }

            timeLeftUIText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);

            timeLeftTime--;

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

        stepState += 1;
        stepUIText.text = stepState + "/" + finalStep;

    }

}
