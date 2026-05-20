using UnityEngine;
/// <summary>
/// 選んだステージの情報を取得するクラス
/// </summary>
public static class StageDataManager
{
    /// <summary>ステージのクリア状況などお持っているデータ</summary>
    public static StageData StageData;
    /// <summary>ステージプレファブ</summary>
    public static GameObject Stage;
    public static void SetStageInfo(StageData stageData, GameObject stage)
    {
        StageData = stageData;
        Stage = stage;
        Debug.Log($"ステージの情報を追加{stageData.name}");
    }
    public static StageData GetCurrentStageData()
    {
        return StageData;
    }
}