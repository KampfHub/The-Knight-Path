using UnityEngine;

public class KnightGirl : MonoBehaviour
{
    GameObject GUI;
    void Start()
    { 
        GUI = GameObject.Find("GUI");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && GUI is not null)
        {
            GUI.GetComponent<GeneralUI>().EndGameMenu();
        }
    }
}
