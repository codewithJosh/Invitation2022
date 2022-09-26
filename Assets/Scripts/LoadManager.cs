using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{

    public Image fill;
    public Text text;

    private void Start()
    {

        int index = PlayerPrefs.GetInt("index", 1);
        LoadScene(index);

    }

    public void LoadScene(int _index)
    {

        StartCoroutine(LoadAsynchronously(_index));

    }

    IEnumerator LoadAsynchronously(int _index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(_index);

        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);

            fill.fillAmount = progress;
            text.text = progress * 100f + "%";

            yield return null;

        }

    }

}
