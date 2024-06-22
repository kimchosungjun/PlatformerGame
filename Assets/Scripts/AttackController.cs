using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Transform hand;
    [SerializeField] GameObject throwWeapon;
    [SerializeField] Transform throwWeaponPos;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        CheckAim();
        CheckCreate();
    }

    private void CheckAim()
    {
        Vector2 mouserWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouserWorldPos - playerPos;

        float angle = Quaternion.FromToRotation(
            transform.localScale.x>0?Vector3.right:Vector3.left, fixedPos).eulerAngles.z;
        hand.rotation = Quaternion.Euler(0,0,angle); 
    }

    private void CheckCreate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CreateWeapon();
        }
    }

    private void CreateWeapon()
    {
        GameObject go = Instantiate(throwWeapon);
    }
}
