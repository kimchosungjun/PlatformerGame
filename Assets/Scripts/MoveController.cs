using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Manager는 비동기적으로 호출할 때만 사용
    // Controller는 Update문을 사용하여 동기적으로 호출하게 함, 호출이 없더라도 타 기능을 불러서 사용하는 경우가 많다
    
    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rb;
    BoxCollider2D coll;
    Animator anim;

    Vector3 moveDir;
    float verticalVelocity = 0f; // 수직으로 떨어지는 힘
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float groundCheckLength;
    [SerializeField] Color colorGroundCheck;
    [SerializeField] bool isGround;

    bool isJump = false; // 점프 버그를 막기 위함

    [SerializeField] bool isOnGround = false;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        CheckGravity();
        Move();
        Jump();
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength),Color.blue);
        // 디버그도 체크 용도로 씬 카메라에 선을 그려줄 수 있다. Debug.DrawLine();
        // 기즈모는 디버그보다 더 많은 라인을 그릴 수 있다. Gizmos.DrawLine();
        // Handles는 using unityeditor를 해야함
    }

    private void CheckGround()
    {
        isOnGround = false;
        if (verticalVelocity > 0f)
            return;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));
        if (hit)
            isOnGround = true;
        //isJump = !isOnGround;
    }

    private void Move()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed; // -1 or 1
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;
    }
    private void Jump()
    {
        if (!isOnGround)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;

        }

        //if (isOnGround && isJump && Input.GetKeyDown(KeyCode.Space))
        //{
        //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // 지긋이 미는 힘, 벡터만 넣었을 때
        //    isJump = true;                                // : 이단 점프는 addforce로 구현 불가
        //}
    }

    private void CheckGravity()
    {
        if (!isOnGround)
        {
            verticalVelocity += Physics2D.gravity.y * Time.deltaTime;
            // 물리와 관련된(Rigidbody) 것에는 Time.deltaTime을 사용하지 않아도 된다.
            if (verticalVelocity < -10f)
            {
                verticalVelocity = -10f;
            }
        }
        else if (isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }
        else if (isOnGround)
        {
            verticalVelocity = 0f;
        }
        rb.velocity = new Vector2(rb.velocity.x, verticalVelocity);
    }

    private void DoAnimation()
    {
        //anim.SetInteger("Horizontal",(int)moveDir.x);
        //anim.SetBool("isOnGround",isOnGround);
    }
}
