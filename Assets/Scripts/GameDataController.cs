using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[System.Serializable]
public struct SaveData
{
    public int coins { get; set; }
    public int level { get; set; }
    public string difficulty { get; set; }
    public bool gameShopEnable { get; set; }
    public string language { get; set; }
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
            SaveData saveData = new SaveData();
            saveData = (SaveData)bf.Deserialize(file);
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
            saveData.level = 1;
            saveData.coins = 0;
            saveData.gameShopEnable = true;
            saveData.difficulty = "Random";
            saveData.language = "ENG";
            bf.Serialize(file, saveData);
            file.Close();
            Debug.Log("Available Level = 1, Coins = 0");
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
