using UnityEngine;
using UnityEngine.EventSystems;

public class GimmickClickRelay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GimmickBase _target;

    //子オブジェクトに付けることで、クリックイベントを親オブジェクトのGimmickBaseに伝えるクラス
    // ポインターのクリック操作を処理する。
    public void OnPointerClick(PointerEventData eventData)
    {
        _target.OnPointerClick(eventData);
    }
}