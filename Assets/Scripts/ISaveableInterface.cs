using LitJson;
using System.IO;
using System.Linq;

public interface ISaveableInterface
{
    string SaveID { get; }
    JsonData SavedData { get; }
    void LoadFromData(JsonData data);

}

public static class SavingService
{
    private const string SCORE_KEY = "score";
    private const string LOCAL_POSITION_KEY = "localPosition";
}
