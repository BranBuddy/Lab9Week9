using System;
using UnityEngine;

[Serializable]
public class TransformSaverData
{
    public Vector3 localPosition;
    public string saveID;
}

public class TransformSaver : MonoBehaviour, ISaveableInterface
{
    public string saveId = Guid.NewGuid().ToString();
    public string SaveID => saveId;

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
    }

    public string SavedData()
    {
        TransformSaverData data = new TransformSaverData
        {
            localPosition = this.transform.position,
            saveID = SaveID
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
    }
}
