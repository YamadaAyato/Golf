using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private TitleBallLoop ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            ball.Respawn();
        }
    }
}