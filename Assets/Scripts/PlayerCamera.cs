using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform player;
    float xRotation = 20f;

    void Start()
    {

        //Cursor.lockState = CursorLockMode.Locked;

    }

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
