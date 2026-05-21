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
            _image.enabled = _isSelecting;
        }
    }
    public void Selecting()
    {
        _isSelecting = !_isSelecting;
        _image.enabled = _isSelecting;
    }

    private GolfInputAction _action; 
    private Image _image;
    private bool _isSelecting = false;
    private void Awake()
    {
        _action = new();
        _image = GetComponent<Image>();
        _image.enabled = false;
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
