using DG.Tweening;
using UnityEngine;

public class FerrisWheelRotate : GimmickBase
{
    [SerializeField]
    private Transform[] _rotateGameObjects;

    private Vector3 _centerPosition;
    private Quaternion[] _defaultRotations;
    private Tween _rotateTween;

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
        // 既存の回転アニメーションがあれば停止
        _rotateTween?.Kill();

        float previousAngle = 0f;
        float targetAngle = _rotateAngle * -dir;

        _rotateTween = DOVirtual.Float(
                  0f,
                  targetAngle,
                  _rotateDuration,
                  currentAngle =>
                  {
                      float deltaAngle = currentAngle - previousAngle;
                      previousAngle = currentAngle;

                      // 各オブジェクトを中心位置を軸に回転させる
                      for (int i = 0; i < _rotateGameObjects.Length; i++)
                      {
                          _rotateGameObjects[i].RotateAround(
                              _centerPosition,
                              Vector3.forward,
                              deltaAngle);

                          _rotateGameObjects[i].rotation = _defaultRotations[i];
                      }
                  })
              .SetEase(_rotateEase)
              .OnComplete(() =>
              {
                  _selectDirection.OnSelectDirection -= Rotate;
              });
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
