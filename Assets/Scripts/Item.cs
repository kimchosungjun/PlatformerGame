using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class Item : MonoBehaviour
{
    [SerializeField] string idx;
    InventoryManager inventoryManager;
    private void Awake()
    {
        inventoryManager = InventoryManager.Instance;
        CItemData testData = new CItemData();
        testData.idx = "000001";
        testData.sprite = GetComponent<SpriteRenderer>().sprite.name;
        string val = JsonConvert.SerializeObject(testData);
        Debug.Log(val);
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // getmask�� �������� ���̾�
        // nametolayer�� �ϳ��� ���̾�
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            inventoryManager.GetItem(idx);
            Destroy(gameObject);
        }
    }
}
