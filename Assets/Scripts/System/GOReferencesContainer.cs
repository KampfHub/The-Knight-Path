using UnityEngine;
public class GOReferencesContainer : MonoBehaviour
{
    private GameObject _playerRef, _soundsControllerRef, _GUIRef,
        _mainMenuPanel, _selectLevelMenuPanel, _loadPanel, _pausePanel, _gameShopPanel, _buttonsPanel,
        _endGamePanel, _settingsPanel, _infoPanel, _windowWin, _windowLose, _HUD,
        _btnBack, _btnNext, _btnGameShop, _btnLevelHightSlot, _btnLevelMiddleSlot, _btnLevelLowSlot,
        _textLevelHightSlot, _textLevelMiddleSlot, _textLevelLowSlot;
    private void Awake()
    {
        FindGameObjectsAndSetReferences();
    }
    private void FindGameObjectsAndSetReferences()
    {
        _playerRef = GameObject.FindWithTag("Player");
        _soundsControllerRef = GameObject.Find("SoundsController");
        _GUIRef = GameObject.Find("GUI");
        _HUD = GameObject.Find("HUD");
        _buttonsPanel = GameObject.Find("ButtonsPanel");
        _mainMenuPanel = GameObject.Find("MainMenu");
        _settingsPanel = GameObject.Find("SettingsMenu");
        _endGamePanel = GameObject.Find("EndGamePanel");
        _selectLevelMenuPanel = GameObject.Find("SelectLevelMenu");
        _infoPanel = GameObject.Find("InfoMenu");
        _loadPanel = GameObject.Find("LoadMenu");
        _pausePanel = GameObject.Find("PauseMenu");
        _gameShopPanel = GameObject.Find("GameShopMenu");
        _windowLose = GameObject.Find("LoseWindow");
        _windowWin = GameObject.Find("WinWindow");
        _textLevelHightSlot = GameObject.Find("textLevelHightSlot");
        _textLevelMiddleSlot = GameObject.Find("textLevelMiddleSlot");
        _textLevelLowSlot = GameObject.Find("textLevelLowSlot");
        _btnLevelHightSlot = GameObject.Find("btnLevelHightSlot");
        _btnLevelMiddleSlot = GameObject.Find("btnLevelMiddleSlot");
        _btnLevelLowSlot = GameObject.Find("btnLevelLowSlot");
        _btnGameShop = GameObject.Find("btnGameShop");
        _btnBack = GameObject.Find("btnBack");
        _btnNext = GameObject.Find("btnNext");       
    }
    //public bool NullCheck(GameObject gameObject) => gameObject is not null ? true : false;
    public GameObject GetPlayerRef() => _playerRef;
    public GameObject GetSoundControllerRef() => _soundsControllerRef;
    public GameObject GetGUIRef() => _GUIRef;
    public GameObject GetHUDRef() => _HUD;
    public GameObject GetButtonsPanelRef() => _buttonsPanel;
    public GameObject GetMainMenuPanelRef() => _mainMenuPanel;
    public GameObject GetSettingsPanelRef() => _settingsPanel;
    public GameObject GetEndGamePanelRef() => _endGamePanel;
    public GameObject GetSelectLevelMenuPanelRef() => _selectLevelMenuPanel;
    public GameObject GetInfoPanelRef() => _infoPanel;
    public GameObject GetLoadPanelRef() => _loadPanel;
    public GameObject GetPausePanelRef() => _pausePanel;
    public GameObject GetGameShopPanelRef() => _gameShopPanel;
    public GameObject GetWindowLoseRef() => _windowLose;
    public GameObject GetWindowWinRef() => _windowWin;
    public GameObject GetTextLevelHightSlotRef() => _textLevelHightSlot;
    public GameObject GetTextLevelMiddleSlotRef() => _textLevelMiddleSlot;
    public GameObject GetTextLevelLowSlotRef() => _textLevelLowSlot;
    public GameObject GetBtnLevelHightSlotRef() => _btnLevelHightSlot;
    public GameObject GetBtnLevelMiddleSlotRef() => _btnLevelMiddleSlot;
    public GameObject GetBtnLevelLowSlotRef() => _btnLevelLowSlot;
    public GameObject GetBtnGameShopRef() => _btnGameShop;
    public GameObject GetBtnBackRef() => _btnBack;
    public GameObject GetBtnNextRef() => _btnNext;
}
