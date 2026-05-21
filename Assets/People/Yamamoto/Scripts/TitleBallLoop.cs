using UnityEngine;

public class TitleBallLoop : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = respawnPoint.position;
        transform.rotation = Quaternion.identity;
    }
}