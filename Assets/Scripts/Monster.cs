using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveDir = new Vector2(1f, 0);
    [SerializeField] float moveSpeed;
    BoxCollider2D checkGroundColl;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        checkGroundColl = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
        CheckGround();
    }

    public void CheckGround()
    {
        if (!checkGroundColl.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            transform.localScale= new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            moveDir *= -1;
        }
    }
}
