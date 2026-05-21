using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class StageInfo
{
    public Image Image;
    public GameObject Stage;
    public StageData StageData => _stageData;
    [SerializeField] private StageData _stageData;
    [SerializeField] private Image[] _stars;

    public void LoadClearInfo()
    {
        var clearedCount = _stageData.GetClearedCount(_stars.Length);
        for (var i = 0; i < clearedCount; i++)
        {
            var anim = _stars[i].GetComponent<Animator>();

            anim.SetBool("IsComplete", true);
        }
    }
}
