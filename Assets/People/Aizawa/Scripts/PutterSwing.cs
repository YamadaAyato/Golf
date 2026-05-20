using UnityEngine;

public class PutterSwing : MonoBehaviour
{
    [SerializeField]
    Vector2 _posOffset;
    [SerializeField]
    Sprite[] _putterImages = new Sprite[2];

    SpriteRenderer _putterrenderer;

    void Awake()
    {
        _putterrenderer = GetComponent<SpriteRenderer>();
    }

    public void ArrangeAngle(float shotAngle)
    {
        var swayValue = new Vector2(0, shotAngle / 100);
        transform.localPosition =  _posOffset - swayValue;
    }

    public void ArrangePower(float shotPower)
    {
        var swingValue = new Vector3(0, 0, shotPower * 40);
        transform.localEulerAngles = -swingValue;
    }

    public void Swing()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 15);
        _putterrenderer.sprite = _putterImages[1];
    }

    public void BackPosition()
    {
        transform.localPosition = _posOffset;
        transform.localEulerAngles = Vector2.zero;
        _putterrenderer.sprite = _putterImages[0];
    }
}
