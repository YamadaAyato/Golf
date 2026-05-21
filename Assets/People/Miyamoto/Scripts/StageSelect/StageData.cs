using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    private string SaveKey => $"ClearInfo_{name}";

    public void SetClearInfo(int index)
    {
        var saved = PlayerPrefs.GetString(SaveKey, "");
        var set = new HashSet<string>(
            string.IsNullOrEmpty(saved) ? System.Array.Empty<string>() : saved.Split(',')
        );
        set.Add(index.ToString());
        PlayerPrefs.SetString(SaveKey, string.Join(",", set));
        PlayerPrefs.Save();
    }

    public bool GetClearInfo(int index)
    {
        var saved = PlayerPrefs.GetString(SaveKey, "");
        if (string.IsNullOrEmpty(saved)) return false;
        return System.Array.Exists(saved.Split(','), x => x == index.ToString());
    }
    public int GetClearedCount(int missionCount)
    {
        var count = 0;
        for (var i = 0; i < missionCount; i++)
        {
            if (GetClearInfo(i)) count++;
        }
        return count;
    }
    [ContextMenu("クリア状況をリセット")]
    public void ResetClearInfo()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}