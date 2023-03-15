using UnityEngine.UI;
using UnityEngine;

public class HUDnew : MonoBehaviour
{
    private Image Image;
    private GameObject playerRef, hpBar, defenseBar;
    //private float ActionTime = 0.0f;

    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
        hpBar = GameObject.Find("HPBar");
        defenseBar = GameObject.Find("DefenseBar");
        //HideAllIcons();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void 
}
