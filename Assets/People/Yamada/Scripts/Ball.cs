using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            StartCoroutine(LoadSceneMode());
            Debug.Log("Goal!");
        }
    }

    private IEnumerator LoadSceneMode()
    {
        AudioManager.Instance.PlaySE("Clear");

        yield return new WaitForSeconds(3f);

        FadeSceneLoader.Instance.LoadScene("StageSelect");
    }
}