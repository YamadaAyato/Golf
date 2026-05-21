using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPositions;
    [SerializeField] private float _moveSpeed = 3f;

    private int _currentTargetIndex;
    private SpriteRenderer _sp;

    private void Start()
    {
        if (!TryGetComponent(out _sp))
        {
            Debug.LogError("SpriteRendererが存在しません。", this);
            enabled = false;
            return;
        }

        if (_targetPositions == null || _targetPositions.Length <= 1)
        {
            Debug.LogError("ターゲット位置は2個以上必要です。", this);
            enabled = false;
            return;
        }

        _currentTargetIndex = 1;
    }

    private void Update()
    {
        // 現在の移動先を取得する。
        Transform targetPoint = _targetPositions[_currentTargetIndex];

        // 現在の位置から移動先に向かって移動する。
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            _moveSpeed * Time.deltaTime);

        // 移動先の位置に応じて、スプライトの向きを変える。
        _sp.flipX = targetPoint.position.x > transform.position.x;

        // 移動先の位置に十分近づいたら、次の移動先を設定する。
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            _currentTargetIndex = (_currentTargetIndex + 1) % _targetPositions.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        if (collision.gameObject.TryGetComponent(out Rigidbody2D targetRb))
        {
            targetRb.linearVelocity = new Vector2(0f, targetRb.linearVelocity.y);

            if (this.gameObject.TryGetComponent(out Collider2D collider))
            {
                collider.enabled = false;
            }
        }
    }
}