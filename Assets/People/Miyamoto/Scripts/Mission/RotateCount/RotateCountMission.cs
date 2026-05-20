using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RotateCountMission : IMission, IDisposable
{
    public void Initialize()
    {
        _selectDirection = GameObject.FindAnyObjectByType<SelectDirection>();
        _selectDirection.OnSelectDirection += OnRotateHandler;

        TextUpdate();
    }
    public bool IsCompleted()
    {
        return _currentRotateCount <= _maxRotateCount;
    }

    public void Dispose()
    {
            _selectDirection.OnSelectDirection -= OnRotateHandler;
        
        _selectDirection = null;
    }
    private void TextUpdate()
    {
        _rotateCountText.text = $"{_currentRotateCount}/{_maxRotateCount}";
    }
    private void OnRotateHandler(int a)
    {
        IncrementRotateCount();
        TextUpdate();
    }
    private void IncrementRotateCount()
    {
        _currentRotateCount++;
    }
    [SerializeField] private int _maxRotateCount;
    [SerializeField] private TMP_Text _rotateCountText;
    private int _currentRotateCount;
    private SelectDirection _selectDirection;
}
