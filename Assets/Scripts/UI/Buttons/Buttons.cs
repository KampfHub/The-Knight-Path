using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] private int _availableLevel;
    AsyncOperation asyncOperation;
    Image loadProgressBar;
    public event PlayerTrigger JumpTrigger;
    public event PlayerTrigger AttackTrigger;
    private bool isPauseButtonPressed = false;
    private int LevelMenuState = 0;
    private GameObject
        mainMenuPanel, selectLevelMenuPanel, loadPanel,
        btnBack, btnNext,
        btnLevelHightSlot, btnLevelMiddleSlot, btnLevelLowSlot, 
        textLevelHightSlot, textLevelMiddleSlot, textLevelLowSlot;
    void Start()
    {
        FindAndSetUIComponents();
        if (GONullCheck(selectLevelMenuPanel))
        {
            ShowLevelMenu(false);
            HideAllLevelMenuButtons();
        }
        if (GONullCheck(loadPanel)) ShowLoadMenu(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TestClick()
    {
        //todo
    }
    public void SettingsButton()
    {
        if (isPauseButtonPressed)
        { 
            Pause(false);
            ShowLevelMenu(false);
        }
        else
        {
            Pause(true);
            ShowLevelMenu(true);
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
        ShowMainMenu(false);
        ShowLevelMenu(true);
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
    private void Pause(bool pauseState)
    {
        if (pauseState) Time.timeScale = 0;
        else Time.timeScale = 1;
        isPauseButtonPressed = pauseState;
    }
    private void FindAndSetUIComponents()
    {
        mainMenuPanel = GameObject.Find("MainMenu");
        selectLevelMenuPanel = GameObject.Find("SelectLevelMenu");
        loadPanel = GameObject.Find("LoadMenu");
        textLevelHightSlot = GameObject.Find("textLevelHightSlot");
        textLevelMiddleSlot = GameObject.Find("textLevelMiddleSlot");
        textLevelLowSlot = GameObject.Find("textLevelLowSlot");
        btnLevelHightSlot = GameObject.Find("btnLevelHightSlot");
        btnLevelMiddleSlot = GameObject.Find("btnLevelMiddleSlot");
        btnLevelLowSlot = GameObject.Find("btnLevelLowSlot");
        btnBack = GameObject.Find("btnBack");
        btnNext = GameObject.Find("btnNext");
        if(GONullCheck(loadPanel))
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
            ShowLevelMenuButtons();
            switch (LevelMenuState)
            {
                default:
                    {
                        SetTextLevelMenu(1);
                        btnBack.SetActive(false);
                        btnNext.SetActive(_availableLevel < 4 ? false : true);
                        break;
                    }
                case 1:
                    {
                        SetTextLevelMenu(4);
                        btnBack.SetActive(true);
                        btnNext.SetActive(_availableLevel < 7 ? false : true);
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

        if(_availableLevel < GetLevelId(btnLevelHightSlot.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                btnLevelHightSlot.SetActive(false);          
            }       
        if(_availableLevel < GetLevelId(btnLevelMiddleSlot.GetComponentInChildren<TextMeshProUGUI>().text))
            {
                btnLevelMiddleSlot.SetActive(false);
            }
        if(_availableLevel < GetLevelId(btnLevelLowSlot.GetComponentInChildren<TextMeshProUGUI>().text))
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
    private void ShowLoadMenu(bool state)
    {
        if (GONullCheck(loadPanel)) loadPanel.SetActive(state);
    }
    private void ShowMainMenu(bool state)
    {
        if (GONullCheck(mainMenuPanel)) mainMenuPanel.SetActive(state);
    }
}
