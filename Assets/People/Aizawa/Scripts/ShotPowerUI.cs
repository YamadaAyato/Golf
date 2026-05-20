using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 発射方向とその力を矢印とゲージで表示する。
/// </summary>
public class ShotPowerUI : MonoBehaviour
{
    [SerializeField]
    Image _gaugeImage;

    void Awake()
    {
        _gaugeImage = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// 矢印の向きを変更する。
    /// </summary>
    /// <param name="shotAngle">矢印の向きの値</param>
    public void RotateAngle(float shotAngle)
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, shotAngle);
    }

    /// <summary>
    /// ゲージの進行度を変更する。
    /// </summary>
    /// <param name="shotPower">ゲージの進行度の値</param>
    public void SelectPower(float shotPower)
    {
        _gaugeImage.fillAmount = shotPower;
    }

    /// <summary>
    /// ゲージを最大の状態にリセットする。
    /// </summary>
    public void ResetGauge()
    {
        _gaugeImage.fillAmount = 1;
    }
}
