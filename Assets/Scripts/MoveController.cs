using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    #region Study
    // Manager�� �񵿱������� ȣ���� ���� ���
    // Controller�� Update���� ����Ͽ� ���������� ȣ���ϰ� ��, ȣ���� ������ Ÿ ����� �ҷ��� ����ϴ� ��찡 ����
    //if (isOnGround && isJump && Input.GetKeyDown(KeyCode.Space))
    //{
    //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // ������ �̴� ��, ���͸� �־��� ��
    //    isJump = true;                                // : �̴� ������ addforce�� ���� �Ұ�
    //}
    // ������ ���õ�(Rigidbody) �Ϳ��� Time.deltaTime�� ������� �ʾƵ� �ȴ�.
    // ����׵� üũ �뵵�� �� ī�޶� ���� �׷��� �� �ִ�. Debug.DrawLine();
    // ������ ����׺��� �� ���� ������ �׸� �� �ִ�. Gizmos.DrawLine();
    // Handles�� using unityeditor�� �ؾ���
    #endregion

    Rigidbody2D rb;
    CapsuleCollider2D capColl;
    BoxCollider2D boxColl;
    Animator anim;
    Camera mainCamera;

    [Header("�÷��̾� �̵� �� ����")]
    // �̵�
    Vector3 moveDir;
    [SerializeField] float moveSpeed;
    // ����
    bool isJump = false; 
    float verticalVelocity = 0f; 
    [SerializeField] float jumpForce;
    [SerializeField] float groundCheckLength;
    [SerializeField] bool isOnGround;
    // �� ����
    bool isWallJump;
    [SerializeField] bool isOnWall;
    [SerializeField] float wallJumpTimer = 0.0f; // ���߿� ser�����
    [SerializeField] float wallJumpTime = 0.3f;
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

    #region Public Method
    public void TriggerEnter(HitBoxType _hitBoxType, Collider2D _collider)
    {
        switch (_hitBoxType)
        {
            case HitBoxType.GroundCheck:
                //isOnGround = true;
                break;
            case HitBoxType.WallCheck:
                isOnWall = true;
                break;
            case HitBoxType.BodyCheck:
                break;
            default:
                break;
        }
    }

    public void TriggerExit(HitBoxType _hitBoxType, Collider2D _collider)
    {
        switch (_hitBoxType)
        {
            case HitBoxType.GroundCheck:
                //isOnGround = false;
                break;
            case HitBoxType.WallCheck:
                isOnWall = false;
                break;
            case HitBoxType.BodyCheck:
                break;
            default:
                break;
        }
    }

    #endregion

    #region Private Method
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
        if (isWallJump)
        {
            isWallJump = false;
            Vector2 direction = rb.velocity;
            direction.x *= -1f;
            rb.velocity = direction;
            verticalVelocity = jumpForce * 0.5f;
        }
        else if (!isOnGround)
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
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // input.mouseposition => canvas�� ��ġ ����, ī�޶��� ���¿� ���� �ٸ� (orthographic, perspective)
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
            if (isOnWall && moveDir.x != 0 && Input.GetKeyDown(KeyCode.Space))
            {
                isWallJump = true;

            }
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
