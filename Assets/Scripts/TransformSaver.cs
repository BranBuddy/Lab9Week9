using System;
using UnityEngine;

[Serializable]
public class TransformSaverData
{
    public Vector3 localPosition;
    public bool isAlive;
    public string saveID;
}

public class TransformSaver : MonoBehaviour, ISaveableInterface
{
    public string saveId = Guid.NewGuid().ToString();
    public string SaveID => saveId;
    public bool isAlive = true;
    public bool aliveState => isAlive;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(saveId))
        {
            saveId = Guid.NewGuid().ToString();
        }
    }

    private void Awake()
    {
        if (string.IsNullOrEmpty(saveId))
        {
            saveId = Guid.NewGuid().ToString();
        }

        isAlive = true;
    }

    public string SavedData()
    {
        TransformSaverData data = new TransformSaverData
        {
            localPosition = this.transform.position,
            saveID = SaveID,
            isAlive = aliveState
        };

        return JsonUtility.ToJson(data);
    }
    
    public void LoadFromData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return;
        }

        TransformSaverData loadData = JsonUtility.FromJson<TransformSaverData>(data);

        this.transform.position = loadData.localPosition;
        this.transform.gameObject.SetActive(aliveState);
    }
}
