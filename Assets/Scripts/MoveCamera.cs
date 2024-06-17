using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform chaseTransform;

    void LateUpdate()
    {
        Vector3 chasePos = chaseTransform.position;
        chasePos.z = transform.position.z;
        transform.position = chasePos;
    }
}
