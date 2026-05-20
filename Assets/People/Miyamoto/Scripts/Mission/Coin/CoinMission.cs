using System;
using TMPro;
using UnityEngine;

public class CoinMission : IMission, IDisposable
{
    public void Initialize()
    {
        _coins = GameObject.FindObjectsByType<Coin>(FindObjectsSortMode.None);
        _maxCount = _coins.Length;
        foreach (Coin coin in _coins)
        {
            coin.OnCoinGet += IncrementCount;
        }
        CoinTextUpdate();
    }
    public void Dispose()
    {
        _coinMissionText = null;
        foreach (var coin in _coins)
        {
            if(coin != null)
            coin.OnCoinGet -= IncrementCount;
        }
    }
    private void IncrementCount()
    {
        _currentGetCoinCount++;
        CoinTextUpdate();
        if (_currentGetCoinCount == _maxCount)
        {
            _coinMissionText.text = "全て集めた‼";
        }
    }
    private void CoinTextUpdate()
    {
        _coinMissionText.text = $"コインを手に入れろ‼{_currentGetCoinCount}/{_maxCount}";
    }

    public bool IsCompleted()
    {
        return _currentGetCoinCount >= _maxCount;
    }

    [SerializeField] private TMP_Text _coinMissionText;
    private Coin[] _coins;
    private int _currentGetCoinCount = 0;
    private int _maxCount;
}