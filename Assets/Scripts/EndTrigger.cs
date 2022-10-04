using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        string tag = gameObject.tag;

        if (FindObjectOfType<PlayerManager>().tag != tag)
        {

            Vector3 position = gameObject.transform.position;

            FindObjectOfType<PlayerManager>().respawnPoint = new Vector3(position.x + getXRandomPosition(), position.y + 3f, position.z);
            FindObjectOfType<PlayerManager>().tag = tag;
            FindObjectOfType<PlayerManager>().OnStepState();

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
