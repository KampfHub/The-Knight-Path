using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private string dialogueBoxName;
    GameObject dialogueBoxRef;
    private void Start()
    {
        if (GameObject.Find(dialogueBoxName) is not null)
        {
            dialogueBoxRef = GameObject.Find(dialogueBoxName);
            dialogueBoxRef.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && dialogueBoxRef is not null)
        {
            dialogueBoxRef.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && dialogueBoxRef is not null)
        {
            dialogueBoxRef.SetActive(false);
        }
    }
}
