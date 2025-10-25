using LitJson;
using UnityEngine;
using System.IO;
using System.Linq;

public class TransformSaver : MonoBehaviour
{
    private const string SCORE_KEY = "score";
    private const string LOCAL_POSITION_KEY = "localPosition";

    private JsonData SerializeValue(object obj) { return JsonMapper.ToObject(JsonUtility.ToJson(obj)); }

    public JsonData SavedData
    {
        get
        {
            var result = new JsonData();
            result[LOCAL_POSITION_KEY] = SerializeValue(transform.localPosition);
            return result;
        }
    }
    
    public void LoadFromData(JsonData data)
    {
        if (data.Keys.Contains(LOCAL_POSITION_KEY))
        {
            transform.localPosition = DeserializeValue<Vector3>(data[LOCAL_POSITION_KEY]);
        }
     }
}
