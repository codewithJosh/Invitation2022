using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadManager : MonoBehaviour
{

    [SerializeField] private Image loadingFillHUD;
    [SerializeField] private TextMeshProUGUI loadingProgressUIText;

    private void Start()
    {

        loadingProgressUIText.text = "0%";
        int countdown = 2;
        StartCoroutine(LoadingToStart(countdown));  

    }

    IEnumerator LoadingToStart(int _countdown)
    {

        while (_countdown > 0)
        {

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        int index = PlayerPrefs.GetInt("index", 2);
        StartCoroutine(LoadAsynchronously(index));

    }

    IEnumerator LoadAsynchronously(int _index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(_index);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadingFillHUD.fillAmount = progress;
            loadingProgressUIText.text = progress * 100f + "%";

            yield return null;

        }

    }

}
