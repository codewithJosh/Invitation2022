using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        string tag = gameObject.tag;

        if (FindObjectOfType<Mapita>().tag != tag)
        {

            Vector3 position = gameObject.transform.position;

            FindObjectOfType<Mapita>().respawnPoint = new Vector3(position.x + getXRandomPosition(), position.y + 3f, position.z);
            FindObjectOfType<Mapita>().tag = tag;
            FindObjectOfType<Mapita>().OnStepState();

        }
        else if (FindObjectOfType<Mapita>().tag == "Finish")
        {



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
