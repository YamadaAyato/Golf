using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Goal"))
        {
            FadeSceneLoader.Instance.LoadScene("StageSelect");
            Debug.Log("Goal!");
        }
    }
}
