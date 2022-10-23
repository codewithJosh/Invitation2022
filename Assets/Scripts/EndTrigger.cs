using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        string tag = gameObject.tag;

        if (FindObjectOfType<RoundManager>().lastTag != tag)
        {

            Vector3 position = gameObject.transform.position;

            FindObjectOfType<RoundManager>().respawnPoint = new Vector3(position.x + getXRandomPosition(), position.y - 5f, position.z);
            FindObjectOfType<RoundManager>().lastTag = tag;
            FindObjectOfType<RoundManager>().OnStepState();

        }

        if (tag == "Finish")
        {



        }


    }

    private float getXRandomPosition()
    {

        return Random.Range(0, 3) switch
        {

            0 => -10f,

            1 => 10f,

            _ => 0,

        };

    }

}
