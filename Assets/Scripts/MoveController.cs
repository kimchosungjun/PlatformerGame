using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Manager는 비동기적으로 호출할 때만 사용
    // Controller는 Update문을 사용하여 동기적으로 호출하게 함, 호출이 없더라도 타 기능을 불러서 사용하는 경우가 많다
    
    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rb;
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0f; // 수직으로 떨어지는 힘
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float groundCheckLength;
    [SerializeField] Color colorGroundCheck;
    [SerializeField] bool isGround;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        Moving();
    }

    private bool CheckGround()
    {
        // tag는 string
        // layer는 int 단, 일반적인 int는 아님
        // layer의 번호는 2^n을 뜻함
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));
        if (hit)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
        return isGround;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength),Color.blue);
        // 디버그도 체크 용도로 씬 카메라에 선을 그려줄 수 있다. Debug.DrawLine();
        // 기즈모는 디버그보다 더 많은 라인을 그릴 수 있다. Gizmos.DrawLine();
        // Handles는 using unityeditor를 해야함
    }

    private void Moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed; // -1 or 1
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;
    }
}
