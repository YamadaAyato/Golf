using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ボールの発射における調整から発射処理、着地判定までを行う。
/// </summary>
public class BallShoter : MonoBehaviour
{
    [SerializeField]
    ShotPowerUI _shotPowerUI;
    [SerializeField]
    float _maxShotPower;
    [SerializeField]
    float _shotPowerBase;
    [SerializeField]
    ShotPhase _nowPhase;

    Rigidbody2D _rb2d;

    float _shotAngle = 0;
    float _shotPower = 0;
    float _relativeDirection = 1;

    /// <summary>
    /// ボールの発射の進行度を発射中・角度決定・力決定の3段階で分ける。
    /// </summary>
    public enum ShotPhase
    {
        Wait,
        Angle,
        Power
    }

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(_nowPhase == ShotPhase.Angle)
            {
                _nowPhase = ShotPhase.Power;
            }
            else if(_nowPhase == ShotPhase.Power)
            {
                _nowPhase = ShotPhase.Wait;
                Shot();
            }
        }
        else if (_nowPhase == ShotPhase.Wait && _rb2d.linearVelocity.magnitude < 0.1f)
        {
            _rb2d.linearVelocity = Vector2.zero;
            _nowPhase = ShotPhase.Angle;
            _shotPowerUI.gameObject.SetActive(true);
            _shotPowerUI.transform.position = transform.position;
        }
    }

    void FixedUpdate()
    {
        if(_nowPhase == ShotPhase.Angle)
        {
            var _reletiveX = Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue() + new Vector3(0,0,10)).x - transform.position.x;
            if(_relativeDirection != Mathf.Sign(_reletiveX))
            {
                _shotAngle = 0;
                _relativeDirection = Mathf.Sign(_reletiveX);
                _shotPowerUI.transform.eulerAngles = transform.eulerAngles = new Vector2(0, _relativeDirection == 1 ? 0 : 180);
            }

            _shotPowerUI.RotateAngle(Mathf.PingPong(_shotAngle += 1.5f, 90));
        }
        else if(_nowPhase == ShotPhase.Power)
        {
            _shotPowerUI.SelectPower(Mathf.PingPong(_shotPower += 1.5f, _maxShotPower) / _maxShotPower);
        }
        else
        {
            var viewPoint = Camera.main.WorldToViewportPoint(transform.position);
            if(viewPoint.x > 1 || viewPoint.x < 0 || viewPoint.y > 1 || viewPoint.y < 0)
            {
                FallOutScreen();
            }
        }
    }

    /// <summary>
    /// ボールの発射処理及び発射用データリセット
    /// </summary>
    void Shot()
    {
        transform.eulerAngles = _shotPowerUI.transform.eulerAngles;
        _rb2d.AddForce(transform.right * _shotPowerBase * (Mathf.PingPong(_shotPower += 1.5f, _maxShotPower) / _maxShotPower), ForceMode2D.Impulse);

        _shotAngle = 0;
        _shotPower = 0;
        transform.eulerAngles = _shotPowerUI.transform.eulerAngles = Vector3.zero;
        _shotPowerUI.ResetGauge();
        _shotPowerUI.gameObject.SetActive(false);
    }

    /// <summary>
    /// 着地できず落下または画面外へ出た場合の処理
    /// </summary>
    void FallOutScreen()
    {
        Destroy(gameObject);
    }
}
