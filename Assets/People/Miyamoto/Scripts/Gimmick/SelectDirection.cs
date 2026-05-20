using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectDirection : MonoBehaviour
{
    public event Action<int> OnSelectDirection;

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
    private void Selecting()
    {
        _isSelecting = !_isSelecting;
        _image.enabled = _isSelecting;
    }

    private GolfInputAction _action; 
    private Image _image;
    private bool _isSelecting = false;
    private GimmickBase _gimmickBase;
    private void Awake()
    {
        _action = new();
        _image = GetComponent<Image>();
        _image.enabled = false;
        _gimmickBase = FindAnyObjectByType<GimmickBase>();
    }
    private void OnEnable()
    {
        _gimmickBase.OnSelect += Selecting;
        _action.Enable();
        _action.Player.Direction.started += SelectHandler;
    }
    private void OnDestroy()
    {
        _gimmickBase.OnSelect += Selecting;
        _action.Player.Direction.started -= SelectHandler;
    }
}
