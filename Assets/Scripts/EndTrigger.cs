using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        string tag = gameObject.tag;

        if (FindObjectOfType<RoundManager>().tag != tag)
        {

            Vector3 position = gameObject.transform.position;

            FindObjectOfType<RoundManager>().respawnPoint = new Vector3(position.x + getXRandomPosition(), position.y, position.z);
            FindObjectOfType<RoundManager>().tag = tag;
            FindObjectOfType<RoundManager>().OnStepState();

        }
        else if (FindObjectOfType<RoundManager>().tag == "Finish")
        {

            Debug.Log("Done");

        }

    }

    private float getXRandomPosition()
    {

        switch (Random.Range(0, 3))
        {

            case 0:
                return -10f;
            case 1:
                return 10f;

        }
        return 0;

    }

}
