using NUnit.Framework;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private Transform[] _rotateGameObjects;

    private int centerOfPositionX;
    private int centerOfPositionY;

    void Start()
    {
        //2つのオブジェクトの中心座標を求める
        centerOfPositionX = (int)(_rotateGameObjects[0].position.x + _rotateGameObjects[1].position.x) / 2;
        centerOfPositionY = (int)(_rotateGameObjects[0].position.y + _rotateGameObjects[1].position.y) / 2;
    }
    void Update()
    {
        
    }
}
