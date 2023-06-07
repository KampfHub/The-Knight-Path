using System;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    private GOReferencesContainer _referencesContainer;
    private Localization _localization;
    private LevelController _levelController;
    private SoundsController _soundsController;
    private Player _player;
    private Impacts _impacts;
    private int _levelMenuStateCounter;
    private void Start()
    {
        SetReferences();
        SetLocalizationOnGUI();
        UIPreparation();
        _impacts = gameObject.AddComponent<Impacts>();
        _levelMenuStateCounter = 0;
    }
    public void ClickHandler(GameObject buttonRef)
    {
        _soundsController.PlaySound("Click");
        switch (buttonRef.name)
        {
            case "btnSelectLevel": OpenLevelMenu(); break;
            case "btnSettings": OpenSettingsMenu(); break;
            case "btnMainMenu": OpenMainMenu(); break;
            case "btnQuit": QuitGame(); break;
            case "btnResume": CloseSettingsMenu(); break;
            case "btnNext": _levelMenuStateCounter ++; UpdateLevelMenu(); break;
            case "btnBack": _levelMenuStateCounter --; UpdateLevelMenu(); break;
            case "btnReplay": Replay(); break;
            case "btnNextLevel": LoadNextLevel(); break;
            case "btnGameShop": OpenGameShop(); break;
            case "btnInfo": OpenInfoMenu(); break;
            case "btnLevelHightSlot": LoadLevel(); break;
            case "btnLevelMiddleSlot": LoadLevel(); break;
            case "btnLevelLowSlot": LoadLevel(); break;
            case "btnPotion_1": UsePotion(buttonRef); break;
            case "btnPotion_2": UsePotion(buttonRef); break;
            case "btnPotion_3": UsePotion(buttonRef); break;
            case "btnPotion_4": UsePotion(buttonRef); break;
            case "btnPotion_5": UsePotion(buttonRef); break;
            case "btnPotion_6": UsePotion(buttonRef); break;
            case "btnCloseSLMenu": CloseSelectLevelMenu(); break;
            case "btnCloseSettingsMenu": CloseSettingsMenu(); break;
            case "btnCloseInfoMenu": CloseInfoMenu(); break;
            case "btnCloseShop": CloseGameShop(); break;
        }
    }
    private void SetReferences()
    {
        _referencesContainer = GameObject.Find("GORefContainer").GetComponent<GOReferencesContainer>();
        _levelController = _referencesContainer.GetLevelControllerRef().GetComponent<LevelController>();
        _soundsController = _referencesContainer.GetSoundControllerRef().GetComponent<SoundsController>();
        _player = _referencesContainer.GetPlayerRef().GetComponent<Player>();
        _localization = GetComponent<Localization>();
    }
    private void ShowMenu(GameObject menuPanel, bool state)
    {
        if (NullCheck(menuPanel)) menuPanel.SetActive(state);
    }
    private GameObject GetUIObjectRef(string uiObjectName)
    {
        foreach (GameObject gameObject in _referencesContainer.GetUIObjects())
        {
            if(gameObject.name == uiObjectName) return gameObject;
        }
        return null;
    }
    private GameObject GetSelectLevelBtnRef() => EventSystem.current.currentSelectedGameObject;
    private int GetLevelId(string btnTextLevel) => _localization.LevelNumberConstructor(btnTextLevel);
    private bool NullCheck(object value) => value is not null ? true : false;
    private void QuitGame()
    {
        if (NullCheck(_levelController)) _levelController.QuitGame();
    }
    private void UsePotion(GameObject buttonRef)
    {   
        int cost = Convert.ToInt32(buttonRef.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text);
        if (_player.GetCoinsValue() >= cost)
        {
            _player.SpendCoins(cost);
            _player.GetImpact(_impacts.GetDefaultPotionImpact(buttonRef.name));
            UpdateWalletValue();
        }
         
    }    
    private void UpdateWalletValue()
    {
        GameObject coins = GetUIObjectRef("textCoinsValue");
        if (NullCheck(coins) && NullCheck(_player))
        {
            coins.GetComponent<TextMeshProUGUI>().text = Convert.ToString(_player.GetCoinsValue());
        }
    }
    private void OpenGameShop()
    { 
        _levelController.Pause(true);
        _player.LockController(true);
        UpdateWalletValue();
        ShowMenu(GetUIObjectRef("GameShopMenu"), true);
        ShowMenu(GetUIObjectRef("ButtonsPanel"), false);
        FillGameShop();
    }
    private void OpenSettingsMenu()
    {
        if (_levelController.GetCurrentLevelID() != 0)
        {
            ShowMenu(GetUIObjectRef("PauseMenu"), true);
            ShowMenu(GetUIObjectRef("ButtonsPanel"), false);
            _levelController.Pause(true);
            _player.LockController(true);
        }
        else
        {
            ShowMenu(GetUIObjectRef("SettingsMenu"), true);
            ShowMenu(GetUIObjectRef("MainMenu"), false);
        }
    }
    private void OpenInfoMenu()
    {
        ShowMenu(GetUIObjectRef("InfoMenu"), true);
        ShowMenu(GetUIObjectRef("ButtonsPanel"), false);
        _levelController.AvailableLevelUpgrade(4);
    }
    private void OpenLevelMenu()
    {
        ShowMenu(GetUIObjectRef("SelectLevelMenu"), true);
        ShowMenu(GetUIObjectRef("ButtonsPanel"), false);
        ShowMenu(GetUIObjectRef("PauseMenu"), false);
        UpdateLevelMenu();
    }
    private void UpdateLevelMenu()
    {
        if (NullCheck(GetUIObjectRef("SelectLevelMenu")))
        {
            ShowLevelMenuButtons();
            switch (_levelMenuStateCounter)
            {
                default:
                    {
                        SetTextLevelMenu(1);
                        GetUIObjectRef("btnBack").SetActive(false);
                        GetUIObjectRef("btnNext").SetActive(_levelController.GetAvailableLevel() < 4 ? false : true);
                        break;
                    }
                case 1:
                    {
                        SetTextLevelMenu(4);
                        GetUIObjectRef("btnBack").SetActive(true);
                        GetUIObjectRef("btnNext").SetActive(_levelController.GetAvailableLevel() < 7 ? false : true);
                        break;
                    }
                case 2:
                    {
                        SetTextLevelMenu(7);
                        GetUIObjectRef("btnBack").SetActive(true);
                        GetUIObjectRef("btnNext").SetActive(false);
                        break;
                    }
            }
            HideUnavailableLevel();
        }
    }
    private void ShowLevelMenuButtons()
    {
        GetUIObjectRef("btnLevelHightSlot").SetActive(true);
        GetUIObjectRef("btnLevelMiddleSlot").SetActive(true);
        GetUIObjectRef("btnLevelLowSlot").SetActive(true);
    }
    private void HideUnavailableLevel()
    {
        if (_levelController.GetAvailableLevel() < GetLevelId(GetUIObjectRef("btnLevelHightSlot").GetComponentInChildren<TextMeshProUGUI>().text))
        {
            GetUIObjectRef("btnLevelHightSlot").SetActive(false);
        }
        if (_levelController.GetAvailableLevel() < GetLevelId(GetUIObjectRef("btnLevelMiddleSlot").GetComponentInChildren<TextMeshProUGUI>().text))
        {
            GetUIObjectRef("btnLevelMiddleSlot").SetActive(false);
        }
        if (_levelController.GetAvailableLevel() < GetLevelId(GetUIObjectRef("btnLevelLowSlot").GetComponentInChildren<TextMeshProUGUI>().text))
        {
            GetUIObjectRef("btnLevelLowSlot").SetActive(false);
        }
    }
    private void SetTextLevelMenu(int initialRange)
    {
        GetUIObjectRef("textLevelHightSlot").GetComponent<TextMeshProUGUI>().text = _localization.TextLevelButtons(initialRange);
        GetUIObjectRef("textLevelMiddleSlot").GetComponent<TextMeshProUGUI>().text = _localization.TextLevelButtons(initialRange + 1);
        GetUIObjectRef("textLevelLowSlot").GetComponent<TextMeshProUGUI>().text = _localization.TextLevelButtons(initialRange + 2);
    }
    private void FillGameShop()
    {
        FillPlaceInGameShop(1, _localization.GetPotionName("Power"), _localization.GetPotionDescription("Power"), 2);
        FillPlaceInGameShop(2, _localization.GetPotionName("Speed"), _localization.GetPotionDescription("Speed"), 2);
        FillPlaceInGameShop(3, _localization.GetPotionName("JumpForce"), _localization.GetPotionDescription("JumpForce"), 2);
        FillPlaceInGameShop(4, _localization.GetPotionName("HP"), _localization.GetPotionDescription("HP"), 1);
        FillPlaceInGameShop(5, _localization.GetPotionName("Defense"), _localization.GetPotionDescription("Defense"), 1);
        FillPlaceInGameShop(6, _localization.GetPotionName("Immortal"), _localization.GetPotionDescription("Immortal"), 4);
    }
    private void FillPlaceInGameShop(int slot, string potionName, string potionDescription, int price)
    {
        GameObject slotPotion = GameObject.Find($"btnPotion_{slot}");
        slotPotion.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = potionName;
        slotPotion.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = potionDescription;
        slotPotion.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(price);
    }
    private void OpenMainMenu()
    {
        StartCoroutine(_levelController.LoadLevel(0, GetUIObjectRef("LoadImage")));
    }
    private void LoadLevel()
    {
        StartCoroutine(_levelController.LoadLevel(
            GetLevelId(GetSelectLevelBtnRef().GetComponentInChildren<TextMeshProUGUI>().text), 
            GetUIObjectRef("LoadImage")));
    }
    private void Replay()
    {
        ShowMenu(GetUIObjectRef("LoseWindow"), false);
        StartCoroutine(_levelController.LoadLevel(_levelController.GetCurrentLevelID(), GetUIObjectRef("LoadImage")));
    }
    private void LoadNextLevel()
    {
        ShowMenu(GetUIObjectRef("WinWindow"), false);
        StartCoroutine(_levelController.LoadLevel(_levelController.GetAvailableLevel(), GetUIObjectRef("LoadImage")));
    }
    private void CloseInfoMenu()
    {
        ShowMenu(GetUIObjectRef("InfoMenu"), false);
        ShowMenu(GetUIObjectRef("ButtonsPanel"), true);
    }
    private void CloseGameShop()
    {
        _levelController.Pause(false);
        _player.LockController(false);
        ShowMenu(GetUIObjectRef("GameShopMenu"), false);
        ShowMenu(GetUIObjectRef("ButtonsPanel"), true);
    }
    private void CloseSelectLevelMenu()
    {
        ShowMenu(GetUIObjectRef("SelectLevelMenu"), false);
        ShowMenu(GetUIObjectRef("ButtonsPanel"), true);
        CloseSettingsMenu();
    }
    private void CloseSettingsMenu()
    {
        if (_levelController.GetCurrentLevelID() != 0)
        {
            ShowMenu(GetUIObjectRef("PauseMenu"), false);
            ShowMenu(GetUIObjectRef("ButtonsPanel"), true);
            _levelController.Pause(false);
            _player.LockController(false);
        }
        else
        {
            ShowMenu(GetUIObjectRef("SettingsMenu"), false);
            ShowMenu(GetUIObjectRef("MainMenu"), true);
        }
    }
    private void UIPreparation()
    {
        SaveData saveData = _referencesContainer.GetDataController().GetComponent<GameDataController>().LoadData();
        if (NullCheck(GetUIObjectRef("SelectLevelMenu"))) ShowMenu(GetUIObjectRef("SelectLevelMenu"), false);
        if (NullCheck(GetUIObjectRef("LoadMenu"))) ShowMenu(GetUIObjectRef("LoadMenu"), false);
        if (NullCheck(GetUIObjectRef("InfoMenu"))) ShowMenu(GetUIObjectRef("InfoMenu"), false);
        if (NullCheck(GetUIObjectRef("EndGameMenu"))) ShowMenu(GetUIObjectRef("EndGameMenu"), false);
        if (NullCheck(GetUIObjectRef("PauseMenu"))) ShowMenu(GetUIObjectRef("PauseMenu"), false);
        if (NullCheck(GetUIObjectRef("WinWindow"))) ShowMenu(GetUIObjectRef("WinWindow"), false);
        if (NullCheck(GetUIObjectRef("LoseWindow"))) ShowMenu(GetUIObjectRef("LoseWindow"), false);
        if (NullCheck(GetUIObjectRef("GameShopMenu")))
        {
            ShowMenu(GetUIObjectRef("GameShopMenu"), false);
            if (saveData._gameShopEnable == false)
                GetUIObjectRef("btnGameShop").SetActive(false);
        }
        if (NullCheck(GetUIObjectRef("Settings")))
        {
            ShowMenu(GetUIObjectRef("Settings"), false);
            GetUIObjectRef("Settings").GetComponent<SettingsMenu>().UpdateLocalization += Replay;
        }
    }
    private void SetLocalizationOnGUI()
    {
        if (NullCheck(GetUIObjectRef("textBalance")))
            GetUIObjectRef("textBalance").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("Balance");
        if (NullCheck(GetUIObjectRef("textResume")))
            GetUIObjectRef("textResume").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("Resume");
        if (NullCheck(GetUIObjectRef("textQuitGame")))
            GetUIObjectRef("textQuitGame").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("QuitGame");
        if (NullCheck(GetUIObjectRef("loseQuitGame")))
            GetUIObjectRef("loseQuitGame").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("QuitGame");
        if (NullCheck(GetUIObjectRef("winQuitGame")))
            GetUIObjectRef("winQuitGame").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("QuitGame");
        if (NullCheck(GetUIObjectRef("textReplay")))
            GetUIObjectRef("textReplay").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("Replay");
        if (NullCheck(GetUIObjectRef("textNextLevel")))
            GetUIObjectRef("textNextLevel").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("NextLevel");
        if (NullCheck(GetUIObjectRef("textMainMenu")))
            GetUIObjectRef("textMainMenu").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("MainMenu");
        if (NullCheck(GetUIObjectRef("textSettings")))
            GetUIObjectRef("textSettings").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("Settings");
        if (NullCheck(GetUIObjectRef("textPlayGame")))
            GetUIObjectRef("textPlayGame").GetComponent<TextMeshProUGUI>().text = _localization.GetGUIText("PlayGame");
        if (NullCheck(GetUIObjectRef("textAttackController")))
            GetUIObjectRef("textAttackController").GetComponent<TextMeshProUGUI>().text = _localization.GetControllerInfo("Attack");
        if (NullCheck(GetUIObjectRef("textJumpController")))
            GetUIObjectRef("textJumpController").GetComponent<TextMeshProUGUI>().text = _localization.GetControllerInfo("Jump");
        if (NullCheck(GetUIObjectRef("textLeftController")))
            GetUIObjectRef("textLeftController").GetComponent<TextMeshProUGUI>().text = _localization.GetControllerInfo("MoveToLeft");
        if (NullCheck(GetUIObjectRef("textRightController")))
            GetUIObjectRef("textRightController").GetComponent<TextMeshProUGUI>().text = _localization.GetControllerInfo("MoveToRight");
    }
}
