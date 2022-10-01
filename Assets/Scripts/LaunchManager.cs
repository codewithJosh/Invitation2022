using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchManager : MonoBehaviour
{

    private float timeDelayed;
    private float timeElapsed;

    void Start()
    {

        timeDelayed = 50f;

    }

    void Update()
    {

        timeElapsed += Time.deltaTime;

        if (timeElapsed > timeDelayed)
        {

            OnTapToSkip();

        }

    }

    public void OnTapToSkip()
    {

        SceneManager.LoadScene(1);

    }

}
