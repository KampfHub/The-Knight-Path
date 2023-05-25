using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private UIController _uiController;
    private GOReferencesContainer _referencesContainer;
    private List<string> _uiComponentsNames= new List<string>();
    private void Start()
    {
        _referencesContainer = GameObject.Find("GORefContainer").GetComponent<GOReferencesContainer>();
        _uiController = _referencesContainer.GetGUIRef().GetComponent<UIController>();
        FillUIComponentsNames();
    }
    public void ClickHandler(GameObject buttonRef)
    {
        if(_uiComponentsNames.Contains(buttonRef.name))
        {
            switch(buttonRef.name)
            {
                case "btnSelectLevel": break;
                case "btnSettings": break;
                case "btnMainMenu": break;
                case "btnQuit": QuitGame(); break;
                case "btnResume": break;
                case "btnNext": break;
                case "btnBack": break;
                case "btnReplay": break;
                case "btnNextLevel": break;
                case "btnGameShop": break;
                case "btnInfo": break;
                case "btnPotion_1": break;
                case "btnPotion_2": break;
                case "btnPotion_3": break;
                case "btnPotion_4": break;
                case "btnPotion_5": break;
                case "btnPotion_6": break;
                case "btnCloseSLMenu": break;
                case "btnCloseSettingsMenu": break;
                case "btnCloseInfoMenu": break;
                case "btnCloseShop": break;
                
            }
        }
    }
    private void FillUIComponentsNames()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        {
            _uiComponentsNames.Add(child.gameObject.name);
        }
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
