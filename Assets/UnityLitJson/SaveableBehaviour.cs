using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

[System.Serializable]
public class SaveData
{
    public List<string> id = new List<string>();
    public List<string> jsonData = new List<string>();
}


public class SaveableBehaviour : MonoBehaviour
{
    private string filePath;
    private static string directory = "ScoreData";
    private static string fileName = "playerScore.txt";

    [SerializeField] ScoreData scoreData;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log($"Saved to: {filePath}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadGameSave();
        }
    }

    private void Save()
    {
        var allSaveableObjects = FindObjectsOfType<MonoBehaviour>(true);
        List<ISaveableInterface> saveableList = new List<ISaveableInterface>();

        foreach (var saveable in allSaveableObjects)
        {
            if (saveable is ISaveableInterface canSave)
            {
                saveableList.Add(canSave);
            }

            if (saveable is GameManager)
            {
                if (!Directory.Exists(Application.persistentDataPath + "/" + directory))
                    Directory.CreateDirectory(Application.persistentDataPath + "/" + directory);

                BinaryFormatter bFormat = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/" + directory + "/" + fileName);
                scoreData = new ScoreData();
                bFormat.Serialize(file, scoreData);
                file.Close(); Debug.LogFormat($"{fileName} was saved");
            }
        }

        SaveData saveData = new SaveData();

        foreach (var saveable in saveableList)
        {
            saveData.id.Add(saveable.SaveID);
            saveData.jsonData.Add(saveable.SavedData());
        }

        string jsonString = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(filePath, jsonString);
    }

    private void LoadGameSave()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("No save could be found");
            return;
        }

        if (File.Exists(Application.persistentDataPath + "/" + directory + "/" + fileName))
        {
            try
            {
                BinaryFormatter bFormat = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + directory + "/" + fileName, FileMode.Open);
                scoreData = (ScoreData)bFormat.Deserialize(file);
                file.Close();
                GameManager.score = scoreData.score;
            }
            catch (SerializationException)
            {
                Debug.LogWarning("Could not load file");
            }
        }

        string jsonString = File.ReadAllText(filePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(jsonString);

        var allSaveableObjects = FindObjectsOfType<MonoBehaviour>(true);
        List<ISaveableInterface> saveableList = new List<ISaveableInterface>();

        foreach (var save in allSaveableObjects)
        {
            if (save is ISaveableInterface saveable)
            {
                saveableList.Add(saveable);
            }
        }

        int loadedCount = 0;

        for(int i = 0; i < saveData.id.Count; i++)
        {
            string ids = saveData.id[i];
            string savedJson = saveData.jsonData[i];

            foreach(var saveable in saveableList)
            {
                if(saveable.SaveID == ids)
                {
                    saveable.LoadFromData(savedJson);
                    loadedCount++;
                    break;
                }
            }
        }
    }
}
