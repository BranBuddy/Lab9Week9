using LitJson;
using System.IO;
using System.Linq;
using UnityEngine;

public interface ISaveableInterface
{
    string SaveID { get; }
    string SavedData();
    void LoadFromData(string data);

}
