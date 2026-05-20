using System.Collections.Generic;
using Template.Editor;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeReference, SubclassSelector] private List<IMission> _missions;

    private void Awake()
    {
        foreach (var mission in _missions)
        {
            mission.Initialize();
        }
    }
    private void OnDestroy()
    {

    }
}
