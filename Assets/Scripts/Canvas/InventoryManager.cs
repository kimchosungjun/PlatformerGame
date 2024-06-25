using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] GameObject viewInventory; // 인벤토리 뷰
    [SerializeField] GameObject fabItem; // 인벤토리에 생성될 프리팹
    List<Transform> listTrsInventory = new List<Transform>();
    private void Awake()
    {
        if(Instance==null)
        { 
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        listTrsInventory.Clear();
        Transform[] childs = viewInventory.GetComponentsInChildren<Transform>(); // 자신부터 검색 : 자신의 transform도 포함됨
        listTrsInventory.AddRange(childs);
        listTrsInventory.RemoveAt(0); 
    }

    public void InActiveInventory()
    {
        bool isActive = viewInventory.activeSelf;
        viewInventory.SetActive(!isActive);
    }

    /// <summary>
    /// 비어있는 아이템 슬롯을 리턴 : -1은 비어있는 슬롯이 없다는 의미
    /// </summary>
    /// <returns></returns>
    private int GetEmptyItemSlot()
    {
        int count = listTrsInventory.Count;
        for(int i=0; i<count; i++)
        {
            Transform trsSlot = listTrsInventory[i];
            if (trsSlot.childCount == 0)
            {
                return i;
            }
        }
        return -1;
    }
}
