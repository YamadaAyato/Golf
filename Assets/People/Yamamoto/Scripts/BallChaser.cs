using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float minY = -100f;
    [SerializeField] private float maxY = 20f;
    [SerializeField] private float minX = -3f;
    [SerializeField] private float maxX = 3f;

    private void LateUpdate()
    {
        // ボールのY座標取得
        float targetY = target.position.y;
        float targetX = target.position.x;

        // 範囲制限
        targetY = Mathf.Clamp(targetY, minY, maxY);
        targetX = Mathf.Clamp(targetX, minX, maxX);

        // 現在位置取得
        Vector3 currentPos = transform.position;

        // Yだけ変更
        currentPos.y = targetY;
        // Xだけ変更
        currentPos.x = targetX;

        // 反映
        transform.position = currentPos;
    }
}