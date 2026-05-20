using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;

    [SerializeField] private float moveSpeed = 3f;

    Transform goal;

    SpriteRenderer sp;

    void Start()
    {
        goal = positionB;

        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 移動
        transform.position = Vector3.MoveTowards
            (transform.position, goal.position, moveSpeed * Time.deltaTime);

        // flip向きの変更
        sp.flipX = goal.position.x < transform.position.x;

        // 着いたら切り替え
        if (Vector3.Distance(transform.position, goal.position) < 0.1f)
        {
            if (goal == positionB)
            {
                goal = positionA;
            }
            else
            {
                goal = positionB;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigi = collision.rigidbody;

        if (rigi != null)
        {
            rigi.linearVelocity = new Vector2(0, rigi.linearVelocity.y);
        }
    }
}
