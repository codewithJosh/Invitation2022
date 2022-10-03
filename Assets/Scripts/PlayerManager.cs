using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{

    public Vector3 startPosition;
    public int countdownTime;
    public TextMeshProUGUI countdown;

    private void Awake()
    {

        startPosition = transform.position;

    }

    void Start()
    {

        StartCoroutine(CountdownToStart());

    }

    IEnumerator CountdownToStart()
    {

        countdown.color = Color.green;
        FindObjectOfType<PlayerMovement>().canMove = false;

        while (countdownTime > 0)
        {

            countdown.text =  "READY---" + countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;

        }

        countdown.color = Color.red;
        countdown.text = "GO!";
        FindObjectOfType<PlayerMovement>().canMove = true;

        yield return new WaitForSeconds(1f);

        countdown.gameObject.SetActive(false);

    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {

        if (transform.position.y < -20f)
        {

            transform.position = startPosition;

        }

    }

}
