using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class StageSelector : MonoBehaviour
{
    [SerializeField] StageInfo[] _stageInfos;
    [SerializeField] Color _color;
    private GolfInputAction _inputAction;
    private int _currentStageIndex;
    private void Awake()
    {
        _inputAction = new();
        _stageInfos[0].Image.color = _color;
        foreach (var stageInfo in _stageInfos)
        {
            stageInfo.LoadClearInfo();
        }
    }
    private void OnEnable()
    {
        _inputAction.Stage.Enable();
        _inputAction.Stage.Move.started += OnMove;
        _inputAction.Stage.Select.started += OnSelect;
    }
    private void OnDisable()
    {
        _inputAction.Stage.Disable();
        _inputAction.Stage.Move.started -= OnMove;
        _inputAction.Stage.Select.started -= OnSelect;
    }
    private void OnDestroy()
    {
        _stageInfos = null;
        _inputAction.Dispose();
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        var dir = (int)ctx.ReadValue<float>();
        var nextIndex = _currentStageIndex + dir;

        if (nextIndex < 0 || nextIndex > _stageInfos.Length - 1)
            return;

        AudioManager.Instance.PlaySE("Select");
        ImageUpdate(nextIndex);
        SetCurrentIndex(nextIndex);
    }
    private void OnSelect(InputAction.CallbackContext ctx)
    {
        StageDataManager.SetStageInfo(_stageInfos[_currentStageIndex].StageData,
                                      _stageInfos[_currentStageIndex].Stage);
        FadeSceneLoader.Instance.LoadScene("InGame");
    }
    private void SetCurrentIndex(int nextIndex)
    {
        _currentStageIndex = nextIndex;
    }
    private void ImageUpdate(int nextIndex)
    {
        _stageInfos[_currentStageIndex].Image.color = Color.white;
        _stageInfos[nextIndex].Image.color = _color;
    }
}
