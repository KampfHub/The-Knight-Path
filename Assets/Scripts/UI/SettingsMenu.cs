using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    private GameObject gameDataController;
    private SaveData dataBuffer;
    void Start()
    {
        FindAndSetDataController();
        LoadData();
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
            gameDataController.GetComponent<GameDataController>().SaveData(saveData);
        }       
    }
    private void FindAndSetDataController()
    {
        if(GameObject.Find("GameDataController") is not null)
        {
            gameDataController = GameObject.Find("GameDataController");
        }
    }
}
