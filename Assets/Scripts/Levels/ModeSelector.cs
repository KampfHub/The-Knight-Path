using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ModeSelector : MonoBehaviour
{
    private string gameDifficulty;
    GameObject easyModePreset, normalModePreset, hardModePreset, randomModePreset;
    private void Awake()
    {
        SearchModePresets();
        HideAllModesPresets();
        LoadData();
        SetGameDifficulty();
    }
    private void Start()
    {
        if (gameDifficulty is null) gameDifficulty = "Random";
    }
    public string GetCurrentGameDifficulty()
    {
        return gameDifficulty;
    }
    private void LoadData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/SaveData.dat", FileMode.Open);
            SaveData saveData = new SaveData();
            saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            gameDifficulty = saveData.difficulty;
            Debug.Log(gameDifficulty);
        }
    }
    private void SetGameDifficulty()
    {
        switch(gameDifficulty)
        {
            case "Easy":
                { 
                    easyModePreset.SetActive(true);
                    Debug.Log("Easy mode has started");
                    break; 
                }
            case "Normal":
                {
                    normalModePreset.SetActive(true);
                    Debug.Log("Normal mode has started");
                    break; 
                }
            case "Hard":
                {
                    hardModePreset.SetActive(true);
                    Debug.Log("Hard mode has started");
                    break; 
                }
            case "Random":
                { 
                    randomModePreset.SetActive(true);
                    Debug.Log("Random mode has started");
                    break; 
                }
            default:
                {
                    randomModePreset.SetActive(true);
                    Debug.Log("Random mode has started");
                    break;
                }
            }
    }
    private void SearchModePresets()
    {
        if (GONullCheck(GameObject.Find("EasyMode"))) easyModePreset = GameObject.Find("EasyMode");
        if (GONullCheck(GameObject.Find("NormalMode"))) normalModePreset = GameObject.Find("NormalMode");
        if (GONullCheck(GameObject.Find("HardMode"))) hardModePreset = GameObject.Find("HardMode");
        if (GONullCheck(GameObject.Find("RandomMode"))) randomModePreset = GameObject.Find("RandomMode");
    }
    private void HideAllModesPresets()
    {
        easyModePreset.SetActive(false);
        normalModePreset.SetActive(false);
        hardModePreset.SetActive(false);
        randomModePreset.SetActive(false);
    }
    private bool GONullCheck(GameObject go)
    {
        return go is not null ? true : false;
    }
}
