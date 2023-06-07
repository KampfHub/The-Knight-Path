using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public event VoidConteiner UpdateLocalization;
    private GameObject gameDataController, soundsControllerRef, GUIRef, panelENG, panelRUS,
        btnSoundsOn, btnSoundsOff, btnGameMode, btnDifficulty, btnLanguage;
    private SaveData dataBuffer;
    private SoundsController soundsController;
    private Localization localization;
    void Start()
    {
        FindAndSetGameObjRefs();
        LoadData();
        LocalizationPreporation();
        SettingsMenuOpenPrep();
    }
    public void CloseBtnClick()
    {
        GUIRef.GetComponent<GeneralUI>().ShowMainMenu();
        gameObject.SetActive(false);
        ClickSound();
    }
    public void SaveChangesBtnClick()
    {
        SaveData();
        UpdateLocalization();
        ClickSound();
    }
    public void SwitchDifficultyBtnKclick()
    {
        switch (dataBuffer._difficulty)
        {
            case "Easy":
                {
                    dataBuffer._difficulty = "Normal";
                    break;
                }
            case "Normal":
                {
                    dataBuffer._difficulty = "Hard";
                    break;
                }
            case "Hard":
                {
                    dataBuffer._difficulty = "Random";
                    break;
                }
            case "Random":
                {
                    dataBuffer._difficulty = "Easy";
                    break;
                }
            default: break;    
        }
        btnDifficulty.transform.GetComponentInChildren<TextMeshProUGUI>().text = GetBtnText(btnDifficulty);
        ClickSound();
    }
    public void SwitchLanguageBtnClick()
    {
        switch (dataBuffer._language)
        {
            case "RUS": 
                {
                    dataBuffer._language = "ENG";
                    panelRUS.SetActive(false);
                    panelENG.SetActive(true); 
                    break;
                } 
            case "ENG":
                {
                    dataBuffer._language = "RUS";
                    panelRUS.SetActive(true);
                    panelENG.SetActive(false);
                    break;
                }
            default: break;
        }
        ClickSound();
    }
    public void ToggleSoundsVolumeBtnClick()
    {
        if (dataBuffer._soundsEnable)
        {
            dataBuffer._soundsEnable = false;
            soundsController.ToogleSoundsEnabled(false);
            btnSoundsOff.SetActive(true);
            btnSoundsOn.SetActive(false);
        }
        else
        {
            dataBuffer._soundsEnable = true;
            soundsController.ToogleSoundsEnabled(true);
            btnSoundsOff.SetActive(false);
            btnSoundsOn.SetActive(true);
        }
        ClickSound();
    }
    public void ToggleGameModeBtnClick()
    {
        if(dataBuffer._gameShopEnable) dataBuffer._gameShopEnable = false;
        else dataBuffer._gameShopEnable = true;
        btnGameMode.transform.GetComponentInChildren<TextMeshProUGUI>().text = GetBtnText(btnGameMode);
        ClickSound();
    }
    private void SettingsMenuOpenPrep()
    {
        switch (dataBuffer._language)
        {
            case "RUS": panelENG.SetActive(false); break;
            case "ENG": panelRUS.SetActive(false); break;
            default: break;
        }
        if (dataBuffer._soundsEnable) btnSoundsOff.SetActive(false);
        if (!dataBuffer._soundsEnable) btnSoundsOn.SetActive(false);
        btnDifficulty.transform.GetComponentInChildren<TextMeshProUGUI>().text = GetBtnText(btnDifficulty);
        btnGameMode.transform.GetComponentInChildren<TextMeshProUGUI>().text = GetBtnText(btnGameMode);
    }
    private void LoadData()
    {
        if (gameDataController is not null)
        {
            SaveData saveData = gameDataController.GetComponent<GameDataController>().LoadData();
            dataBuffer._difficulty = saveData._difficulty;
            dataBuffer._language = saveData._language;
            dataBuffer._gameShopEnable= saveData._gameShopEnable;
            dataBuffer._soundsEnable= saveData._soundsEnable;
            dataBuffer._level = saveData._level;
            dataBuffer._coins = saveData._coins;
        }
    }
    
    private void SaveData()
    {
        if (gameDataController is not null)
        {
            SaveData saveData = new SaveData();
            saveData._difficulty = dataBuffer._difficulty;
            saveData._language = dataBuffer._language;
            saveData._soundsEnable = dataBuffer._soundsEnable;
            saveData._gameShopEnable = dataBuffer._gameShopEnable;
            saveData._level = dataBuffer._level;
            saveData._coins = dataBuffer._coins;
            gameDataController.GetComponent<GameDataController>().SaveData(saveData);
        }       
    }
    private string GetBtnText(GameObject buttonRef)
    {
        switch(buttonRef.name)
        {
            case "btnDifficulty":
                {
                    return localization.TextSettingsButtons(btnDifficulty.name, dataBuffer._difficulty);
                }
            case "btnGameMode":
                {
                    return localization.TextSettingsButtons(btnGameMode.name, dataBuffer._gameShopEnable.ToString());
                }
            default: return "Error!";
        }
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
    private void FindAndSetGameObjRefs()
    {
        if(GameObject.Find("GameDataController") is not null)
        {
            gameDataController = GameObject.Find("GameDataController");
        }
        if (GameObject.Find("MainMenuCamera") is not null)
        {
            GUIRef = GameObject.Find("MainMenuCamera");
        }
        if (GameObject.Find("btnLanguage") is not null)
        {
            btnLanguage = GameObject.Find("btnLanguage");
        }
        if (GameObject.Find("btnDifficulty") is not null)
        {
            btnDifficulty = GameObject.Find("btnDifficulty");
        }
        if (GameObject.Find("btnGameMode") is not null)
        {
            btnGameMode = GameObject.Find("btnGameMode");
        }
        if (GameObject.Find("btnMusicOnToogle") is not null)
        {
            btnSoundsOn = GameObject.Find("btnMusicOnToogle");
        }
        if (GameObject.Find("btnMusicOffToogle") is not null)
        {
            btnSoundsOff = GameObject.Find("btnMusicOffToogle"); 
        }
        if (GameObject.Find("ENGpanel") is not null)
        {
            panelENG = GameObject.Find("ENGpanel");
        }
        if (GameObject.Find("RUSpanel") is not null)
        {
            panelRUS = GameObject.Find("RUSpanel");
        }
        if (GameObject.Find("SoundsController") is not null)
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
