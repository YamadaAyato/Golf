using System;
using System.Collections.Generic;
using System.Linq;
using Template.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ボールの発射における調整から発射処理、着地判定までを行う。
/// </summary>
public class BallShoter : MonoBehaviour
{
    public event Action OnShot;
    [SerializeField]
    private ShotPowerUI _shotPowerUI;
    [SerializeField]
    private float _maxShotPower;
    [SerializeField]
    private float _shotPowerBase;
    [SerializeField,ReadOnly]
    private ShotPhase _nowPhase;

    private Rigidbody2D _rb2d;
    private PutterSwing _putterSwing;

    private float _shotAngle = 0;
    private float _shotPower = 0;
    private float _relativeDirection = 1;
    private List<RaycastHit2D> _onGroundChecker = new();
    private Transform _startPoint;
    private bool _canEnterShotMode;

    /// <summary>
    /// ボールの発射の進行度を発射中・角度決定・力決定の3段階で分ける。
    /// </summary>
    public enum ShotPhase
    {
        Wait,
        Angle,
        Power
    }

    public void Initialie(Transform startPoint)
    {
        _startPoint = startPoint;
    }

    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _putterSwing = transform.GetChild(0).GetComponent<PutterSwing>();
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
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _canEnterShotMode = true;
            }

            if (_canEnterShotMode == false)
            {
                return;
            }

            _onGroundChecker.Clear();
            for(int i = 0; i < 3; i++)
            {
                _onGroundChecker.Add(Physics2D.Linecast(transform.position  - new Vector3(-0.5f + (0.5f * i), i == 1 ? 0.5f : 0), transform.position - new Vector3(-0.5f + (0.5f * i), 0.6f)));
            }

            if(_onGroundChecker.Count(ray => ray.collider != null) > 0)
            {
                _rb2d.linearVelocity = Vector2.zero;
                _nowPhase = ShotPhase.Angle;
                _canEnterShotMode = false;

                _shotPowerUI.gameObject.SetActive(true);
                _putterSwing.gameObject.SetActive(true);
                _putterSwing.transform.SetParent(transform);
                _putterSwing.BackPosition();
                _shotPowerUI.transform.position = transform.position;
            }
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

            float processedAngle = Mathf.PingPong(_shotAngle += 2f, 90);
            _shotPowerUI.RotateAngle(processedAngle);
            _putterSwing.ArrangeAngle(processedAngle);
        }
        else if(_nowPhase == ShotPhase.Power)
        {
            float processedPower = Mathf.PingPong(_shotPower += 1.5f, _maxShotPower) / _maxShotPower;
            _shotPowerUI.SelectPower(processedPower);
            _putterSwing.ArrangePower(processedPower);
        }
        else
        {
            var viewPoint = Camera.main.WorldToViewportPoint(transform.position);
            if(viewPoint.x > 1 || viewPoint.x < 0 || viewPoint.y < 0)
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
        _putterSwing.transform.parent = null;

        transform.eulerAngles = _shotPowerUI.transform.eulerAngles;
        _rb2d.AddForce(transform.right * _shotPowerBase * (Mathf.PingPong(_shotPower += 1.5f, _maxShotPower) / _maxShotPower), ForceMode2D.Impulse);
        AudioManager.Instance.PlaySE("Shot");

        _shotAngle = 0;
        _shotPower = 0;
        transform.eulerAngles = _shotPowerUI.transform.eulerAngles = Vector3.zero;
        _shotPowerUI.ResetGauge();
        _putterSwing.Swing();
        _shotPowerUI.gameObject.SetActive(false);
        OnShot?.Invoke();
    }

    /// <summary>
    /// 着地できず落下または画面外へ出た場合の処理
    /// </summary>
    void FallOutScreen()
    {
        if (_startPoint == null)
        {
            Debug.LogError("StartPointが設定されていません。");
            return;
        }

        _rb2d.linearVelocity = Vector2.zero;
        _rb2d.angularVelocity = 0f;

        transform.position = _startPoint.position;
        transform.rotation = Quaternion.identity;

        _shotAngle = 0f;
        _shotPower = 0f;
        _nowPhase = ShotPhase.Wait;
        _canEnterShotMode = false;

        _shotPowerUI.ResetGauge();
        _shotPowerUI.gameObject.SetActive(false);
        _putterSwing.gameObject.SetActive(false);
    }
}
