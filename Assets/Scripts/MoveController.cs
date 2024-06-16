using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    // Manager�� �񵿱������� ȣ���� ���� ���
    // Controller�� Update���� ����Ͽ� ���������� ȣ���ϰ� ��, ȣ���� ������ Ÿ ����� �ҷ��� ����ϴ� ��찡 ����
    
    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rb;
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0f; // �������� �������� ��
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
        // tag�� string
        // layer�� int ��, �Ϲ����� int�� �ƴ�
        // layer�� ��ȣ�� 2^n�� ����
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
        // ����׵� üũ �뵵�� �� ī�޶� ���� �׷��� �� �ִ�. Debug.DrawLine();
        // ������ ����׺��� �� ���� ������ �׸� �� �ִ�. Gizmos.DrawLine();
        // Handles�� using unityeditor�� �ؾ���
    }

    private void Moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed; // -1 or 1
        moveDir.y = rb.velocity.y;
        rb.velocity = moveDir;
    }
}
