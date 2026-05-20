using UnityEngine;

public class FadeChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public void OnClick()
    {
        FadeSceneLoader.Instance.LoadScene(_sceneName);
    }
}
