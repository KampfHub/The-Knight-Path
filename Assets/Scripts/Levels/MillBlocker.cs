using UnityEngine;

public class MillBlocker : MonoBehaviour
{
    private bool millState = true;
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rock")
        {
            gameObject.GetComponent<Mill>().enabled = false;
            millState = false;
        }
    }

    public bool GetMillState()
    {
        return millState;
    }
}

