using NUnit.Framework;
using UnityEngine;

public class FerrisWheelRotate : GimmickBase
{
    [SerializeField]
    private Transform[] _rotateGameObjects;

    private Vector3 _centerPosition;
    private Quaternion[] _defaultRotations;

    void Start()
    {
        // 回転させるオブジェクトの中心位置を計算
        _centerPosition =
        (_rotateGameObjects[0].position +
         _rotateGameObjects[1].position) / 2f;
        // 各オブジェクトの初期回転を保存
        _defaultRotations = new Quaternion[_rotateGameObjects.Length];
        
        
        for (int i = 0; i < _rotateGameObjects.Length; i++)
        {
            _defaultRotations[i] =
                _rotateGameObjects[i].rotation;
        }
}
    // 回転処理をオーバーライド
    protected override void Rotate(int dir)
    {
        // 各オブジェクトを中心位置を軸に回転させる
        for (int i = 0; i < _rotateGameObjects.Length; i++)
        {
            _rotateGameObjects[i].RotateAround(
                _centerPosition,
                Vector3.forward,
                _rotateAngle * -dir
            );

            // 回転後にオブジェクトの回転を初期回転にリセット
            _rotateGameObjects[i].rotation =
                _defaultRotations[i];
        }
    }

    //void Update()
    //{
    //    for (int i = 0; i < _rotateGameObjects.Length; i++)
    //    {
    //        _rotateGameObjects[i].RotateAround(
    //            _centerPosition,
    //            Vector3.forward,
    //            90 * Time.deltaTime
    //        );

    //        _rotateGameObjects[i].rotation =
    //            _defaultRotations[i];
    //    }
    //}
}
