using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Premonition : MonoBehaviour
{

    [SerializeField] private Image creditsHUD;
    [SerializeField] private Sprite[] credits;

    private enum PremonitionStates { idle, premonition, credits };
    private PremonitionStates premonitionState = PremonitionStates.idle;

    private bool canTap;

    void Start()
    {

        canTap = false;
        int countdown = 50;
        StartCoroutine(PremonitionToStart(countdown));

    }

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnTapToSkip"))
        {

            OnTapToSkip();

        }

    }

    IEnumerator PremonitionToStart(int _countdown)
    {

        premonitionState = PremonitionStates.premonition;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("premonitionState", (int)premonitionState);

        int count = 0;

        while (_countdown > 0)
        {

            if (count == 4 && canTap == false)
            {

                canTap = true;

            }
            else if (count < 5)
            {

                count++;

            }

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        OnTapToSkip();

    }

    public void OnTapToSkip()
    {

        if (canTap == true)
        {

            int countdown = 5;
            StartCoroutine(CreditsToStart(countdown));

        }

    }

    IEnumerator CreditsToStart(int _countdown)
    {

        int randomState = Random.Range(0, 2);

        creditsHUD.sprite = credits[randomState];
        premonitionState = PremonitionStates.credits;
        FindObjectOfType<GameManager>().GetAnimator.SetInteger("premonitionState", (int)premonitionState);

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);

    }

}
