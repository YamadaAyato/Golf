using UnityEngine;

public class BallSpawer : MonoBehaviour
{
    [SerializeField] private BallShoter _ballShoter;

    private void Start()
    {
        Transform startPoint = FindAnyObjectByType<Start>().transform;
        BallShoter ballShoter = Instantiate(_ballShoter, startPoint.position, Quaternion.identity);
    }
}
