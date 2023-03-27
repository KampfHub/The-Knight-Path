using System.Collections;
using TMPro;
using UnityEngine;
public class Buttons : MonoBehaviour
{
    public event PlayerTrigger JumpTrigger;
    public event PlayerTrigger AttackTrigger;
    private bool isPauseButtonPressed = false;
    private int LevelMenuState = 0;
    private GameObject mainMenuPanel, selectLevelMenuPanel, textLevelHightSlot, textLevelMiddleSlot, textLevelLowSlot,
        btnBack, btnNext;
    void Start()
    {
        FindAndSetUIComponents();
        ShowLevelMenu(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SettingsButton()
    {
        if (isPauseButtonPressed) Pause(false);
        else Pause(true);
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
    public void TestClick()
    {
        
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
        textLevelHightSlot = GameObject.Find("textLevelHightSlot");
        textLevelMiddleSlot = GameObject.Find("textLevelMiddleSlot");
        textLevelLowSlot = GameObject.Find("textLevelLowSlot");
        btnBack = GameObject.Find("btnBack");
        btnNext = GameObject.Find("btnNext");

    }
    private void ShowLevelMenu(bool state)
    {
        if (selectLevelMenuPanel is not null) selectLevelMenuPanel.SetActive(state);
        switch (LevelMenuState)
        {
            default:
                {
                    SetTextLevelMenu(1);
                    btnBack.SetActive(false);
                    btnNext.SetActive(true);
                    break;
                }
            case 1:
                {
                    SetTextLevelMenu(4);
                    btnBack.SetActive(true);
                    btnNext.SetActive(true);
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
        
    }
    private void SetTextLevelMenu(int initialRange)
    {
        textLevelHightSlot.GetComponent<TextMeshProUGUI>().text = $"Level {initialRange}";
        textLevelMiddleSlot.GetComponent<TextMeshProUGUI>().text = $"Level {initialRange + 1}";
        textLevelLowSlot.GetComponent<TextMeshProUGUI>().text = $"Level {initialRange + 2}";   
    }
    private void ShowMainMenu(bool state)
    {
        if (mainMenuPanel is not null) mainMenuPanel.SetActive(state);
    }
}
