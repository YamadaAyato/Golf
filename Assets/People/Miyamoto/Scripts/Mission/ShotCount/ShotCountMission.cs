using System;
using TMPro;
using UnityEngine;

public class ShotCountMission : IMission, IDisposable
{
    public void Initialize()
    {
        _ballShoter = GameObject.FindAnyObjectByType<BallShoter>();
        _ballShoter.OnShot += CountIncrement;
        _ballShoter.OnShot += TextUpdate;
        TextUpdate();   
    }

    public void Dispose()
    {
        _ballShoter.OnShot -= CountIncrement;
        _ballShoter.OnShot -= TextUpdate;
    }
    private void CountIncrement()
    {
        _shotCount++;
    }

    public bool IsCompleted()
    {
        return _shotCount < _maxShotCount;
    }
    private void TextUpdate()
    {
        _shotCountText.text = $"打つ回数を{_shotCount}/{_maxShotCount}回以内にゴールしろ";
    }
    [SerializeField] private int _maxShotCount;
    [SerializeField] private TMP_Text _shotCountText;
    private BallShoter _ballShoter;
    private int _shotCount;
}
