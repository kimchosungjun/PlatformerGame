using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    #region Study
    // Manager는 비동기적으로 호출할 때만 사용
    // Controller는 Update문을 사용하여 동기적으로 호출하게 함, 호출이 없더라도 타 기능을 불러서 사용하는 경우가 많다
    //if (isOnGround && isJump && Input.GetKeyDown(KeyCode.Space))
    //{
    //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // 지긋이 미는 힘, 벡터만 넣었을 때
    //    isJump = true;                                // : 이단 점프는 addforce로 구현 불가
    //}
    // 물리와 관련된(Rigidbody) 것에는 Time.deltaTime을 사용하지 않아도 된다.
    // 디버그도 체크 용도로 씬 카메라에 선을 그려줄 수 있다. Debug.DrawLine();
    // 기즈모는 디버그보다 더 많은 라인을 그릴 수 있다. Gizmos.DrawLine();
    // Handles는 using unityeditor를 해야함
    #endregion

    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rb;
    CapsuleCollider2D capColl;
    BoxCollider2D boxColl;
    Animator anim;
    Camera mainCamera;

    Vector3 moveDir;
    bool isJump = false; // 점프 버그를 막기 위함
    float verticalVelocity = 0f; // 수직으로 떨어지는 힘
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float groundCheckLength;
    [SerializeField] bool isOnGround;

    #region Unity Cycle
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capColl = GetComponent<CapsuleCollider2D>();
        boxColl = GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;
    }
  
    void Update()
    {
        CheckGround();
        CheckGravity();
        Move();
        CheckAim();
        Jump();
        DoAnimation();
    }
    #endregion

    #region Method
    private void CheckGround()
    {
        isOnGround = false;
        if (verticalVelocity > 0f)
            return;
        RaycastHit2D hit = Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));
        if (hit)
            isOnGround = true;
    }

    private void CheckGravity()
    {
        if (!isOnGround)
        {
            verticalVelocity += Physics2D.gravity.y * Time.deltaTime;
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

    private void Move()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed; // Return -1 or 1
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;
    }

    private void CheckAim()
    {
        //Vector3 scale = transform.localScale;
        //if (scale.x !=1.0f && moveDir.x>0)
        //{ 
        //    scale.x = 1.0f;
        //    transform.localScale = scale;
        //}
        //else if(scale.x!=-1.0f && moveDir.x<0)
        //{
        //    scale.x = -1.0f;
        //    transform.localScale = scale;
        //}
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // input.mouseposition => canvas의 위치 기준, 카메라의 상태에 따라 다름 (orthographic, perspective)
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;
        Vector3 playerScale = transform.localScale;
        if(fixedPos.x>0 && playerScale.x != 1.0f)
        {
            playerScale.x = 1.0f;
        }
        else if(fixedPos.x<0 && playerScale.x != -1.0f)
        {
            playerScale.x = -1.0f;
        }
        transform.localScale = playerScale;
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
    }

    private void DoAnimation()
    {
        anim.SetInteger("Horizontal",(int)moveDir.x);
        anim.SetBool("IsOnGround",isOnGround);
    }
    #endregion

    #region Debug Method
    private void OnDrawGizmos()
    {
        //if(isOnGround)
        //    Debug.DrawLine(boxColl.bounds.center, boxColl.bounds.center- new Vector3(0, groundCheckLength), Color.red);
        //else
        //    Debug.DrawLine(boxColl.bounds.center, boxColl.bounds.center - new Vector3(0, groundCheckLength), Color.blue);
    }
    #endregion
}
