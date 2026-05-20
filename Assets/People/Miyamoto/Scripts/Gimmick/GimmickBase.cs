using DG.Tweening;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
///     ギミックの基底クラス
/// </summary>
public virtual class GimmickBase : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    ///     ポインターのクリック操作を処理する。
    /// </summary>
    /// <remarks>実装時はクリックの検証、状態更新、および必要に応じてイベントの伝播を行うこと。</remarks>
    /// <param name="eventData">クリックに関する情報を保持する PointerEventData。null を渡してはならない。</param>
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    /// <summary>
    ///     これをOverrideして派生クラスでギミックの回転処理を実装する
    /// </summary>
    /// <param name="direction">回転方向</param>
    protected virtual void Rotate(RotateDirection direction)
    {
        //this.gameObject.transform.DORotate();
    }
}

public enum RotateDirection
{
    Left,
    Right,
}