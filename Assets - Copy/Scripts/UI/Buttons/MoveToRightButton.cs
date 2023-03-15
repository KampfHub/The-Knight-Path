using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToRightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private GameObject playerRef;
    private bool isHold;

    private void Awake()
    {
        playerRef = GameObject.FindWithTag("Player");
    }
    public bool ButtonIsHold()
    {
        return isHold;
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isHold = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isHold = false;
        playerRef.GetComponent<Player>().StopMove();
    }
}
