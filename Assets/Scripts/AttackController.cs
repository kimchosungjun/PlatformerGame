using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        CheckAim();
    }

    private void CheckAim()
    {
        Vector2 mouserWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

    }
}
