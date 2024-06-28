using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] List<Sprite> allSprites;
    public Sprite GetSprite(string _spriteName)
    {
        int count = allSprites.Count;
        for(int i=0; i<count; i++)
        {
            if (_spriteName == allSprites[i].name)
                return allSprites[i];
        }
        return null;
    }
}
