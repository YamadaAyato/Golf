using UnityEngine;

/// <summary>
/// 発射時のパターを振る演出を制御する。
/// </summary>
public class PutterSwing : MonoBehaviour
{
    [SerializeField]
    private Vector2 _posOffset;
    [SerializeField]
    private Sprite[] _putterImages = new Sprite[2];

    private SpriteRenderer _putterRenderer;

    void Awake()
    {
        _putterRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 発射角度とパターの高さを揃える。
    /// </summary>
    /// <param name="shotAngle">発射角度。範囲が広いため1%に縮小する。</param>
    public void ArrangeAngle(float shotAngle)
    {
        var swayValue = new Vector2(0, shotAngle / 100);
        transform.localPosition =  _posOffset - swayValue;
    }

    /// <summary>
    /// 発射力とパターの角度を揃える。
    /// </summary>
    /// <param name="shotPower">発射力。範囲が狭いため拡大する。</param>
    public void ArrangePower(float shotPower)
    {
        var swingValue = new Vector3(0, 0, shotPower * 40);
        transform.localEulerAngles = -swingValue;
    }

    /// <summary>
    /// パターを振り、振った画像に変更。
    /// </summary>
    public void Swing()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 15);
        _putterRenderer.sprite = _putterImages[1];
    }

    /// <summary>
    /// パターをボールの位置に戻し、振る前の画像に変更。
    /// </summary>
    public void BackPosition()
    {
        transform.localPosition = _posOffset;
        transform.localEulerAngles = Vector2.zero;
        _putterRenderer.sprite = _putterImages[0];
    }
}
