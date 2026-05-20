using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     フェード演出付きのシーンローダー。
/// </summary>
public class FadeSceneLoader : MonoBehaviour
{
    public static FadeSceneLoader Instance { get; private set; }

    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration;

    private bool _isLoading;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Color color = _fadeImage.color;
        color.a = 0f;
        _fadeImage.color = color;

        _fadeImage.raycastTarget = false;
    }

    /// <summary>
    ///     フェード付きでシーンを読み込む。
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (_isLoading)
        {
            return;
        }

        _isLoading = true;

        _fadeImage.raycastTarget = true;

        // フェードインしてからシーンを読み込み、フェードアウトする。
        _fadeImage
            .DOFade(1f, _fadeDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                SceneLoarder.LoadScene(sceneName);

                _fadeImage
                    .DOFade(0f, _fadeDuration)
                    .SetUpdate(true)
                    .OnComplete(() =>
                    {
                        _isLoading = false;
                        _fadeImage.raycastTarget = false;
                    });
            });
    }
}