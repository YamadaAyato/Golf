using System.Collections.Generic;
using System.Linq;
using Template.Editor;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private IMission[] _missions;

    private StageData _stageData;

    private void Awake()
    {
        foreach (var mission in _missions)
        {
            mission.Initialize();
        }
        _stageData = StageDataManager.GetCurrentStageData();
    }
    private void OnDestroy()
    {
        for (var i = 0; i < _missions.Length; i++)
        {
            if (_missions[i].IsCompleted())
                _stageData.SetClearInfo(i);
        }
    }
}
