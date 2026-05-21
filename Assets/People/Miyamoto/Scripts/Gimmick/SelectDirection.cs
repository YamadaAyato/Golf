using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectDirection : MonoBehaviour
{
    public event Action<int> OnSelectDirection;

    //親オブジェクトに付けることで、子オブジェクトからクリックイベントを受け取るクラス

    private void SelectHandler(InputAction.CallbackContext ctx)
    {
        if(!_isSelecting) return;

        if (ctx.started)
        {
            float dir = ctx.ReadValue<float>();
            OnSelectDirection?.Invoke((int)dir);
            _isSelecting = false;
            foreach (var image in _images)
            {
                image.enabled = _isSelecting;
            }
        }
    }
    public void Selecting()
    {
        _isSelecting = !_isSelecting;
        Debug.Log($"{_isSelecting}");
        foreach (var image in _images)
        {
            image.enabled = _isSelecting;
        }
    }
    [SerializeField] private Image[] _images;
    private GolfInputAction _action; 
    private bool _isSelecting = false;
    private void Awake()
    {
        _action = new();
        foreach (var image in _images)
        {
            image.enabled = false;
        }
    }
    private void OnEnable()
    {
        _action.Enable();
        _action.Player.Direction.started += SelectHandler;
    }
    private void OnDisable()
    {
        _action.Player.Direction.started -= SelectHandler;
        _action.Disable();
    }
}
