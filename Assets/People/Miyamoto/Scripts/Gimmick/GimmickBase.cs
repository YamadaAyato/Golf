using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///     ギミックの基底クラス
/// </summary>
public class GimmickBase : MonoBehaviour, IPointerClickHandler
{
    public event Action OnSelect;

    [SerializeField] protected float _rotateDuration;
    [SerializeField] protected float _rotateAngle;
    [SerializeField] protected Ease _rotateEase;
    protected SelectDirection _selectDirection;
    /// <summary>
    ///     ポインターのクリック操作を処理する。
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect?.Invoke();
    }

    /// <summary>
    ///     これをOverrideして派生クラスでギミックの回転処理を実装する
    /// </summary>
    protected virtual void Rotate(int dir)
    {
        Debug.Log("回転");
        var currentRotation = transform.rotation.eulerAngles;
        transform
            .DORotate(
                new Vector3(
                    currentRotation.x,
                    currentRotation.y,
                    currentRotation.z += _rotateAngle* -dir),
                _rotateDuration)
            .SetEase(_rotateEase);
    }
    private void Awake()
    {
        _selectDirection = FindAnyObjectByType<SelectDirection>();
    }
    private void OnEnable()
    {
        _selectDirection.OnSelectDirection += Rotate;
    }
    private void OnDisable()
    {
        _selectDirection.OnSelectDirection -= Rotate;
    }
}