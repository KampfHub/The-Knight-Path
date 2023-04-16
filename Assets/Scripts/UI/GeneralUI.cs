using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[System.Serializable]
public class GeneralUI : MonoBehaviour
{
    private int availableLevel;
    AsyncOperation asyncOperation;
    Image loadProgressBar;
    public event PlayerTrigger JumpTrigger, AttackTrigger;
    public event LoadCoinConteiner UploadingCoins;
    public event LoadTextConteiner UploadingDifficultyValue;
    private bool isPauseButtonPressed = false;
    private int LevelMenuState = 0;
    private GameObject playerRef,
        mainMenuPanel, selectLevelMenuPanel, loadPanel, pausePanel, gameShopPanel, buttonsPanel,
        endGamePanel, settingsPanel, btnBack, btnNext, windowWin, windowLose, HUD,
        btnLevelHightSlot, btnLevelMiddleSlot, btnLevelLowSlot, 
        textLevelHightSlot, textLevelMiddleSlot, textLevelLowSlot;
    private void Start()
    {
        LoadGame();
        FindAndSetUIComponents();
        UIPreparation();
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
        ShowLevelMenu(true);
    }
    public void ShowSettingsMenuBtnClick()
    {
        ShowMenu(settingsPanel, true);
        ShowMenu(mainMenuPanel, false);
    }
    public void OpenLevelHSBtnClick()
    {
        StartCoroutine(LoadLevel(GetLevelId(btnLevelHightSlot.GetComponentInChildren<TextMeshProUGUI>().text)));
    }
    public void OpenLevelMSBtnClick()
    {
        StartCoroutine(LoadLevel(GetLevelId(btnLevelMiddleSlot.GetComponentInChildren<TextMeshProUGUI>().text)));
    }
    public void OpenLevelLSBtnClick()
    {
        StartCoroutine(LoadLevel(GetLevelId(btnLevelLowSlot.GetComponentInChildren<TextMeshProUGUI>().text)));
    }
    public void NextLevelBtnClick()
    {
        StartCoroutine(LoadLevel(availableLevel));
    }
    public void MainMenuBtnClick()
    {
        StartCoroutine(LoadLevel(0));
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
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    public void CloseSelectLevelMenuBtnClick()
    {
        ShowLevelMenu(false);
        Pause(false);
    }
    public void NextBtnClick()
    {
        LevelMenuState++;
        ShowLevelMenu(true);
    }
    public void BackBtnClick()
    {
        LevelMenuState--;
        ShowLevelMenu(true);
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
        ShowMenu(endGamePanel, true);
    }
    public void UsePotion()
    {
        Impact impact = new Impact();
        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;
        string buttonName = buttonRef.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text;
        int cost = Convert.ToInt32(buttonRef.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text);
        switch (buttonName)
        { 
            case "Potion of Power":
                {
                    impact._type = "Boost";
                    impact._name = "Power";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    break; 
                }
            case "Potion of Speed":
                {
                    impact._type = "Boost";
                    impact._name = "Speed";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    break; 
                }
            case "Mixture of Lightness":
                {
                    impact._type = "Boost";
                    impact._name = "JumpForce";
                    impact._value = 1.25f;
                    impact._duration = 10f;
                    break;
                }
            case "Elixir of Health":
                {
                    impact._type = "Boost";
                    impact._name = "HP";
                    impact._value = 33.3f;
                    impact._duration = 0f;
                    break;
                }
            case "Elixir of Forteresses":
                {
                    impact._type = "Boost";
                    impact._name = "Defence";
                    impact._value = 30f;
                    impact._duration = 0f;
                    break;
                }
            case "Mixture of Immortality":
                {
                    impact._type = "Boost";
                    impact._name = "Immortal";
                    impact._value = 0f;
                    impact._duration = 20f;
                    break;
                }
        }
        if(GONullCheck(playerRef))
        {
            if(playerRef.GetComponent<Player>().GetCoinsValue() >= cost)
            {
                playerRef.GetComponent<Player>().SpendCoins(cost);
                SetWalletValue();
                playerRef.GetComponent<Character>().GetImpact(impact);
                playerRef.GetComponent<Character>().HealthWidgetTrigger();
            }  
        }
    }
    public void GameShopBtnClick()
    {
        ShowMenu(gameShopPanel, true);
        Pause(true);
        FillGameShop();
        SetWalletValue();
    }
    public void CloseGameShopBtnClick()
    {
        ShowMenu(gameShopPanel, false);
        Pause(false);

    }
    public void AvailableLevelUpgrade(int level)
    {
        availableLevel = level;
    }
    public int GetAvailableLevel()
    {
        return availableLevel;
    }
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
        btnBack = GameObject.Find("btnBack");
        btnNext = GameObject.Find("btnNext");
        if (GONullCheck(loadPanel))
        {
            loadProgressBar = GameObject.Find("LoadProgressBar").GetComponent<Image>();
        }
    }
    private bool GONullCheck(GameObject gameObject)
    {
        return gameObject is not null ? true : false;
    }
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
            availableLevel = saveData.level;
            if (playerRef is not null)
            {
                UploadingCoins(saveData.coins);
                UploadingDifficultyValue(saveData.difficulty);
            }
        }
    }
    private void UIPreparation()
    {
        if (GONullCheck(selectLevelMenuPanel))
        {
            ShowLevelMenu(false);
            HideAllLevelMenuButtons();
        }
        if (GONullCheck(loadPanel)) ShowMenu(loadPanel, false);
        if (GONullCheck(endGamePanel)) ShowMenu(endGamePanel, false);
        if (GONullCheck(pausePanel)) ShowMenu(pausePanel, false);
        if (GONullCheck(windowWin)) ShowMenu(windowWin, false);
        if (GONullCheck(windowLose)) ShowMenu(windowLose, false);
        if (GONullCheck(gameShopPanel)) ShowMenu(gameShopPanel, false);
        if (GONullCheck(settingsPanel)) ShowMenu(settingsPanel, false);
        if (availableLevel == 0) availableLevel = 1;  
    }
    private int GetLevelId(string btnTextLevel)
    {
        btnTextLevel = btnTextLevel.Trim(new char[] { 'L','e','v','l',' '});
        return Int32.TryParse(btnTextLevel, out int j) ? j : 0;
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
        textLevelHightSlot.GetComponent<TextMeshProUGUI>().text = $"Level {initialRange}";
        textLevelMiddleSlot.GetComponent<TextMeshProUGUI>().text = $"Level {initialRange + 1}";
        textLevelLowSlot.GetComponent<TextMeshProUGUI>().text = $"Level {initialRange + 2}";
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
        FillPlaceInGameShop(1, "Potion of Power", "Increases attack power by 25% for 10 seconds", 2);
        FillPlaceInGameShop(2, "Potion of Speed", "Increases movement speed by 25% for 10 seconds", 2);
        FillPlaceInGameShop(3, "Mixture of Lightness", "Increases jump force by 25% for 10 seconds", 2);
        FillPlaceInGameShop(4, "Elixir of Health", "Restores 33.3% of the maximum amount of health", 1);
        FillPlaceInGameShop(5, "Elixir of Forteresses", "Gives 30 armor points", 1);
        FillPlaceInGameShop(6, "Mixture of Immortality", "Gives invulnerability for 20 seconds", 4);
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
    private void ShowMenu(GameObject menuPanel, bool state)
    {
        if (GONullCheck(menuPanel)) menuPanel.SetActive(state);
    }
}
