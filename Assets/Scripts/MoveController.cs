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

    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rb;
    BoxCollider2D coll;
    Animator anim;

    Vector3 moveDir;
    bool isJump = false; // ���� ���׸� ���� ����
    float verticalVelocity = 0f; // �������� �������� ��
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float groundCheckLength;
    [SerializeField] bool isOnGround;

    #region Unity Cycle
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
  
    void Update()
    {
        CheckGround();
        CheckGravity();
        Move();
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, LayerMask.GetMask("Ground"));
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
        if(isOnGround)
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), Color.red);
        else
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLength), Color.blue);
    }
    #endregion
}
