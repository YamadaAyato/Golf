using Template.Editor;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private IMission[] _missions;

    private StageData _stageData;
    private GameObject _stage;

    private void Start()
    {
        AudioManager.Instance.PlayBGM("BGM");
        foreach (var mission in _missions)
        {
            mission.Initialize();
        }
        (_stageData, _stage) = StageDataManager.GetCurrentStageData();
    }
    private void OnDestroy()
    {
        Debug.Log("クリア情報を読み込んでいます");
        for (var i = 0; i < _missions.Length; i++)
        {
            if (_missions[i].IsCompleted())
                _stageData.SetClearInfo(i);
        }
    }
}
