using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("커서 이미지")]
    [SerializeField,Tooltip("0은 디폴트 <color=red>1은 클릭</color>")] List<Texture2D> cursors;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.SetCursor
                (cursors[1], new Vector2(cursors[1].width * 0.5f, cursors[1].height * 0.5f), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor
            (cursors[0], new Vector2(cursors[0].width * 0.5f, cursors[0].height * 0.5f), CursorMode.Auto);
        }
    }
}
