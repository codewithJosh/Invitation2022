using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Vector3 startPosition;

    private void Awake()
    {

        startPosition = transform.position;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
