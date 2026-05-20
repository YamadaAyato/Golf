using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action OnCoinGet;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BallShoter>(out var ball))
        {
            OnCoinGet?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
