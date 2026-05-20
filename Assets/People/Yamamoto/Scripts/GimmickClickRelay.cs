using UnityEngine;
using UnityEngine.EventSystems;

public class GimmickClickRelay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GimmickBase _target;

    //子オブジェクトに付けることで、クリックイベントをGimmickBaseに転送するクラス
    // ポインターのクリック操作を処理する。
    public void OnPointerClick(PointerEventData eventData)
    {
        _target.OnPointerClick(eventData);
    }
}