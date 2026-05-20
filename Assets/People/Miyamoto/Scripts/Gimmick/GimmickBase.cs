using System;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
///     ギミックの基底クラス
/// </summary>
public class GimmickBase : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float _rotateDuration;
    [SerializeField] private float _rotateAngle;
    [SerializeField] private Image _directionImage;
    [SerializeField] private Ease _rotateEase;

    private bool _isSelecting = false;
    private CancellationTokenSource _cts;

    private void Awake()
    {
        _isSelecting = false;

        if (_directionImage == null)
        {
            Debug.LogError($"[{nameof(GimmickBase)}] _directionImage が未アサインです", this);
            return;
        }

        _directionImage.enabled = false;
    }

    private void OnDestroy()
    {
        CancelWaiting();
    }

    /// <summary>
    ///     ポインターのクリック操作を処理する。
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // 待機中ならキャンセル（トグルOFF）
        if (_isSelecting)
        {
            CancelWaiting();
            _isSelecting = false;
            _directionImage.enabled = false;
            return;
        }

        // 待機開始（トグルON）
        _isSelecting = true;
        _directionImage.enabled = true;
        _ = WaitForRotateAsync();
    }

    /// <summary>
    ///     これをOverrideして派生クラスでギミックの回転処理を実装する
    /// </summary>
    protected virtual void Rotate(RotateDirectionType direction)
    {
        var currentRotation = transform.rotation.eulerAngles;
        transform
            .DORotate(
                new Vector3(
                    currentRotation.x,
                    currentRotation.y,
                    currentRotation.z + GetRotateAngle(direction)),
                _rotateDuration)
            .SetEase(_rotateEase);
    }

    /// <summary>
    ///     回転角度を返す
    /// </summary>
    private int GetRotateAngle(RotateDirectionType direction)
    {
        return direction == RotateDirectionType.Left? (int)_rotateAngle : -(int)_rotateAngle;
    }

    /// <summary>
    ///     キャンセル可能なキー入力待機ループ
    /// </summary>
    private async Task WaitForRotateAsync()
    {
        // 前回のCTSが残っていれば破棄
        CancelWaiting();
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        try
        {
            while (!token.IsCancellationRequested)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    Rotate(RotateDirectionType.Left);
                    break;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    Rotate(RotateDirectionType.Right);
                    break;
                }

                await Task.Yield();
            }
        }
        catch (OperationCanceledException)
        {
            // キャンセルは正常系なので無視
        }
        finally
        {
            // 回転入力が確定した場合のみ状態をリセット
            if (!token.IsCancellationRequested)
            {
                _isSelecting = false;
                _directionImage.enabled = false;
            }

            _cts?.Dispose();
            _cts = null;
        }
    }

    /// <summary>
    ///     待機タスクをキャンセルしてCTSを破棄する
    /// </summary>
    private void CancelWaiting()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }
}