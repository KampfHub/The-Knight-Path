using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public void KeyboardController(Action action, KeyCode keyCode, string pressType)
    {
        switch(pressType)
        {
            case "click": if (Input.GetKeyDown(keyCode)) action(); break;
            case "down": if (Input.GetKey(keyCode)) action(); break;
            case "up": if (Input.GetKeyUp(keyCode)) action(); break;
        }
    }
    public void UIController(Action action, bool isHold)
    {
        if(isHold) action();
    }
}
