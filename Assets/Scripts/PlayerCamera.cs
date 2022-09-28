using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform player;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

        //Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        float lookX = SimpleInput.GetAxis("LookX") * mouseSensitivity * Time.deltaTime;
        float lookY = SimpleInput.GetAxis("LookY") * mouseSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * lookX);

    }

}
