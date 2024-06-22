using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSword : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 force;
    bool isRight;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, (isRight ? -360 : 360) * Time.deltaTime));
    }
}
