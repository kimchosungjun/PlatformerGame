using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform canvas; // �巡�� �� ��ġ�ϴ� ��
    Transform beforeParent; // ���ƿ� ��ġ
    CanvasGroup canvasGroup;
    Image itemImage;

    private void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>(); //getcomponent�� ���
        canvas = InventoryManager.Instance.CanvasInventory;
        itemImage = GetComponent<Image>();
    }

    /// <summary>
    /// idx �ѹ��� ���޹����� �ش� �������� Json���κ��� �˻��Ͽ� ã��, �ش� ������ �����Ϳ��� �ʿ��� �������� �����ͼ� �ش� ��ũ��Ʈ�� ä��
    /// </summary>
    /// <param name="_idx"></param>
    public void SetItem(string _idx)
    {
        string name = JsonManager.Instance.GetSpriteNameFromIdx(_idx);
        itemImage.sprite = SpriteManager.Instance.GetSprite(name);
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
