using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Goal"))
        {
            LoadSceneMode();
            Debug.Log("Goal!");
        }
    }

    private async void LoadSceneMode()
    {
        AudioManager.Instance.PlaySE("Clear");
        await Task.Delay(3000);
        FadeSceneLoader.Instance.LoadScene("StageSelect");
    }
}
