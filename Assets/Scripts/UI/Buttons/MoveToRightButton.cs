using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToRightButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool rightDirectionIsHold;
    public bool MoveToRightButtonIsHold()
    {
        return rightDirectionIsHold;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        rightDirectionIsHold = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        rightDirectionIsHold = false;    
    }
}
