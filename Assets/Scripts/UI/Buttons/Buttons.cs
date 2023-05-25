using UnityEngine;
using UnityEngine.EventSystems;
public class Buttons : MonoBehaviour
{
    private UIController _uiController;
    private GOReferencesContainer _referencesContainer;
    private void Start()
    {
        _referencesContainer = GameObject.Find("GORefContainer").GetComponent<GOReferencesContainer>();  
        _uiController = _referencesContainer.GetGUIRef().GetComponent<UIController>();
    }

    public void BtnClick()
    {
        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;
        _uiController.ClickHandler(buttonRef);
    }
}
