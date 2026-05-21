using NUnit.Framework;
using DG.Tweening;
using UnityEngine;

public class MerryFerrisWheelRotate : GimmickBase
{
    //親オブジェクトに付けることで、子オブジェクトからクリックイベントを受け取るクラス

    [SerializeField]
    private Transform[] _rotateGameObjects;

    //[SerializeField]
    //private Transform _centerVisual;

    [SerializeField]
    private LineRenderer[] _lineRenderers;

    private Vector3 _centerPosition;
    private Quaternion[] _defaultRotations;
    private bool _isRotating = false;

    void Start()
    {
        //_centerVisual.position = _centerPosition;

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

    void Update()
    {
        // LineRendererを使用して、回転させるオブジェクトと中心位置を線で結ぶ
        for (int i = 0; i < _rotateGameObjects.Length; i++)
        {
            LineRenderer line =
                _rotateGameObjects[i]
                .GetComponent<LineRenderer>();
            if (line == null) continue;

            line.SetPosition(0, _rotateGameObjects[i].position);
            line.SetPosition(1, _centerPosition);
        }
    }

    // 回転処理をオーバーライド
    protected override void Rotate(int dir)
    {
        // すでに回転中の場合は処理をスキップ
        if (_isRotating) return;
        // 回転開始のフラグを立てる
        _isRotating = true;

        float previousAngle = 0f;

        // DOTween.Toを使用して、回転角度を0から指定された角度まで変化させるアニメーションを作成
        DOTween.To(
            () => 0f,
            angle =>
            {
                // 前回の角度からの差分を計算
                float delta = angle - previousAngle;
                // 各オブジェクトを中心位置を軸にして回転させる
                for (int i = 0; i < _rotateGameObjects.Length; i++)
                {
                    _rotateGameObjects[i].RotateAround(
                        _centerPosition,
                        Vector3.forward,
                        delta * -dir
                    );
                    // 回転後にオブジェクトの回転を初期回転にリセット
                    _rotateGameObjects[i].rotation =
                        _defaultRotations[i];
                }
                // 現在の角度を保存
                previousAngle = angle;
            },
            _rotateAngle,
            _rotateDuration
        )
        .SetEase(_rotateEase)
        .OnComplete(
            () =>
            {
                // 回転が完了したらフラグをリセット
                _isRotating = false;
            }
        );
    }

    // Gizmosを使用して、回転の中心位置と回転させるオブジェクトを視覚化
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(_centerPosition, 0.2f);

        Gizmos.color = Color.red;

        if (_rotateGameObjects == null) return;

        foreach (var obj in _rotateGameObjects)
        {
            if (obj == null) continue;

            Gizmos.DrawLine(
                obj.position,
                _centerPosition
            );
        }
    }
}
