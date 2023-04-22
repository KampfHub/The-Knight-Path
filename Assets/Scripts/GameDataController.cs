using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public struct SaveData
{   
    private string difficulty, language;
    public int _coins { get; set; }
    public int _level { get; set; }
    public bool _gameShopEnable { get; set; }
    public bool _soundsEnable { get; set; }
    public string _difficulty
    {
        set { if (value is null) difficulty = "Random"; else difficulty = value; }
        get { return difficulty; }
    }
    public string _language
    {
        set { if (value is null) language = "ENG"; else language = value; }
        get { return language; }
    }

}
[System.Serializable]
public class GameDataController : MonoBehaviour
{
    public void SaveData(SaveData saveData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
        + "/SaveData.dat");
        bf.Serialize(file, saveData);
        file.Close();
        Debug.Log("Game data saved!");
    }

    public SaveData LoadData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/SaveData.dat", FileMode.Open);
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game data loaded!");
            return saveData;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath
              + "/SaveData.dat");
            SaveData saveData = new SaveData();
            saveData._level = 1;
            saveData._coins = 0;
            saveData._gameShopEnable = true;
            saveData._soundsEnable = true;
            saveData._difficulty = "Random";
            saveData._language = "ENG";
            bf.Serialize(file, saveData);
            file.Close();
            Debug.Log("Create new save data!");
            return saveData;
        }
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/SaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
              + "/SaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
            Debug.Log("No save data to delete.");
    }
}
