using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonManager : MonoBehaviour
{
    public static JsonManager Instance;
    List<CItemData> itemDatas;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        InitJsonDatas();
    }

    private void InitJsonDatas()
    {
       TextAsset itemData = Resources.Load("ItemData") as TextAsset;
        itemDatas = JsonConvert.DeserializeObject<List<CItemData>>(itemData.ToString());
    }

    public string GetSpriteNameFromIdx(string _idx)
    {
        if (itemDatas == null)
            return string.Empty;
        return itemDatas.Find(x => x.idx == _idx).sprite;
    }
}
