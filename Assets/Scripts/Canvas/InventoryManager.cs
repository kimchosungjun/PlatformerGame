using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] GameObject viewInventory; // �κ��丮 ��
    [SerializeField] GameObject fabItem; // �κ��丮�� ������ ������
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
        Transform[] childs = viewInventory.GetComponentsInChildren<Transform>(); // �ڽź��� �˻� : �ڽ��� transform�� ���Ե�
        listTrsInventory.AddRange(childs);
        listTrsInventory.RemoveAt(0); 
    }

    public void InActiveInventory()
    {
        bool isActive = viewInventory.activeSelf;
        viewInventory.SetActive(!isActive);
    }

    /// <summary>
    /// ����ִ� ������ ������ ���� : -1�� ����ִ� ������ ���ٴ� �ǹ�
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
