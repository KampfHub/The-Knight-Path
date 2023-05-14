using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GeneralUI : MonoBehaviour
{
    public event VoidHandler JumpTrigger, AttackTrigger;
    public event IntConteiner UploadingCoins;
    public event TextConteiner UploadingDifficultyValue;
    private int availableLevel;
    private string currentLocolization; //buffer only
    private bool gameShopEnabled;
    private AsyncOperation asyncOperation;
    private Image loadProgressBar;
    private GameObject soundsControllerRef;
    private SoundsController soundsController;
    private Localization localization;
    private bool isPauseButtonPressed = false;
    private int LevelMenuState = 0;
    private GameObject playerRef,
        mainMenuPanel, selectLevelMenuPanel, loadPanel, pausePanel, gameShopPanel, buttonsPanel,
        endGamePanel, settingsPanel, infoPanel, btnBack, btnNext, windowWin, windowLose, HUD,
        btnGameShop, btnLevelHightSlot, btnLevelMiddleSlot, btnLevelLowSlot,
        textLevelHightSlot, textLevelMiddleSlot, textLevelLowSlot;
    private void Start()
    {
        LocalizationPreporation();
        LoadGame();
        FindAndSetUIComponents();
        SetLocalizationOnGUI();
        UIPreparation();
        SoundControllerPreporation();
    }
    public void SettingsButton()
    {
        if (isPauseButtonPressed)
        { 
            Pause(false);
            ShowMenu(pausePanel, false);
            ShowLevelMenu(false);
        }
        else
        {
            Pause(true);
            ShowMenu(pausePanel, true);
        }
        ClickSound();
    }
    public void JumpButton()
    {
        JumpTrigger();
    }
    public void AttackButton()
    {
        AttackTrigger();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenSelectLevelMenu()
    {
        ShowMenu(mainMenuPanel, false);
        ShowMenu(buttonsPanel, false);
        ShowLevelMenu(true);
    }
    public void ShowSettingsMenuBtnClick()
    {
        ShowMenu(settingsPanel, true);
        ShowMenu(mainMenuPanel, false);
        ClickSound();
    }
    public void ShowMainMenu()
    {
        ShowMenu(mainMenuPanel, true);
    }
    public void OpenLevelHSBtnClick()
    {
        StartCoroutine(LoadLevel(GetLevelId(btnLevelHightSlot.GetComponentInChildren<TextMeshProUGUI>().text)));
        ClickSound();
    }
    public void OpenLevelMSBtnClick()
    {
        StartCoroutine(LoadLevel(GetLevelId(btnLevelMiddleSlot.GetComponentInChildren<TextMeshProUGUI>().text)));
        ClickSound();
    }
    public void OpenLevelLSBtnClick()
    {
        StartCoroutine(LoadLevel(GetLevelId(btnLevelLowSlot.GetComponentInChildren<TextMeshProUGUI>().text)));
        ClickSound();
    }
    public void NextLevelBtnClick()
    {
        ShowMenu(windowWin, false);
        StartCoroutine(LoadLevel(availableLevel));
        ClickSound();
    }
    public void MainMenuBtnClick()
    {
        StartCoroutine(LoadLevel(0));
        ClickSound();
    }
    public void ShowInfoBtnClick()
    {
        ShowMenu(infoPanel, true);
        ShowMenu(buttonsPanel, false);
    }
    public void CloseInfoPanel()
    {
        ShowMenu(infoPanel, false);
        ShowMenu(buttonsPanel, true);
    }
    public void ShowWinWindow()
    {
        ShowMenu(windowWin, true);
        HideHUDandActionBtns();
    }
    public void ShowLoseWindow()
    {
        ShowMenu(windowLose, true);
        HideHUDandActionBtns();
    }
    public void RelpayBtnClick()
    {
        ShowMenu(windowLose, false);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        ClickSound();
    }
    public void CloseSelectLevelMenuBtnClick()
    {
        ShowLevelMenu(false);
        Pause(false);
        ClickSound();
    }
    public void NextBtnClick()
    {
        LevelMenuState++;
        ShowLevelMenu(true);
        ClickSound();
    }
    public void BackBtnClick()
    {
        LevelMenuState--;
        ShowLevelMenu(true);
        ClickSound();
    }
    public void EndGameMenu()
    {
        ShowMenu(HUD, false);
        ShowMenu(buttonsPanel, false);
        if (GONullCheck(playerRef))
        {
            playerRef.GetComponent<Player>().LockController(true);
            playerRef.GetComponent<Player>().StopMove();
        }
        soundsController.MuteLevelMusic();
        soundsController.PlaySound("GameOver", 0.7f);
        ShowMenu(endGamePanel, true);
    }
    public void UsePotion()
    {
        Impact impact = new Impact();
        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;
        int cost = Convert.ToInt32(buttonRef.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text);
        switch (buttonRef.name)
        { 
            case "btnPotion_1":
                {
                    impact._type = "Boost";
                    impact._name = "Power";
                    impact._value = 1.5f;
                    impact._duration = 10f;
                    break; 
                }
            case "btnPotion_2":
                {
                    impact._type = "Boost";
                    impact._name = "Speed";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    break; 
                }
            case "btnPotion_3":
                {
                    impact._type = "Boost";
                    impact._name = "JumpForce";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    break;
                }
            case "btnPotion_4":
                {
                    impact._type = "Boost";
                    impact._name = "HP";
                    impact._value = 33.3f;
                    impact._duration = 0f;
                    break;
                }
            case "btnPotion_5":
                {
                    impact._type = "Boost";
                    impact._name = "Defence";
                    impact._value = 30f;
                    impact._duration = 0f;
                    break;
                }
            case "btnPotion_6":
                {
                    impact._type = "Boost";
                    impact._name = "Immortal";
                    impact._value = 0f;
                    impact._duration = 10f;
                    break;
                }
        }
        if(GONullCheck(playerRef))
        {
            if(playerRef.GetComponent<Player>().GetCoinsValue() >= cost)
            {
                playerRef.GetComponent<Player>().SpendCoins(cost);
                SetWalletValue();
                playerRef.GetComponent<Player>().GetImpact(impact);
            }  
        }
    }
    public void GameShopBtnClick()
    {
        ShowMenu(gameShopPanel, true);
        Pause(true);
        FillGameShop();
        SetWalletValue();
        SetLocalizationOnGUI();
        ClickSound();
    }
    public void CloseGameShopBtnClick()
    {
        ShowMenu(gameShopPanel, false);
        Pause(false);
        ClickSound();
    }
    public void AvailableLevelUpgrade(int level)
    {
        availableLevel = level;
    }
    public int GetAvailableLevel() => availableLevel;
    private void Pause(bool pauseState)
    {
        if (pauseState) Time.timeScale = 0;
        else Time.timeScale = 1;
        isPauseButtonPressed = pauseState;
        ShowMenu(buttonsPanel, !pauseState);
        if (GONullCheck(playerRef)) playerRef.GetComponent<Player>().LockController(pauseState);
    }
    private void FindAndSetUIComponents()
    {
        playerRef = GameObject.FindWithTag("Player");
        HUD = GameObject.Find("HUD");
        buttonsPanel = GameObject.Find("ButtonsPanel");
        mainMenuPanel = GameObject.Find("MainMenu");
        settingsPanel = GameObject.Find("SettingsMenu");
        endGamePanel = GameObject.Find("EndGamePanel");
        selectLevelMenuPanel = GameObject.Find("SelectLevelMenu");
        infoPanel = GameObject.Find("InfoMenu");
        loadPanel = GameObject.Find("LoadMenu");
        pausePanel = GameObject.Find("PauseMenu");
        gameShopPanel = GameObject.Find("GameShopMenu");
        windowLose = GameObject.Find("LoseWindow");
        windowWin = GameObject.Find("WinWindow");
        textLevelHightSlot = GameObject.Find("textLevelHightSlot");
        textLevelMiddleSlot = GameObject.Find("textLevelMiddleSlot");
        textLevelLowSlot = GameObject.Find("textLevelLowSlot");
        btnLevelHightSlot = GameObject.Find("btnLevelHightSlot");
        btnLevelMiddleSlot = GameObject.Find("btnLevelMiddleSlot");
        btnLevelLowSlot = GameObject.Find("btnLevelLowSlot");
        btnGameShop = GameObject.Find("btnGameShop");
        btnBack = GameObject.Find("btnBack");
        btnNext = GameObject.Find("btnNext");
        if (GONullCheck(loadPanel))
        {
            loadProgressBar = GameObject.Find("LoadProgressBar").GetComponent<Image>();
        }
    }
    private bool GONullCheck(GameObject gameObject) => gameObject is not null ? true : false;
    private IEnumerator LoadLevel(int levelId)
    {
        yield return null;
        selectLevelMenuPanel.SetActive(false);
        loadPanel.SetActive(true);
        if(isPauseButtonPressed) Pause(false);
        asyncOperation = SceneManager.LoadSceneAsync(levelId);
        while (!asyncOperation.isDone)
        {
            float loadProgress = asyncOperation.progress / 0.9f;
            loadProgressBar.fillAmount = loadProgress;
            yield return null;
        }
    }
    private void LoadGame()
    {
        GameObject gameDataController = GameObject.Find("GameDataController");
        GameObject playerRef = GameObject.FindWithTag("Player");
        if (gameDataController is not null)
        {
            SaveData saveData = gameDataController.GetComponent<GameDataController>().LoadData();
            availableLevel = saveData._level;
            currentLocolization = saveData._language;
            gameShopEnabled = saveData._gameShopEnable;
            if (playerRef is not null)
            {
                UploadingCoins(saveData._coins);
                UploadingDifficultyValue(saveData._difficulty);
            }
        }
    }
    private void UIPreparation()
    {
        HideUIController(); // only PC build
        if (GONullCheck(selectLevelMenuPanel))
        {
            ShowLevelMenu(false);
            HideAllLevelMenuButtons();
        }
        if (GONullCheck(loadPanel)) ShowMenu(loadPanel, false);
        if (GONullCheck(infoPanel)) ShowMenu(infoPanel, false);
        if (GONullCheck(endGamePanel)) ShowMenu(endGamePanel, false);
        if (GONullCheck(pausePanel)) ShowMenu(pausePanel, false);
        if (GONullCheck(windowWin)) ShowMenu(windowWin, false);
        if (GONullCheck(windowLose)) ShowMenu(windowLose, false);
        if (GONullCheck(gameShopPanel))
        {
           ShowMenu(gameShopPanel, false);
           if (!gameShopEnabled) btnGameShop.SetActive(false);
        } 
        if (GONullCheck(settingsPanel))
        {
            ShowMenu(settingsPanel, false);
            settingsPanel.GetComponent<SettingsMenu>().UpdateLocalization += RelpayBtnClick;
        }
        if (availableLevel == 0) availableLevel = 1;  
    }
    private int GetLevelId(string btnTextLevel) => localization.LevelNumberConstructor(btnTextLevel);
    private void HideUIController()
    {                                       // only PC build
        if (GONullCheck(buttonsPanel))
        {
            GameObject.Find("btnJump").SetActive(false);
            GameObject.Find("btnAttack").SetActive(false);
            GameObject.Find("btnMoveToLeft").SetActive(false);
            GameObject.Find("btnMoveToRight").SetActive(false);
        }
    }
    private void ShowLevelMenu(bool state)
    {
        if (GONullCheck(selectLevelMenuPanel))
        {
            selectLevelMenuPanel.SetActive(state);
            LoadGame();
            ShowLevelMenuButtons();
            switch (LevelMenuState)
            {
                default:
                    {
                        SetTextLevelMenu(1);
                        btnBack.SetActive(false);
                        btnNext.SetActive(availableLevel < 4 ? false : true);
                        break;
                    }
                case 1:
                    {
                        SetTextLevelMenu(4);
                        btnBack.SetActive(true);
                        btnNext.SetActive(availableLevel < 7 ? false : true);
                        break;
                    }
                case 2:
                    {
                        SetTextLevelMenu(7);
                        btnBack.SetActive(true);
                        btnNext.SetActive(false);
                        break;
                    }
            }
            HideUnavailableLevel();
            ShowMenu(pausePanel, false);
        } 
        
    }
    private void SetTextLevelMenu(int initialRange)
    {
        textLevelHightSlot.GetComponent<TextMeshProUGUI>().text = localization.TextLevelButtons(initialRange);
        textLevelMiddleSlot.GetComponent<TextMeshProUGUI>().text = localization.TextLevelButtons(initialRange + 1);
        textLevelLowSlot.GetComponent<TextMeshProUGUI>().text = localization.TextLevelButtons(initialRange + 2);
    }
    private void ShowLevelMenuButtons()
    {
        btnLevelHightSlot.SetActive(true);
        btnLevelMiddleSlot.SetActive(true);
        btnLevelLowSlot.SetActive(true); 
    }
    private void HideUnavailableLevel()
    {

        if(availableLevel < GetLevelId(btnLevelHightSlot.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                btnLevelHightSlot.SetActive(false);          
            }       
        if(availableLevel < GetLevelId(btnLevelMiddleSlot.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                btnLevelMiddleSlot.SetActive(false);
            }
        if(availableLevel < GetLevelId(btnLevelLowSlot.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                btnLevelLowSlot.SetActive(false);
            }
    }
    private void HideAllLevelMenuButtons()
    {
        if(GONullCheck(selectLevelMenuPanel))
        {
            btnLevelHightSlot.SetActive(false);
            btnLevelMiddleSlot.SetActive(false);
            btnLevelLowSlot.SetActive(false);
            btnNext.SetActive(false);
            btnBack.SetActive(false);
        }  
    }
    private void FillGameShop()
    {
        FillPlaceInGameShop(1, localization.GetPotionName("Power"), localization.GetPotionDescription("Power"), 2);
        FillPlaceInGameShop(2, localization.GetPotionName("Speed"), localization.GetPotionDescription("Speed"), 2);
        FillPlaceInGameShop(3, localization.GetPotionName("JumpForce"), localization.GetPotionDescription("JumpForce"), 2);
        FillPlaceInGameShop(4, localization.GetPotionName("HP"), localization.GetPotionDescription("HP"), 1);
        FillPlaceInGameShop(5, localization.GetPotionName("Defense"), localization.GetPotionDescription("Defense"), 1);
        FillPlaceInGameShop(6, localization.GetPotionName("Immortal"), localization.GetPotionDescription("Immortal"), 4);
    }
    private void FillPlaceInGameShop(int slot, string potionName, string potionDescription, int price)
    {
        GameObject slotPotion = GameObject.Find($"btnPotion_{slot}");
        if(GONullCheck(slotPotion))
        {
            slotPotion.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = potionName;
            slotPotion.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = potionDescription;
            slotPotion.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(price);
        }
    }
    private void SetWalletValue()
    {
        GameObject coins = GameObject.Find("textCoinsValue");
        if (GONullCheck(coins) && GONullCheck(playerRef))
        {
            coins.GetComponent<TextMeshProUGUI>().text = Convert.ToString(playerRef.GetComponent<Player>().GetCoinsValue());
        }
    }
    private void HideHUDandActionBtns()
    {
        if (GONullCheck(HUD)) HUD.SetActive(false);
        ShowMenu(buttonsPanel, false);
    }
    private void LocalizationPreporation()
    {
        if (GetComponent<Localization>() is not null)
        {
            localization = GetComponent<Localization>();
        }
        else
        {
            gameObject.AddComponent<Localization>();
            localization = GetComponent<Localization>();
        }
    }
    private void SetLocalizationOnGUI()
    {
        if (GONullCheck(GameObject.Find("textBalance"))) GameObject.Find("textBalance").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("Balance");
        if (GONullCheck(GameObject.Find("textResume"))) GameObject.Find("textResume").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("Resume");
        if (GONullCheck(GameObject.Find("textQuitGame"))) GameObject.Find("textQuitGame").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("QuitGame");
        if (GONullCheck(GameObject.Find("loseQuitGame"))) GameObject.Find("loseQuitGame").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("QuitGame");
        if (GONullCheck(GameObject.Find("winQuitGame"))) GameObject.Find("winQuitGame").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("QuitGame");
        if (GONullCheck(GameObject.Find("textReplay"))) GameObject.Find("textReplay").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("Replay");
        if (GONullCheck(GameObject.Find("textNextLevel"))) GameObject.Find("textNextLevel").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("NextLevel");
        if (GONullCheck(GameObject.Find("textMainMenu"))) GameObject.Find("textMainMenu").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("MainMenu");
        if (GONullCheck(GameObject.Find("textSettings"))) GameObject.Find("textSettings").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("Settings");
        if (GONullCheck(GameObject.Find("textPlayGame"))) GameObject.Find("textPlayGame").GetComponent<TextMeshProUGUI>().text = localization.GetGUIText("PlayGame");
        if (GONullCheck(GameObject.Find("textAttackController")))
            GameObject.Find("textAttackController").GetComponent<TextMeshProUGUI>().text = localization.GetControllerInfo("Attack");
        if (GONullCheck(GameObject.Find("textJumpController")))
            GameObject.Find("textJumpController").GetComponent<TextMeshProUGUI>().text = localization.GetControllerInfo("Jump");
        if (GONullCheck(GameObject.Find("textLeftController")))
            GameObject.Find("textLeftController").GetComponent<TextMeshProUGUI>().text = localization.GetControllerInfo("MoveToLeft");
        if (GONullCheck(GameObject.Find("textRightController")))
            GameObject.Find("textRightController").GetComponent<TextMeshProUGUI>().text = localization.GetControllerInfo("MoveToRight");
    }
    private void ShowMenu(GameObject menuPanel, bool state)
    {
        if (GONullCheck(menuPanel)) menuPanel.SetActive(state);
    }
    private void SoundControllerPreporation()
    {
        if (GONullCheck(GameObject.Find("SoundsController")))
        {
            soundsControllerRef = GameObject.Find("SoundsController");
            soundsController = soundsControllerRef.GetComponent<SoundsController>();
        }
    }
    private void ClickSound()
    {
        soundsController.PlaySound("Click");
    }
}
