using UnityEngine;
using UnityEngine.EventSystems;

public class JumpPressed : MonoBehaviour, IPointerDownHandler 
{
    public event VoidHandler JumpTrigger;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        JumpTrigger();
    }
}
