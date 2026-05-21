using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private List<StageData> _stageDatas;

    public void FirstStart()
    {
        foreach (var stage in _stageDatas)
        {
            stage.ResetClearInfo();
        }
        FadeSceneLoader.Instance.LoadScene("StageSelect");
    }
    public void ContinueStart()
    {
        FadeSceneLoader.Instance.LoadScene("StageSelect");
    }
}
