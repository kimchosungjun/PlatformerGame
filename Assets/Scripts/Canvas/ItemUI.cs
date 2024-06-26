using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas; // 드래그 시 위치하는 곳
    Transform beforeParent; // 돌아올 위치
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// idx 넘버를 전달받으면 해당 아이템을 Json으로부터 검색하여 찾고, 해당 아이템 데이터에서 필요한 정보만을 가져와서 해당 스크립트에 채움
    /// </summary>
    /// <param name="_idx"></param>
    public void SetItem(string _idx)
    {
        canvas = InventoryManager.Instance.CanvasInventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParent = transform.parent;
        transform.SetParent(canvas);
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent == canvas)
        {
            transform.SetParent(beforeParent);
            transform.position = beforeParent.position;
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
