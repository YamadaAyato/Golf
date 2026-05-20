using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public void OnClick()
    {
        SceneLoarder.LoadScene(_sceneName);
    }
}
