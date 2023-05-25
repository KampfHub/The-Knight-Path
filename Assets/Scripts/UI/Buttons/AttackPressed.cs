using UnityEngine;
using UnityEngine.EventSystems;

public class AttackPressed : MonoBehaviour, IPointerDownHandler
{
    public event VoidConteiner AttackTrigger;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        AttackTrigger();
    }
}
