using UnityEngine;

public class StageLoader : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Instantiate(StageDataManager.Stage);
    }
}
