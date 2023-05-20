using UnityEngine;
using UnityEngine.EventSystems;

public class JumpPressed : MonoBehaviour, IPointerDownHandler 
{
    public event VoidConteiner JumpTrigger;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        JumpTrigger();
    }
}
